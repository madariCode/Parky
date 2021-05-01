using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyWeb.Models.ViewModels
{
    public class IndexVM
    {
        public IEnumerable<ParqueNacional> ParqueNacionalLista { get; set; }
        public IEnumerable<Sendero> SenderoLista { get; set; }
    }
}
