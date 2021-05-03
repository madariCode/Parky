using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyAPI.Modelos
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string contraseña { get; set; }
        public string Rol { get; set; }
        [NotMapped]
        public string Token { get; set; }
    }
}
