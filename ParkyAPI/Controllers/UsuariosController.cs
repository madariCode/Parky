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
            if (usuario == null)
            {
                return BadRequest(new { message = "El usuario o la contraseña son incorrectos!" });
            }
            return Ok(usuario);
        }

        [AllowAnonymous]
        [HttpPost("registro")]
        public IActionResult Registro([FromBody] Usuario modelo)
        {
            bool siNombreUsuarioUnico = _uRepo.EsUnicoUsuario(modelo.Nombre);
            if (!siNombreUsuarioUnico)
            {
                return BadRequest(new { message = "El nombre de usuario ya existe" });
            }
            var usuario = _uRepo.Registro(modelo.Nombre, modelo.contraseña);

            if(usuario == null)
            {
                return BadRequest(new { message = "Error durante el registro" });
            }

            return Ok();
        }
    }
}
