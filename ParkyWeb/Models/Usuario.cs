using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyWeb.Models
{
    public class Usuario
    {
        public string Nombre { get; set; }
        public string contraseña { get; set; }
        public string Rol { get; set; }        
        public string Token { get; set; }
    }
}
