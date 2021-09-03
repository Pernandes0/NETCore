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
        
        /*private static string GetRandomSalt()
        {
            return BCrypt.Net.BCrypt.GenerateSalt(12);
        }

        private static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, GetRandomSalt());
        }

        private static bool ValidatePassword(string password, string hashPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashPassword);
        }
        */
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
        [HttpPut("ChangePasswor")]
        public ActionResult ChangePassword(ChangePasswordVM changePasswordVM)
        {
            string email = accountRepository.CheckLoginEmail(changePasswordVM.Email);
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest(new
                {
                    statusCode = HttpStatusCode.BadRequest,
                    message = "Data tidak ada di database"
                });
            }
            else accountRepository.CheckLoginPassword(email, changePasswordVM.Password);
            //else ValidatePassword(changePasswordVM.Password, changePasswordVM.NewPassword);
            {
                //string saltPass = BCrypt.Net.BCrypt.GenerateSalt(12);
                Account account = new Account();
                account.NIK = email;
                //account.Password = BCrypt.Net.BCrypt.HashPassword(changePasswordVM.NewPassword, saltPass);
                account.Password = (changePasswordVM.NewPassword);
                Update(account);
                return Ok(new
                {
                    statusCode = HttpStatusCode.OK,
                    massage = "Password anda berhasl diganti"
                });
            }
        }
            
    }
}
