using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Controle_Estoque_Basico.Data;
using Controle_Estoque_Basico.Models;
using Controle_Estoque_Basico.Interfaces;

namespace Controle_Estoque_Basico.Controllers
{
    public class SaidaProdutosController : Controller
    {
        private readonly AppDbContext _context;

        private readonly ISaidaProdutosRepositorio _repSpro;

        public SaidaProdutosController(AppDbContext context, ISaidaProdutosRepositorio repSpro)
        {
            _context = context;
            _repSpro = repSpro;
        }


        public async Task<IActionResult> Index()
        {
            var t = await _context.SaidaProduto.Include(x => x.Produto).Include(x => x.Categoria).Where(x => x.SPRO_ISDELETED == false).ToListAsync();
           return View(t);
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saidaProduto = await _context.SaidaProduto
                .Include(x=>x.Produto)
                .Include(x=>x.Categoria)
                .FirstOrDefaultAsync(m => m.SPRO_ID == id);
            
            if (saidaProduto == null)
            {
                return NotFound();
            }

            return View(saidaProduto);
        }


        public IActionResult Create()
        {
            ViewData["PRO_IDCATEGORIA"] = new SelectList(_context.Categoria, "CAT_ID", "CAT_NOME");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PRO_ID,PRO_NOME,PRO_DESCRICAO,PRO_QUANTIDADE,PRO_DATAENTRADA,PRO_VALIDADE,PRO_IDCATEGORIA,PRO_ISDELETED")] Produto produto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(produto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PRO_IDCATEGORIA"] = new SelectList(_context.Categoria, "CAT_ID", "CAT_NOME", produto.PRO_IDCATEGORIA);
            return View(produto);
        }

        // GET: SaidaProdutos/Edit/5
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
            ViewData["PRO_IDCATEGORIA"] = new SelectList(_context.Categoria, "CAT_ID", "CAT_NOME", produto.PRO_IDCATEGORIA);
            return View(produto);
        }

        // POST: SaidaProdutos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PRO_ID,PRO_NOME,PRO_DESCRICAO,PRO_QUANTIDADE,PRO_DATAENTRADA,PRO_VALIDADE,PRO_IDCATEGORIA,PRO_ISDELETED")] Produto produto)
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
            ViewData["PRO_IDCATEGORIA"] = new SelectList(_context.Categoria, "CAT_ID", "CAT_NOME", produto.PRO_IDCATEGORIA);
            return View(produto);
        }

        // GET: SaidaProdutos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produto
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(m => m.PRO_ID == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        // POST: SaidaProdutos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produto = await _context.Produto.FindAsync(id);
            _context.Produto.Remove(produto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProdutoExists(int id)
        {
            return _context.Produto.Any(e => e.PRO_ID == id);
        }

        public async Task<decimal> TotalSaidaProdutos()
        {
            var qtd = await _context.SaidaProduto.Where(x => x.SPRO_ISDELETED == false).ToListAsync();
          
            return qtd.Select(x => x.SPRO_QUANTIDADE).Sum(); ;
        }

        
    }
}
