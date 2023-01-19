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

        public ProdutosController(AppDbContext context, ICategoriasRepositorio repCat)
        {
            _context = context;
            _repCat = repCat;
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
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produto.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }
            return View(produto);
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
        public async Task<IActionResult> Edit(int id, [Bind("PRO_ID,PRO_NOME,PRO_DESCRICAO,PRO_DATAENTRADA, PRO_VALIDADE,PRO_QUANTIDADE")] Produto produto)
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
        public async Task<IActionResult> Excluir(int _id)
        {
            try
            {
                if (_id == 0)
                    return Json("Produto não encontrado ou id inexistente.");

                var produto = await _context.Produto.FindAsync(_id);

                if (produto == null)
                    return Json("Produto não encontrado.");

                _context.Produto.Remove(produto);
                await _context.SaveChangesAsync();


                return PartialView("ListaProdutosPartial", await _context.Produto.ToListAsync());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            string mensagem = string.Empty;


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

                    //_context.Produto.Remove(produto);
                    await _context.SaveChangesAsync();
                }
                
                return PartialView("ListaProdutosPartial", await _context.Produto.Where(x => x.PRO_ISDELETED == false).ToListAsync());
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

        public async void CarregaViewCreate()
        {

            var listaCategoria = await _context.Categoria.Where(x => x.CAT_ISDELETED == false).ToListAsync();

            if (listaCategoria.Count() > 0)
            {
                var categorias = new SelectList(listaCategoria, "CAT_ID", "CAT_NOME").ToList();

                ViewBag.CategoriasVB = categorias;

                ViewData["VehicleId"] = categorias;
            }
        }


        #endregion

    }
}
