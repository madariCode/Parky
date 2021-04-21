using Microsoft.EntityFrameworkCore;
using ParkyAPI.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyAPI.Datos
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opciones) : base (opciones)
        {

        }

        public DbSet<ParqueNacional> parquesNacionales { get; set; }
    }
}
