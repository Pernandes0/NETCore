using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NETCore.Models;
using NETCore.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace NETCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OldController : ControllerBase
    {
        private readonly OldRepository personRepository;
        public OldController(OldRepository personRepository)
        {
            this.personRepository = personRepository;
        }
        [HttpPost]
        public ActionResult Insert(Person person)
        {
            try
            {
                personRepository.Insert(person);
                return Ok(new
                {

                    statusCode = HttpStatusCode.OK,
                    message = "Data berhasil di Insert"
                });
            }
            catch (Exception)
            {
                return BadRequest(new {
                    statusCode = HttpStatusCode.BadRequest,
                    message = "Input data salah" });
            }
        }
        [HttpGet]

        public ActionResult Get()
        {
            try
            {
                return Ok(new
                {
                    statusCode = HttpStatusCode.OK,
                    data = personRepository.Get(),
                    message = "Data Ditemukan"
                });
            }
            catch
            {
                return BadRequest(new
                {
                    statusCode = HttpStatusCode.BadRequest,
                    message = "Data Tidak Ditemukan"
                });
            }
            // return Ok(personRepository.Get());
        }
        [HttpGet("{NIK}")]

        public ActionResult Get(string NIK)
        {
            try
            {
                return Ok(new
                {
                    statusCode = HttpStatusCode.OK,
                    data = personRepository.Get(NIK),
                    message = "Data Ditemukan"
                });
                // return Ok(personRepository.Get(NIK));
            }
            catch
            {
                return BadRequest(new { 
                statusCode = HttpStatusCode.BadRequest,
                massage = "Data Tidak Ditemukan"});
            }
           // return Ok(personRepository.Get(NIK));
        }
        [HttpPut]
        public ActionResult Update(Person persons)
        {
            try
            { 
                // personRepository.Update(persons);
                return Ok(new { 
                statusCode = HttpStatusCode.OK,
                data = personRepository.Update(persons),
                message = "Data Berhasl di Update"
                });
            }
            catch
            {
                return BadRequest("Tidak Ada data yang di update");
            }
        }
        [HttpDelete("{NIK}")]

        public ActionResult Delete(string NIK)
        {
            try
            {
              //  personRepository.Delete(NIK);
                return Ok(new { 
                statusCode = HttpStatusCode.OK,
                data = personRepository.Delete(NIK),
                message = "Data berhasil dihapus"});
            }
            catch
            {
                return BadRequest(new { 
                statusCode = HttpStatusCode.BadRequest,
                message = "NIK tidak ditemukan"});
            }
        }
    }
}
