using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParkyAPI.Modelos;
using ParkyAPI.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyAPI.Controllers
{
    [Authorize]
    [Route("api/v{version:apiVersion}/usuarios")]
    [ApiController]
    public class UsuariosController : Controller
    {
        private readonly IUsuarioRepository _uRepo;

        public UsuariosController(IUsuarioRepository uRepo)
        {
            _uRepo = uRepo;
        }

        [AllowAnonymous]
        [HttpPost("autentificar")]
        public IActionResult Autentificar([FromBody] Usuario modelo)
        {
            var usuario = _uRepo.Autentificar(modelo.Nombre, modelo.contraseña);
            if(usuario == null)
            {
                return BadRequest(new { message = "El usuario o la contraseña son incorrectos!" });
            }
            return Ok(usuario);
        }
    }
}
