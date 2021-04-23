using ParkyAPI.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyAPI.Repository.IRepository
{
    public interface ISenderoRepository
    {
        ICollection<Sendero> GetSenderos();
        ICollection<Sendero> GetSenderosEnParqueNacional(int pnId);
        Sendero GetSendero(int senderoID);
        bool SenderoExiste(string nombre);
        bool SenderoExiste(int id);
        bool CrearSendero(Sendero sendero);
        bool UpdateSendero(Sendero sendero);
        bool DeleteSendero(Sendero sendero);
        bool Guardar();
    }
}
