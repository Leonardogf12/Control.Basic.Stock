using Controle_Estoque_Basico.Data;
using Controle_Estoque_Basico.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Controle_Estoque_Basico.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            await TotalProdutosAVencer("", "");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<decimal> TotalProdutosAVencer(string _dataDe, string _dataAte)
        {
            DateTime dataDe = new DateTime();
            DateTime dataAte = new DateTime();

            if (string.IsNullOrEmpty(_dataDe) && string.IsNullOrEmpty(_dataAte))
            {
                dataDe = DateTime.Now.AddMonths(-1);
                dataAte = DateTime.Now.AddMonths(1);
            }
            else
            {
                dataDe = Convert.ToDateTime(_dataDe);
                dataAte = Convert.ToDateTime(_dataAte);
            }


            return await _context.Produto.Where(x => x.PRO_ISDELETED == false
                                            && x.PRO_VALIDADE >= dataDe
                                            && x.PRO_VALIDADE <= dataAte).CountAsync();
        }
    }
}
