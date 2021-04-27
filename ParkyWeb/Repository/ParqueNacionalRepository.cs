using Newtonsoft.Json;
using ParkyWeb.Models;
using ParkyWeb.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ParkyWeb.Repository
{
    public class ParqueNacionalRepository : Repository<ParqueNacional>, IParqueNacionalRepository
    {
        private readonly IHttpClientFactory _clientFactory;

        public ParqueNacionalRepository(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }
    }
}
