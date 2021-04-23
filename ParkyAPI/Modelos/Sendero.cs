using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyAPI.Modelos
{
    public class Sendero
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public double Distancia { get; set; }
        public enum TipoDificultad { Facil, Moderado, Dificil, Experto }
        public TipoDificultad Dificultad { get; set; }
        [Required]
        public int ParqueNacionalId { get; set; }
        [ForeignKey("ParqueNacionalId")]
        public ParqueNacional ParqueNacional { get; set; }
        public DateTime Creado { get; set; }
    }
}
