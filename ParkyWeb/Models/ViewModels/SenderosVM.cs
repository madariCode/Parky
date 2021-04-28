using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyWeb.Models.ViewModels
{
    public class SenderosVM
    {
        public IEnumerable<SelectListItem> ParqueNacionalLista { get; set; }
        public Sendero Sendero { get; set; }
    }
}
