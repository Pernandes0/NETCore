using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NETCore.Base;
using NETCore.Context;
using NETCore.Models;
using NETCore.Repository.Data;
using NETCore.ViewModel;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NETCore.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController<Account, AccountRepository, string>
    {
        private readonly AccountRepository accountRepository;
        private readonly MyContext myContext;
        public IConfiguration configuration;
        public AccountController(AccountRepository repository, MyContext myContext, IConfiguration config) : base(repository)
        {
            this.accountRepository = repository;
            this.myContext = myContext;
            configuration = config;
        }
        //[Authorize]
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
                var claims = new List<Claim>
                {
                    new Claim("NIK", NIK),
                    new Claim("Email", loginVM.Email),
                };
                foreach (var item in accountRepository.GetRole(NIK))
                {
                    claims.Add(new Claim(ClaimTypes.Role, NIK));
                }
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));

                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    configuration["Jwt:Issuer"],
                    configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(10),
                    signingCredentials: signIn);
                /*return Ok(new JWTokenVM {Token = token, Messages = "Login Succes"})*/
                return Ok(new JWTokenVM
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Messages = "Login Berhasil!!"
                });
                /*return Ok(new
                {
                    status = HttpStatusCode.OK,
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    message = "Login Berhasil"
                });*/
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
            try
            {
                accountRepository.ForgetPassword(forgetPasswordVM);
                return Ok(new
                {
                statusCode = HttpStatusCode.OK,
                message = "Password berhasil dikirim melalui email"
                });
            }
            catch
            {
                return BadRequest(new
                {
                    statusCode = HttpStatusCode.BadRequest,
                    mesaage = "email tidak ada di database atau salah memasukan email"
                });
            }
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
