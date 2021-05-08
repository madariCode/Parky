using ParkyWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyWeb.Repository.IRepository
{
    public interface ICuentaRepository : IRepository<Usuario>
    {
        Task<Usuario> LoginAsync(string url, Usuario objToCreate);
        Task<bool> RegistroAsync(string url, Usuario objToCreate);
    }
}
