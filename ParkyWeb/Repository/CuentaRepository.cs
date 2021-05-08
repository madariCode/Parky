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
    public class CuentaRepository : Repository<Usuario>, ICuentaRepository
    {
        private readonly IHttpClientFactory _clientFactory;

        public CuentaRepository(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<Usuario> LoginAsync(string url, Usuario objToCreate)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url);

            if (objToCreate != null)
            {
                request.Content = new StringContent(
                    JsonConvert.SerializeObject(objToCreate), Encoding.UTF8, "application/json");
            }
            else
            {
                return new Usuario();
            }

            var client = _clientFactory.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Usuario>(jsonString);
            }
            else
            {
                return new Usuario();
            }
        }

        public async Task<bool> RegistroAsync(string url, Usuario objToCreate)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url);

            if (objToCreate != null)
            {
                request.Content = new StringContent(
                    JsonConvert.SerializeObject(objToCreate), Encoding.UTF8, "application/json");
            }
            else
            {
                return false;
            }

            var client = _clientFactory.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;    
            }
            else
            {
                return false;
            }
        }
    }
}
