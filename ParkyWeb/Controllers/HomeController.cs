using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ParkyWeb.Models;
using ParkyWeb.Models.ViewModels;
using ParkyWeb.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ParkyWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IParqueNacionalRepository _pnRepo;
        private readonly ICuentaRepository _cuentaRepo;
        private readonly ISenderoRepository _sRepo;

        public HomeController(ILogger<HomeController> logger, IParqueNacionalRepository pnRepo, ISenderoRepository sRepo, ICuentaRepository cuentaRepo)
        {
            _logger = logger;
            _pnRepo = pnRepo;
            _sRepo = sRepo;
            _cuentaRepo = cuentaRepo;
        }


        public async Task<IActionResult> Index()
        {
            IndexVM lista = new IndexVM()
            {
                ParqueNacionalLista = await _pnRepo.GetAllAsync(SD.ParquesNacionalesAPIPath, HttpContext.Session.GetString("JWToken")),
                SenderoLista = await _sRepo.GetAllAsync(SD.SenderosAPIPath, HttpContext.Session.GetString("JWToken"))
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

        [HttpGet]
        public IActionResult Login()
        {
            Usuario obj = new Usuario();
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Usuario obj)
        {
            Usuario objUsuario = await _cuentaRepo.LoginAsync(SD.CuentaAPIPath + "autentificar/", obj);
            if(objUsuario.Token == null)
            {
                return View();
            }

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(ClaimTypes.Name, objUsuario.Nombre));
            identity.AddClaim(new Claim(ClaimTypes.Role, objUsuario.Rol));
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            HttpContext.Session.SetString("JWToken", objUsuario.Token);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Registro()
        {            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registro(Usuario obj)
        {
            bool resultado = await _cuentaRepo.RegistroAsync(SD.CuentaAPIPath + "registro/", obj);
            if (resultado == false)
            {
                return View();
            }

            return RedirectToAction("Login");
        }
       
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            HttpContext.Session.SetString("JWToken", "");
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult AccesoDenegado()
        {            
            return View();
        }
    }
}
