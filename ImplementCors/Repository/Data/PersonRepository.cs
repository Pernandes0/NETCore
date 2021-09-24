using ImplementCors.Base.urls;
using Microsoft.AspNetCore.Http;
using NETCore.Models;
using NETCore.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ImplementCors.Repository.Data
{
    public class PersonRepository : GeneralRepository<Person, string>
    {
        private readonly Addres addres;
        private readonly HttpClient httpClient;
        private readonly string request;
        private readonly IHttpContextAccessor _contextAccessor;

        public PersonRepository(Addres addres, string request = "Person/" ) : base(addres, request)
        {
            this.addres = addres;
            this.request = request;
            _contextAccessor = new HttpContextAccessor();
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(addres.link)
            };
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", _contextAccessor.HttpContext.Session.GetString("JWToken"));
        }
        public async Task<List<GetPersonVM>> GetPersonVMs()
        {
            List<GetPersonVM> registers = new List<GetPersonVM>();
            using (var respone = await httpClient.GetAsync(request + "GetPersonVM"))
            {
                string apiRespone = await respone.Content.ReadAsStringAsync();
                registers = JsonConvert.DeserializeObject<List<GetPersonVM>>(apiRespone);
            }
            return registers;
        }
        public async Task<List<GetPersonVM>> GetPersonVMsNIK(string NIK)
        {
            List<GetPersonVM> register = new List<GetPersonVM>();
            using (var respone = await httpClient.GetAsync(request + "GetNIK/" + NIK))
            {
                string apiResponse = await respone.Content.ReadAsStringAsync();
                register = JsonConvert.DeserializeObject<List<GetPersonVM>>(apiResponse);
            }
            return register;
        }
    }
}
