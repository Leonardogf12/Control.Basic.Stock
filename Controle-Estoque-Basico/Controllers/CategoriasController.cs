using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Controle_Estoque_Basico.Data;
using Controle_Estoque_Basico.Models;
using Controle_Estoque_Basico.Repositorios;
using Controle_Estoque_Basico.Interfaces;

namespace Controle_Estoque_Basico.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly AppDbContext _context;

        private readonly ICategoriasRepositorio _repCat;

        public CategoriasController(AppDbContext context, ICategoriasRepositorio repCat)
        {
            _context = context;
            _repCat = repCat;
        }


        #region GETS

        public async Task<IActionResult> Index()
        {
            return View(await _context.Categoria.Where(x=>x.CAT_ISDELETED == false).ToListAsync());
        }

        public async Task<IActionResult> ListaProdutosPartialAsync()
        {
            var lista = await _context.Categoria.ToListAsync();

            return PartialView("ListaCategoriasPartial", lista);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categoria
                .FirstOrDefaultAsync(m => m.CAT_ID == id);
            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categoria.FindAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }
            return View(categoria);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categoria
                .FirstOrDefaultAsync(m => m.CAT_ID == id);
            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        #endregion


        #region POSTS

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Categoria categoria)
        {
            try
            {
                if (ModelState.IsValid)
                {
                   
                     await _repCat.Salvar(categoria);

                    //_context.Add(categoria);
                    //await _context.SaveChangesAsync();
                    
                    return RedirectToAction(nameof(Index));
                }
                return View(categoria);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CAT_ID,CAT_NOME,CAT_ISDELETED")] Categoria categoria)
        {
            if (id != categoria.CAT_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoria);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriaExists(categoria.CAT_ID))
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
            return View(categoria);
        }

        [HttpPost]
        public async Task<IActionResult> Excluir(int _id)
        {
            try
            {
                if (_id == 0)
                    return Json("Categoria não encontrada ou id inexistente.");

                var categoria = await _context.Categoria.FindAsync(_id);

                if (categoria == null)
                    return Json("Categoria não encontrada.");

                _context.Categoria.Remove(categoria);
                await _context.SaveChangesAsync();


                return PartialView("ListaCategoriasPartial", await _context.Categoria.ToListAsync());
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
                    var categoria = await _context.Categoria.FindAsync(aRegistros[i]);

                    if (categoria != null)
                        categoria.CAT_ISDELETED = true;
                    
                    await _context.SaveChangesAsync();
                }

                return PartialView("ListaCategoriasPartial", await _context.Categoria.Where(x => x.CAT_ISDELETED == false).ToListAsync());
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                return Json(ex.Message);
            }
        }

        #endregion


        #region COMUM

        private bool CategoriaExists(int id)
        {
            return _context.Categoria.Any(e => e.CAT_ID == id);
        }

        public async Task<int> TotalCategorias()
        {
            return await _context.Categoria.Where(x => x.CAT_ISDELETED == false).CountAsync();
        }

        #endregion

    }
}
