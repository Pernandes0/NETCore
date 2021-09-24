using Microsoft.AspNetCore.Cors;
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
    [EnableCors("AllowAllOrigins")]
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
        public ActionResult<Entity> Get()
        {
            var data = repository.Get();
            if (data.Count() == 0)
            {
                return BadRequest(new
                {
                    statusCode = HttpStatusCode.BadRequest,
                    message = "Data tidak ada"
                });
            }
            return Ok(new 
            { 
                statusCode = HttpStatusCode.OK, 
                data, 
                massage = "Data ditampilkan"
            });
        }
        [HttpGet("{key}")]
        public ActionResult Get(Key key)
        {
            var data = repository.Get(key);
            if (data == null)
            {
                return BadRequest(new
                {
                    stausCode = HttpStatusCode.BadRequest,
                    message = "Salah Input"
                });
            }
            return Ok(new
            {
                statusCode = HttpStatusCode.OK,
                data,
                message = "Data ditemukan"
            });
        }
        [HttpPut]
        public ActionResult Update(Entity entity)
        {
            var data = repository.Update(entity);
            try
            {
                if (data != 0)
                {
                    return Ok(new 
                    {
                        statusCode = HttpStatusCode.OK,
                        data,
                        message = "Data Terupdate"
                    });
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR : " + e.Message);
            }
            return BadRequest(new
            {
                statusCode = HttpStatusCode.BadRequest,
                message = "Data tidak ditemukan"
            });
        }
        //[EnableCors("AllowAllOrigins")]
        [HttpDelete("{key}")]
        public ActionResult Delete(Key key)
        {
            var data = repository.Delete(key);
            if (data == 0)
            {
                return BadRequest(new 
                { 
                    statusCode = HttpStatusCode.BadRequest, 
                    message = "Data gagal dihapus atau tidak ada data" 
                });
            }
            return Ok(new 
            { 
              statusCode = HttpStatusCode.OK, 
              message = "Data Berhasil dihapus" 
            });
        }
    }
}
