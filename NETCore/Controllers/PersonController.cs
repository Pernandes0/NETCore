using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NETCore.Base;
using NETCore.Repository.Data;
using NETCore.Models;
using System.Net;
using NETCore.ViewModel;
using NETCore.Context;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;

namespace NETCore.Controllers
{
    [EnableCors("AllowAllOrigins")]
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : BaseController<Person, PersonRepository, string>
    {
        private readonly PersonRepository personRepository;
        private readonly MyContext myContext;
        public PersonController(PersonRepository repository, MyContext myContext) : base(repository)
        {
            this.personRepository = repository;
            this.myContext = myContext;
        }
        //[Authorize(Roles = "User")]
        //[EnableCors("AllowAllOrigins")]
        [HttpPost("InsertPerson")]
        public ActionResult InsertPerson(GetPersonVM getPersonVM)
        {
            var checkEmail = myContext.Persons.Where(p => p.Email.Equals(getPersonVM.Email));
            var checkNIK = myContext.Persons.Where(p => p.NIK.Equals(getPersonVM.NIK));
            var checkPhone = myContext.Persons.Where(p => p.Phone.Equals(getPersonVM.Phone));
            if (checkEmail.Count() == 0 && checkNIK.Count() == 0 && checkPhone.Count() == 0)
            {
                var gerPerson_ = personRepository.InsertPerson(getPersonVM);
                return Ok(new
                {
                    statusCode = HttpStatusCode.OK,
                    data = getPersonVM,
                    message = "Data berhasil dimasukan"
                });
            }
            else if (checkEmail.Count() > 0)
            {
                return BadRequest(new
                {
                    statusCode = HttpStatusCode.BadRequest,
                    message = "Email sudah digunakan"
                });
            }
            else if (checkNIK.Count() > 0)
            {
                return BadRequest(new
                {
                    statusCode = HttpStatusCode.BadRequest,
                    message = "NIK sudah digunakan"
                });
            }
            else if (checkPhone.Count() > 0)
            {
                return BadRequest(new
                {
                    statusCode = HttpStatusCode.BadRequest,
                    message = "Nomor handphone sudah digunakan"
                });
            }
            else
            {
                return BadRequest(new
                {
                statucCode = HttpStatusCode.BadRequest,
                message = "Data gagal dimasukan"
                });     
            }
        }
        // [Authorize(Roles = "Manager")]
        //[EnableCors("AllowOrigin")]
        [HttpGet("GetPersonVM")]
        public ActionResult GetPersonVM()
        {
            var getPerson = personRepository.GetPersonVMs();
            if (getPerson != null)
            {
                /*return Ok(new
                {
                    status = HttpStatusCode.OK,
                    data = getPerson,
                    message = "Data berhasil di tampilkan"
                });*/
                return Ok(getPerson);
            }
            else
            {
                return BadRequest(new
                {
                    status = HttpStatusCode.BadRequest,
                    massage = "Data tidak ditemukan"
                });
            }
        }
        [HttpGet("GetNIK/{NIK}")]
        public ActionResult GetNIK(string NIK)
        {
            var person = personRepository.GetPersonVMsNIK(NIK);
            if (person == null)
            {
                return BadRequest(new 
                {
                    statusCode = HttpStatusCode.BadRequest,
                    message = "Tidak ada data"
                });
            }
            else
            {
                /*return Ok(new 
                {
                    statusCode = HttpStatusCode.BadRequest,
                    data = person,
                    message = "Data berhasil ditampilkan"
                });*/
                return Ok(person);
            }
        }
    }
}
