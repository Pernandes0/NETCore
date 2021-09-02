using NETCore.Context;
using NETCore.Models;
using NETCore.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace NETCore.Repository.Data
{
    public class AccountRepository : GeneralRepository<MyContext, Account, string>
    {
        private readonly MyContext myContext;
        public AccountRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }
        public string CheckLoginEmail(string Email)
        {
            var checkLoginemail = (from p in myContext.Persons where p.Email == Email select new GetPersonVM { NIK = p.NIK }).ToList();
            if (checkLoginemail.Count == 0)
                return null;
            else
                return checkLoginemail[0].NIK;
        }
        public bool CheckLoginPassword(string NIK, string Password)
        {
            var checkLoginemail = (from p in myContext.Persons
                                   join a in myContext.Accounts on p.NIK equals a.NIK
                                   where p.NIK == NIK
                                   select new GetPersonVM
                                   {
                                       NIK = p.NIK,
                                       Password = a.Password
                                   }).ToList();
            //if (ValidatePassword(Password, hashPassword))
            if ((checkLoginemail[0].Password == Password))
                return true;
            else
                return false;
        }
        public void ForgetPassword(ForgetPasswordVM forgetPasswordVM)
        {
                /*Guid guid = Guid.NewGuid();
                string password = guid.ToString();
                return password;
                */
            var chekEmail = myContext.Persons.Where(p => p.Email.Equals(forgetPasswordVM.Email)).FirstOrDefault();
            if (chekEmail != null)
            {
                string guid = Guid.NewGuid().ToString();
                string password = guid;

                var checkEmail_ = myContext.Accounts.Where(a => a.NIK == chekEmail.NIK).FirstOrDefault();
                checkEmail_.Password = guid;
                Update(checkEmail_);
                Email(password, forgetPasswordVM.Email);
            }

        }
       // public void ChangePassword(ChangePasswordVM changePasswordVM)
        //{
        //}
        public static void Email(string password, string destinationEmail)
        {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("pernandes1601122@gmail.com");
                message.To.Add(new MailAddress(destinationEmail));
                message.Subject = "Reset password request";
                message.Body = $"Hi, password anda telah diganti menjadi {password}";
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("pernandes1601122@gmail.com", "");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
        }
    }
}
