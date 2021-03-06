using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyAPI.Modelos.Dtos
{
    public class ParqueNacionalDto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string ComunidadAutonoma { get; set; }
        public byte[] Imagen { get; set; }
        public DateTime Creado { get; set; }
        public DateTime Establecido { get; set; }
    }
}
