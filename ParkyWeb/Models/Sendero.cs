using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyWeb.Models
{
    public class Sendero
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public double Distancia { get; set; }
        public enum TipoDificultad { Facil, Moderado, Dificil, Experto }
        public TipoDificultad Dificultad { get; set; }
        [Required]
        public int ParqueNacionalId { get; set; }
        public ParqueNacional ParqueNacionalDto { get; set; }
    }
}
