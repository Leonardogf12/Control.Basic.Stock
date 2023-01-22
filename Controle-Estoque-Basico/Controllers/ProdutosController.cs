using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Controle_Estoque_Basico.Data;
using Controle_Estoque_Basico.Models;
using Microsoft.AspNetCore.Http;
using Controle_Estoque_Basico.ViewModels;
using Controle_Estoque_Basico.Interfaces;

namespace Controle_Estoque_Basico.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly AppDbContext _context;        
        private readonly ICategoriasRepositorio _repCat;
        private readonly ISaidaProdutosRepositorio _repSpro;

        public ProdutosController(AppDbContext context, ICategoriasRepositorio repCat, ISaidaProdutosRepositorio repSpro)
        {
            _context = context;
            _repCat = repCat;
            _repSpro = repSpro;
        }

        #region GETS

        public async Task<IActionResult> Index()
        {
            var t = await _context.Produto.Include(p => p.Categoria).Where(x => x.PRO_ISDELETED == false).ToListAsync();
            return View(t);
        }

        public async Task<IActionResult> ListaProdutosPartialAsync()
        {
            var lista = await _context.Produto.ToListAsync();

            return PartialView("ListaProdutosPartial", lista);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produto
                .FirstOrDefaultAsync(m => m.PRO_ID == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
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

        #endregion

        #region POSTS

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Produto produto)
        {
            try
            {
                _context.Add(produto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PRO_ID, PRO_NOME, PRO_DESCRICAO, PRO_DATAENTRADA, PRO_VALIDADE, PRO_QUANTIDADE, PRO_IDCATEGORIA")] Produto produto)
        {
            if (id != produto.PRO_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdutoExists(produto.PRO_ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(produto);
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
                
                return PartialView("ListaProdutosPartial", await _context.Produto.Include(x=>x.Categoria).Where(x => x.PRO_ISDELETED == false).ToListAsync());
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

            return PartialView("ListaProdutosPartial", await _context.Produto.Include(x=>x.Categoria).Where(x => x.PRO_ISDELETED == false).ToListAsync());
        }

        #endregion

    }
}
