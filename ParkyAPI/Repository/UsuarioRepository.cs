using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ParkyAPI.Datos;
using ParkyAPI.Modelos;
using ParkyAPI.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ParkyAPI.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly AppSettings _appSettings;  

        public UsuarioRepository(ApplicationDbContext db, IOptions<AppSettings> appsettings)
        {
            _db = db;
            _appSettings = appsettings.Value;
        }

        public Usuario Autentificar(string nombreUsuario, string contraseña)
        {
            var usuario = _db.usuarios.SingleOrDefault(x => x.Nombre == nombreUsuario && x.contraseña == contraseña);

            //usuario no encontrado
            if (usuario == null)
            {
                return null;
            }

            //Si se encontró el usuario generar token JWT
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.secreto);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, usuario.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials
                                    (new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            usuario.Token = tokenHandler.WriteToken(token);
            usuario.contraseña = "";
            return usuario;
        }

        public bool EsUnicoUsuario(string nombreUsuario)
        {
            var usuario = _db.usuarios.SingleOrDefault(u => u.Nombre == nombreUsuario);

            //El usuario no se encuentra
            if (usuario == null)
                return true;

            return false;
        }
        public Usuario Registro(string nombreUsuario, string contraseña)
        {
            var usuarioObj = new Usuario()
            {
                Nombre = nombreUsuario,
                contraseña = contraseña
            };

            _db.usuarios.Add(usuarioObj);
            _db.SaveChanges();
            
            usuarioObj.contraseña = "";
            return usuarioObj;
        }
    }
}
