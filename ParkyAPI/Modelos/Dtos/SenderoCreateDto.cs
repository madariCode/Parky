using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using static ParkyAPI.Modelos.Sendero;

namespace ParkyAPI.Modelos.Dtos
{
    public class SenderoCreateDto
    {
        [Required]
        public string Nombre { get; set; }
        [Required]
        public double Distancia { get; set; }
        public TipoDificultad Dificultad { get; set; }
        [Required]
        public int ParqueNacionalId { get; set; }
    }
}
