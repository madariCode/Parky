using ParkyAPI.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyAPI.Repository.IRepository
{
    public interface IParqueNacionalRepository
    {
        ICollection<ParqueNacional> GetParqueNacionales();
        ParqueNacional GetParqueNacional(int parqueNacionalID);
        bool ParqueNacionalExiste(string nombre);
        bool ParqueNacionalExiste(int id);
        bool CrearParqueNacional(ParqueNacional parqueNacional);
        bool UpdateParqueNacional(ParqueNacional parqueNacional);
        bool DeleteParqueNacional(ParqueNacional parqueNacional);
        bool Guardar();
    }
}
