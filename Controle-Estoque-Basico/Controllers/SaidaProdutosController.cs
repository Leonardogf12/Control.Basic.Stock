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
        private readonly IProdutosRepositorio _repPro;

        public SaidaProdutosController(AppDbContext context, ISaidaProdutosRepositorio repSpro, IProdutosRepositorio repPro)
        {
            _context = context;
            _repSpro = repSpro;
            _repPro = repPro;
        }


        public async Task<IActionResult> Index()
        {
            var t = await _context.SaidaProduto.Include(x => x.Produto).Include(x => x.Categoria).Where(x => x.SPRO_ISDELETED == false).ToListAsync();
            return View(t);
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
                    var saidaProduto = await _context.SaidaProduto.FindAsync(aRegistros[i]);

                    saidaProduto.SPRO_ISDELETED = true;

                    await _context.SaveChangesAsync();
                }

                return PartialView("ListaSaidaProdutosPartial", await _context.SaidaProduto.Include(x => x.Produto).Include(x => x.Categoria).Where(x => x.SPRO_ISDELETED == false).ToListAsync());
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                return Json(ex.Message);
            }
        }

        public async Task<IActionResult> DesfazerVenda(string _registros)
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
                    var saidaProduto = await _context.SaidaProduto.FindAsync(aRegistros[i]);

                    Produto produto = new Produto();
                    produto = await _context.Produto.Where(x => x.PRO_ID == saidaProduto.SPRO_IDPRODUTO && x.PRO_ISDELETED == false).FirstOrDefaultAsync();
                    produto.PRO_QUANTIDADE += saidaProduto.SPRO_QUANTIDADE;
                    await _repPro.Salvar(produto);

                    _context.SaidaProduto.Remove(saidaProduto);
                    await _context.SaveChangesAsync();
                    
                }

                return PartialView("ListaSaidaProdutosPartial", await _context.SaidaProduto.Include(x => x.Produto).Include(x => x.Categoria).Where(x => x.SPRO_ISDELETED == false).ToListAsync());
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                return Json(ex.Message);
            }
        }

        public async Task<decimal> TotalSaidaProdutos()
        {
            var qtd = await _context.SaidaProduto.Where(x => x.SPRO_ISDELETED == false).ToListAsync();

            return qtd.Count() > 0 ? qtd.Select(x => x.SPRO_QUANTIDADE).Sum() : 0;

        }


    }
}
