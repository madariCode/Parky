using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ParkyWeb.Models;
using ParkyWeb.Models.ViewModels;
using ParkyWeb.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IParqueNacionalRepository _pnRepo;
        private readonly ISenderoRepository _sRepo;

        public HomeController(ILogger<HomeController> logger, IParqueNacionalRepository pnRepo, ISenderoRepository sRepo)
        {
            _logger = logger;
            _pnRepo = pnRepo;
            _sRepo = sRepo;
        }


        public async Task<IActionResult> Index()
        {
            IndexVM lista = new IndexVM()
            {
                ParqueNacionalLista = await _pnRepo.GetAllAsync(SD.ParquesNacionalesAPIPath),
                SenderoLista = await _sRepo.GetAllAsync(SD.SenderosAPIPath)
            };

            return View(lista);
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
    }
}
