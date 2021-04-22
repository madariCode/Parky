using ParkyAPI.Datos;
using ParkyAPI.Modelos;
using ParkyAPI.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyAPI.Repository
{
    public class ParqueNacionalRepository : IParqueNacionalRepository
    {
        private readonly ApplicationDbContext _db;

        public ParqueNacionalRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool CrearParqueNacional(ParqueNacional parqueNacional)
        {
            _db.parquesNacionales.Add(parqueNacional);
            return Guardar();
        }

        public bool DeleteParqueNacional(ParqueNacional parqueNacional)
        {
            _db.parquesNacionales.Remove(parqueNacional);
            return Guardar();
        }

        public ParqueNacional GetParqueNacional(int parqueNacionalID)
        {
            return _db.parquesNacionales.FirstOrDefault(a => a.Id == parqueNacionalID);
        }

        public ICollection<ParqueNacional> GetParqueNacionales()
        {
            return _db.parquesNacionales.OrderBy(a => a.Nombre).ToList();
        }

        public bool Guardar()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool ParqueNacionalExiste(string nombre)
        {
            bool value = _db.parquesNacionales.Any(a => a.Nombre.ToLower().Trim() == nombre.ToLower().Trim());
            return value;
        }

        public bool ParqueNacionalExiste(int id)
        {
            return _db.parquesNacionales.Any(a => a.Id == id);
        }

        public bool UpdateParqueNacional(ParqueNacional parqueNacional)
        {
            _db.parquesNacionales.Update(parqueNacional);
            return Guardar();
        }
    }
}
