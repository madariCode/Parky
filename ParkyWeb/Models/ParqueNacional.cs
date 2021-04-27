using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyWeb.Models
{
    public class ParqueNacional
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        [Display(Name ="Comunidad Autonoma")]
        public string ComunidadAutonoma { get; set; }
        public byte[] Imagen { get; set; }
        public DateTime Creado { get; set; }
        public DateTime Establecido { get; set; }
    }
}
