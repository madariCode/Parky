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
    public class SenderoRepository : Repository<Sendero>, ISenderoRepository
    {
        private readonly IHttpClientFactory _clientFactory;

        public SenderoRepository(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }
    }
}
