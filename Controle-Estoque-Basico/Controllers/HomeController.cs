using Controle_Estoque_Basico.Data;
using Controle_Estoque_Basico.Models;
using Controle_Estoque_Basico.ViewModels;
using Controle_Estoque_Basico.Visoes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Controle_Estoque_Basico.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration configuration;

        public HomeController(ILogger<HomeController> logger, AppDbContext context, IConfiguration config)
        {
            _context = context;
            _logger = logger;
            configuration = config;
        }

        public async Task<IActionResult> Index()
        {
            var vm = new HomeViewModel();

            await TotalProdutosAVencer(vm, "", "");
            return View(vm);
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

        public async Task<decimal> TotalProdutosAVencer(HomeViewModel model, string _dataDe, string _dataAte)
        {
            model.DataDe = new DateTime();
            model.DataAte = new DateTime();

            if (string.IsNullOrEmpty(_dataDe) && string.IsNullOrEmpty(_dataAte))
            {
                model.DataDe = DateTime.Now.AddMonths(-1);
                model.DataAte = DateTime.Now.AddMonths(1);
            }
            else
            {
                model.DataDe = Convert.ToDateTime(_dataDe);
                model.DataAte = Convert.ToDateTime(_dataAte);
            }


            return await _context.Produto.Where(x => x.PRO_ISDELETED == false
                                            && x.PRO_VALIDADE >= model.DataDe
                                            && x.PRO_VALIDADE <= model.DataAte).CountAsync();

        }

        public async Task<IActionResult> CarregaDadosGraficoDeLinhasComFiltro(string _dataDe, string _dataAte)
        {
            try
            {
                var _objeto = new List<GraficoProdutosAVencerDadosLista>();
                List<GraficoProdutosAVencerDados> list = new List<GraficoProdutosAVencerDados>();

                var dataDe = DateTime.Now;
                var dataAte = DateTime.Now;

                if (string.IsNullOrEmpty(_dataDe) && string.IsNullOrEmpty(_dataAte))
                {
                    //*VALOR CHUMBADO - FILTRO DE 1 MES PRA TRAZ E 1 MES PRA FRENTE.
                    dataDe = Convert.ToDateTime(dataDe).AddMonths(-1);
                    dataAte = Convert.ToDateTime(dataAte).AddMonths(1);
                }
                else
                {
                    //*VALOR FILTRADO
                    dataDe = Convert.ToDateTime(_dataDe);
                    dataAte = Convert.ToDateTime(_dataAte);
                }

                using var connection = new MySqlConnection(configuration.GetConnectionString("DefaultConnection"));

                await connection.OpenAsync();

                using var command = new MySqlCommand("SELECT date_format(PRO_VALIDADE, '%m-%Y') AS MES_ANO , SUM(PRO_QUANTIDADE) AS QUANTIDADE " +
                                                     $"FROM PRODUTO WHERE PRO_ISDELETED = FALSE AND PRO_VALIDADE BETWEEN '{dataDe.ToString("yyyy-MM-dd")}' " +
                                                     $"AND '{dataAte.ToString("yyyy-MM-dd")}' GROUP BY MONTH(PRO_VALIDADE) ORDER BY PRO_VALIDADE ASC", connection);

                using var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    list.Add(new GraficoProdutosAVencerDados()
                    {
                        mes = reader.GetString("MES_ANO"),
                        valor = reader.GetDecimal("QUANTIDADE"),
                    });

                }

                List<string> mesesStr = new List<string>();
                List<decimal> valoresDecimal = new List<decimal>();

                foreach (var item in list)
                {
                    mesesStr.Add(item.mes);
                    valoresDecimal.Add(item.valor);
                }

                _objeto = new List<GraficoProdutosAVencerDadosLista>{
                new GraficoProdutosAVencerDadosLista {
                    meses = mesesStr,
                    valores = valoresDecimal,
                },
            };

                return Json(_objeto, new System.Text.Json.JsonSerializerOptions());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public static IEnumerable<(string Month, int Year)> MesesIntervalo(DateTime startDate, DateTime endDate)
        {
            DateTime iterator;
            DateTime limit;

            if (endDate > startDate)
            {
                iterator = new DateTime(startDate.Year, startDate.Month, 1);
                limit = endDate;
            }
            else
            {
                iterator = new DateTime(endDate.Year, endDate.Month, 1);
                limit = startDate;
            }

            var dateTimeFormat = CultureInfo.CurrentCulture.DateTimeFormat;
            while (iterator <= limit)
            {
                yield return (
                    dateTimeFormat.GetAbbreviatedMonthName(iterator.Month), iterator.Year
                );

                iterator = iterator.AddMonths(1);
            }
        }       
    }
}
