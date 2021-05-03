using ParkyAPI.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyAPI.Repository.IRepository
{
    public interface IUsuarioRepository
    {
        bool EsUnicoUsuario(string nombreUsuario);
        Usuario Autentificar(string nombreUsuario, string contraseña);
        Usuario Registro(string nombreUsuario, string contraseña);
    }
}
