using ImplementCors.Base.Controller_;
using ImplementCors.Models;
using ImplementCors.Repository.Data;
using Microsoft.AspNetCore.Mvc;
using NETCore.Models;
using NETCore.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ImplementCors.Controllers
{
    [Route("[controller]")]
    public class PersonController : BaseController<Person, PersonRepository, string>
    {
        private readonly PersonRepository repository;
        readonly HttpClient http = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:44384/api/")
        };
        public PersonController(PersonRepository repository) : base(repository)
        {
            this.repository = repository;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("GetPersonVM")]
        //[HttpGet]
        public async Task<JsonResult> GetPersonVM()
        {
            var result = await repository.GetPersonVMs();
            return Json(result);
        }
        [HttpGet("GetNIK/{NIK}")]
        //[HttpGet]
        public async Task<JsonResult> GetNIK(string NIK)
        {
            var result = await repository.GetPersonVMsNIK(NIK);
            return Json(result);
        }
        [HttpPost("InsertPerson")]
        public IActionResult InsertPerson(GetPersonVM person)
        {
            try
            {
                var conv = JsonConvert.SerializeObject(person);
                var buffer = System.Text.Encoding.UTF8.GetBytes(conv);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var result = http.PostAsync("Person/InsertPerson", byteContent).Result;
                return Json(result);
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
