using Microsoft.EntityFrameworkCore;
using ParkyAPI.Datos;
using ParkyAPI.Modelos;
using ParkyAPI.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyAPI.Repository
{
    public class SenderoRepository : ISenderoRepository
    {
        private readonly ApplicationDbContext _db;

        public SenderoRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool CrearSendero(Sendero sendero)
        {
            _db.senderos.Add(sendero);
            return Guardar();
        }

        public bool DeleteSendero(Sendero sendero)
        {
            _db.senderos.Remove(sendero);
            return Guardar();
        }

        public Sendero GetSendero(int senderoID)
        {
            return _db.senderos.Include(c => c.ParqueNacional).FirstOrDefault(a => a.Id == senderoID);
        }

        public ICollection<Sendero> GetSenderos()
        {
            return _db.senderos.Include(c => c.ParqueNacional).OrderBy(a => a.Nombre).ToList();
        }

        public ICollection<Sendero> GetSenderosEnParqueNacional(int pnId)
        {
            return _db.senderos.Include(c => c.ParqueNacional).Where(c => c.ParqueNacionalId == pnId).ToList();
        }

        public bool Guardar()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool SenderoExiste(string nombre)
        {
            bool value = _db.senderos.Any(a => a.Nombre.ToLower().Trim() == nombre.ToLower().Trim());
            return value;
        }

        public bool SenderoExiste(int id)
        {
            return _db.senderos.Any(a => a.Id == id);
        }

        public bool UpdateSendero(Sendero sendero)
        {
            _db.senderos.Update(sendero);
            return Guardar();
        }
    }
}
