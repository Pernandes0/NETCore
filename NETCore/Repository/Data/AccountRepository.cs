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
        public string GetRole(string NIK)
        {
            string roles = "";
            var getRole = (from a in myContext.Accounts
                           join ar in myContext.AccountRoles on a.NIK equals ar.AccountNIK
                           join r in myContext.Roles on ar.RoleId equals r.Id
                           select new Role
                           {
                               Name = r.Name
                           }).ToList();
            for (int i = 0; i < getRole.Count; i++)
            {
                roles += getRole[i].Name;
                if (i < getRole.Count - 1)
                {
                    roles += "";
                }
            }
            return roles;
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
            var checkLoginPass = (from p in myContext.Persons
                                   join a in myContext.Accounts on p.NIK equals a.NIK
                                   where p.NIK == NIK
                                   select new GetPersonVM
                                   {
                                       NIK = p.NIK,
                                       Password = a.Password
                                   }).ToList();
            if ((checkLoginPass[0].Password == Password))
            //if (BCrypt.Net.BCrypt.Verify(Password, checkLoginPass[0].Password))
                return true;
            else
                return false;
        }
        public void ForgetPassword(ForgetPasswordVM forgetPasswordVM)
        {
            var person = myContext.Persons.Where(p => p.Email.Equals(forgetPasswordVM.Email)).FirstOrDefault();
            if (person != null)
            {
                string guid = Guid.NewGuid().ToString();
                string password = guid;

                var account = myContext.Accounts.Where(a => a.NIK == person.NIK).FirstOrDefault();
                // myContext.Entry(BCrypt.Net.BCrypt.HashPassword(account.Password));
                account.Password = guid;
                Update(account);
                var update = myContext.SaveChanges();
                Email(password, forgetPasswordVM.Email);
            }
        }
        public static void Email(string password, string destinationEmail)
        {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("pernandes1601122@gmail.com");
                message.To.Add(new MailAddress(destinationEmail));
                message.Subject = $"Reset password request {DateTime.Now}";
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
