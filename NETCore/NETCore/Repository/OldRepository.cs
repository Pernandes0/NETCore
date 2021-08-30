using Microsoft.EntityFrameworkCore;
using NETCore.Context;
using NETCore.Models;
using NETCore.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NETCore.Repository
{
    public class OldRepository : IPersonRepository
    {
        private readonly MyContext myContext;
        public OldRepository(MyContext myContext)
        {
            this.myContext = myContext;
        }
        public int Delete(string NIK)
        {
            var wantdelete = myContext.Persons.Find(NIK);
            if (wantdelete == null)
            {
                throw new ArgumentNullException();
            }
            myContext.Persons.Remove(wantdelete);
            var delete = myContext.SaveChanges();
            return delete;
        }

        public IEnumerable<Person> Get()
        {
            if (myContext.Persons.ToList().Count == 0)
            {
                throw new ArgumentNullException();
            }
            return myContext.Persons.ToList();
        }

        public Person Get(string NIK)
        {
            if(myContext.Persons.Find(NIK) != null) 
            {
                return myContext.Persons.Find(NIK);
            }
            else
            {
                throw new ArgumentNullException();
            }
            //return myContext.Persons.Find(NIK);
        }

        public int Insert(Person person)
        {
            myContext.Persons.Add(person);
            var insert = myContext.SaveChanges();
            return insert;
        }

        public int Update(Person person)
        {
            var wantUpdate = myContext.Persons.Find(person.NIK);
            if (wantUpdate != null)
            {
                throw new Exception();
            }
            myContext.Entry(person).State = EntityState.Modified;
            var update = myContext.SaveChanges();
            return update;
        }
    }
}

