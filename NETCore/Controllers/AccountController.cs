using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NETCore.Base;
using NETCore.Context;
using NETCore.Models;
using NETCore.Repository.Data;
using NETCore.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace NETCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController<Account, AccountRepository, string>
    {
        private readonly AccountRepository accountRepository;
        private readonly MyContext myContext;
        public AccountController(AccountRepository repository, MyContext myContext) : base(repository)
        {
            this.accountRepository = repository;
            this.myContext = myContext;
        }
        [HttpPost("Login")]
        public ActionResult Login(LoginVM loginVM)
        {
            string NIK = accountRepository.CheckLoginEmail(loginVM.Email);
            if (string.IsNullOrEmpty(NIK))
            {
                return BadRequest(new
                {
                    statusCode = HttpStatusCode.BadRequest,
                    message = "Data tidak ada di database"
                });
            }
            else if (accountRepository.CheckLoginPassword(NIK, loginVM.Password))
            {
                return Ok(new
                {
                    statusCode = HttpStatusCode.OK,
                    message = "Berhasil Login"
                });
            }
            else
            {
                return Ok(new
                {
                    statusCode = HttpStatusCode.OK,
                    message = "Password Salah"
                });
            }
        }
        [HttpPost("ForgetPassword")]
        public ActionResult ForgetPassword(ForgetPasswordVM forgetPasswordVM)
        {
            
            accountRepository.ForgetPassword(forgetPasswordVM);
            return Ok(new
            {
                statusCode = HttpStatusCode.OK,
                message = "Password berhasil dikirim melalui email"
            });
        }
            
    }
}
