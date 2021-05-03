using Microsoft.EntityFrameworkCore;
using ParkyAPI.Datos;
using ParkyAPI.Modelos;
using ParkyAPI.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyAPI.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _db;
        public UsuarioRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public Usuario Autentificar(string nombreUsuario, string contraseña)
        {
            throw new NotImplementedException();
        }

        public bool EsUnicoUsuario(string nombreUsuario)
        {
            throw new NotImplementedException();
        }

        public Usuario Registro(string nombreUsuario, string contraseña)
        {
            throw new NotImplementedException();
        }
    }
}
