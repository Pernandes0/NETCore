using NETCore.Context;
using NETCore.Models;
using NETCore.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NETCore.Repository.Data
{
    public class PersonRepository : GeneralRepository<MyContext, Person, string>
    {
        private readonly MyContext myContext;
        public PersonRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }
        public IEnumerable<GetPersonVM> GetPersonVMs()
        {
            var getPersonVMs = (from p in myContext.Persons
                                join a in myContext.Accounts on p.NIK equals a.NIK
                                join pr in myContext.Profillings on a.NIK equals pr.NIK
                                join e in myContext.Educations on pr.EducationId equals e.Id
                                select new GetPersonVM
                                {
                                    NIK = p.NIK,
                                    FullName = p.FirstName + " " + p.LastName,
                                    Phone = p.Phone,
                                    BirthDate = p.BirthDate,
                                    Salary = p.Salary,
                                    Email = p.Email,
                                    gender = (GetPersonVM.Gender)p.gender,
                                    Password = "********",
                                    Degree = e.Degree,
                                    GPA = e.GPA
                                }).ToList();
            if (getPersonVMs.Count == 0)
            {
                return null;
            }
            return getPersonVMs;
        }
        public IEnumerable<GetPersonVM> GetPersonVMsNIK(string NIK)
        {
            var getPersonVMsNIK = (from p in myContext.Persons
                                   join a in myContext.Accounts on p.NIK equals a.NIK
                                   join pr in myContext.Profillings on a.NIK equals pr.NIK
                                   join e in myContext.Educations on pr.EducationId equals e.Id
                                   where p.NIK == NIK
                                   select new GetPersonVM
                                   {
                                       NIK = p.NIK,
                                       FullName = p.FirstName + " " + p.LastName,
                                       Phone = p.Phone,
                                       BirthDate = p.BirthDate,
                                       Salary = p.Salary,
                                       Email = p.Email,
                                       gender = (GetPersonVM.Gender)p.gender,
                                       Password = "*******",
                                       Degree = e.Degree,
                                       GPA = e.GPA
                                   }).ToList();
            if (getPersonVMsNIK.Count == 0)
            {
                return null;
            }
            return getPersonVMsNIK;
        }
        public int InsertPerson(GetPersonVM getPersonVM)
        {
            Person person = new Person();
            person.NIK = getPersonVM.NIK;
            string[] name = getPersonVM.FullName.Split(' ');
            person.FirstName = name[0];
            person.LastName = name[1];
            person.Phone = getPersonVM.Phone;
            person.BirthDate = getPersonVM.BirthDate;
            person.Salary = getPersonVM.Salary;
            person.Email = getPersonVM.Email;
            person.gender = (Person.Gender)getPersonVM.gender;
            myContext.Persons.Add(person);

            Account account = new Account();
            account.NIK = getPersonVM.NIK;
            // string saltPassword = BCrypt.Net.BCrypt.GenerateSalt(12);
            // account.Password = BCrypt.Net.BCrypt.HashPassword(getPersonVM.Password, saltPassword);
            account.Password = (getPersonVM.Password);
            myContext.Accounts.Add(account);
        
            Education education = new Education();            
            education.Degree = getPersonVM.Degree;
            education.GPA = getPersonVM.GPA;
            // education.UniversityId = 1;
            education.UniversityId = getPersonVM.UniversityId;
            myContext.Educations.Add(education);
            myContext.SaveChanges();

            AccountRole accountRole = new AccountRole();
            accountRole.AccountNIK = getPersonVM.NIK;
            //accountRole.RoleId = 4;
            myContext.AccountRoles.Add(accountRole);
            myContext.SaveChanges();

            Profilling profiling = new Profilling();
            profiling.NIK = getPersonVM.NIK;
            profiling.EducationId = education.Id;
            myContext.Profillings.Add(profiling);
            var insert = myContext.SaveChanges();
            return insert;
        }
    }
}
