using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NETCore.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace NETCore.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<Entity, Repository, Key> : ControllerBase
        where Entity : class
        where Repository : IRepository<Entity, Key>
    {
        private readonly Repository repository;

        public BaseController(Repository repository)
        {
            this.repository = repository;
        }
        [HttpPost]
        public ActionResult Insert(Entity entity)
        {
            try
            {
                repository.Insert(entity);
                return Ok(new
                {

                    statusCode = HttpStatusCode.OK,
                    message = "Data berhasil di Insert"
                });
            }
            catch (Exception)
            {
                return BadRequest(new
                {
                    statusCode = HttpStatusCode.BadRequest,
                    message = "Input data salah"
                });
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
                    data = repository.Get(),
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
        [HttpGet("{key}")]

        public ActionResult Get(Key key)
        {
            try
            {
                return Ok(new
                {
                    statusCode = HttpStatusCode.OK,
                    data = repository.Get(key),
                    message = "Data Ditemukan"
                });
                // return Ok(personRepository.Get(NIK));
            }
            catch
            {
                return BadRequest(new
                {
                    statusCode = HttpStatusCode.BadRequest,
                    massage = "Data Tidak Ditemukan"
                });
            }
            // return Ok(personRepository.Get(NIK));
        }
        [HttpPut]
        public ActionResult Update(Entity entity)
        {
            try
            {
                // personRepository.Update(persons);
                return Ok(new
                {
                    statusCode = HttpStatusCode.OK,
                    data = repository.Update(entity),
                    message = "Data Berhasl di Update"
                });
            }
            catch
            {
                return BadRequest("Tidak Ada data yang di update");
            }
        }
        [HttpDelete("{key}")]

        public ActionResult Delete(Key key)
        {
            try
            {
                //  personRepository.Delete(NIK);
                return Ok(new
                {
                    statusCode = HttpStatusCode.OK,
                    data = repository.Delete(key),
                    message = "Data berhasil dihapus"
                });
            }
            catch
            {
                return BadRequest(new
                {
                    statusCode = HttpStatusCode.BadRequest,
                    message = "NIK tidak ditemukan"
                });
            }
        }
    }
}
