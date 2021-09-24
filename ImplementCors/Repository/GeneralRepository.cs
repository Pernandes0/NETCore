using ImplementCors.Base.urls;
using ImplementCors.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ImplementCors.Repository
{
    public class GeneralRepository<Entity, Key> : IRepository<Entity, Key>
        where Entity : class
    {
        private readonly Addres addres;
        private readonly string request;
        //private readonly IHttpContextAccessor httpContextAccessor;
        private readonly HttpClient httpClient;
        public GeneralRepository(Addres addres, string request)
        {
            this.addres = addres;
            this.request = request;
            //_contextAccessor = new HttpContextAccessor();
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(addres.link)
            };
        }
        public HttpStatusCode Delete(Key key)
        {
            var result = httpClient.DeleteAsync(request + key).Result;
            return result.StatusCode;
            //throw new NotImplementedException();
        }

        public async Task<List<Entity>> Get()
        {
            List<Entity> entities = new List<Entity>();

            using (var response = await httpClient.GetAsync(request))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<Entity>>(apiResponse);
            }
            return entities;
        }

        public async Task<Entity> Get(Key key)
        {
            Entity entity = null;

            using (var response = await httpClient.GetAsync(request + key))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entity = JsonConvert.DeserializeObject<Entity>(apiResponse);
            }
            return entity;
            //throw new NotImplementedException();
        }

        public HttpStatusCode Post(Entity entity)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            var result = httpClient.PostAsync(addres.link + request, content).Result;
            return result.StatusCode;
            //throw new NotImplementedException();
        }

        public HttpStatusCode Put(Key key, Entity entity)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            var result = httpClient.PutAsync(request + key, content).Result;
            return result.StatusCode;
            //throw new NotImplementedException();
        }
    }
}
