using Controle_Estoque_Basico.Data;
using Controle_Estoque_Basico.Interfaces;
using Controle_Estoque_Basico.Models;
using Controle_Estoque_Basico.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Controle_Estoque_Basico.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ICategoriasRepositorio _repCat;
        private readonly ISaidaProdutosRepositorio _repSpro;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProdutosController(AppDbContext context, ICategoriasRepositorio repCat, ISaidaProdutosRepositorio repSpro, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _repCat = repCat;
            _repSpro = repSpro;
            _webHostEnvironment = webHostEnvironment;
        }

        #region GETS

        public async Task<IActionResult> Index()
        {
            var t = await _context.Produto.Include(p => p.Categoria).Where(x => x.PRO_ISDELETED == false).ToListAsync();
            return View(t);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produto.Where(x => x.PRO_ID == id
                                                && x.PRO_ISDELETED == false).FirstOrDefaultAsync();

            if (produto == null)
            {
                return NotFound();
            }

            var vm = new ProdutosViewModel { Produto = produto };

            return View(vm);
        }

        public async Task<IActionResult> Create()
        {

            List<Categoria> categorias = await _repCat.BuscaCategorias();
            var vm = new ProdutosViewModel { Categorias = categorias };

            return View(vm);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            List<Categoria> categorias = await _repCat.BuscaCategorias();
            var vm = new ProdutosViewModel { Categorias = categorias };

            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produto.FindAsync(id);

            vm.Produto = produto;

            if (produto == null)
            {
                return NotFound();
            }

            return View(vm);
        }

        public async Task<IActionResult> ListaProdutosPartialAsync()
        {
            var lista = await _context.Produto.ToListAsync();

            return PartialView("ListaProdutosPartial", lista);
        }

        #endregion

        #region POSTS

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProdutosViewModel model)
        {
            try
            {
                if (model.Produto.PRO_IDCATEGORIA > 0)
                {
                    string uniqueFileName = UploadedFile(model);

                    if (string.IsNullOrEmpty(uniqueFileName))
                        model.Produto.ImagemProdutoModel = "sem_foto.png";
                    else
                        model.Produto.ImagemProdutoModel = uniqueFileName;

                    _context.Add(model.Produto);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                
                model.Categorias = await _repCat.BuscaCategorias();

                return View(model);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProdutosViewModel model)
        {
            try
            {
                string uniqueFileName = UploadedFile(model);

                if (string.IsNullOrEmpty(uniqueFileName))
                    model.Produto.ImagemProdutoModel = "sem_foto.png";
                else
                    model.Produto.ImagemProdutoModel = uniqueFileName;

                _context.Update(model.Produto);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProdutoExists(model.Produto.PRO_ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> ExcluirVarios(string _registros)
        {
            try
            {
                string mensagem = string.Empty;

                string[] separators = { "," };
                string[] aAux;
                int[] aRegistros;

                aAux = _registros.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                aRegistros = Array.ConvertAll(aAux, int.Parse);

                for (int i = 0; i < aRegistros.Length; i++)
                {
                    var produto = await _context.Produto.FindAsync(aRegistros[i]);

                    produto.PRO_ISDELETED = true;

                    await _context.SaveChangesAsync();
                }

                return PartialView("ListaProdutosPartial", await _context.Produto.Include(x => x.Categoria).Where(x => x.PRO_ISDELETED == false).ToListAsync());
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                return Json(ex.Message);
            }
        }

        #endregion

        #region COMUM

        private bool ProdutoExists(int id)
        {
            return _context.Produto.Any(e => e.PRO_ID == id);
        }

        public async Task<int> TotalProdutosEmEstoque()
        {
            return await _context.Produto.Where(x => x.PRO_ISDELETED == false).CountAsync();
        }

        public async Task<IActionResult> InformarBaixaProduto(int _id, decimal _qtd)
        {

            Produto produto = await _context.Produto.Where(x => x.PRO_ID == _id).FirstOrDefaultAsync();

            if (produto == null)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                return Json($"Produto não encontrado, contate o suporte.");
            }

            if (produto != null)
            {
                if (produto.PRO_QUANTIDADE < _qtd)
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                    return Json($"A quantidade informada não pode ser maior que a quantidade em Estoque.");
                }

                if (produto.PRO_QUANTIDADE >= _qtd)
                    produto.PRO_QUANTIDADE -= _qtd;


                if (produto.PRO_QUANTIDADE == 0)
                    produto.PRO_STATUS = true;

                await _context.SaveChangesAsync();

                SaidaProduto saidaProduto = new SaidaProduto();

                saidaProduto.SPRO_IDPRODUTO = produto.PRO_ID;
                saidaProduto.SPRO_IDCATEGORIA = produto.PRO_IDCATEGORIA;
                saidaProduto.SPRO_QUANTIDADE = _qtd;
                saidaProduto.SPRO_DATASAIDA = DateTime.Now;

                await _repSpro.Salvar(saidaProduto);
            }

            return PartialView("ListaProdutosPartial", await _context.Produto.Include(x => x.Categoria).Where(x => x.PRO_ISDELETED == false).ToListAsync());
        }

        private string UploadedFile(ProdutosViewModel model)
        {
            string uniqueFileName = null;

            if (model.ImagemProdutoViewModel != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Imagens");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImagemProdutoViewModel.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ImagemProdutoViewModel.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        #endregion

    }
}
