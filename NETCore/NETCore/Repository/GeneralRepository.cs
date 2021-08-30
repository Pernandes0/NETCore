using Microsoft.EntityFrameworkCore;
using NETCore.Context;
using NETCore.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NETCore.Repository
{
    public class GeneralRepository<Context, Entity, Key> : IRepository<Entity, Key>  
        where Entity : class
        where Context : MyContext
    {
        private readonly MyContext myContext;
        private readonly DbSet<Entity> dbSet;

        public GeneralRepository(MyContext myContext)
        {
            this.myContext = myContext;
            dbSet = myContext.Set<Entity>();

        }
  
        public int Delete(Key key)
        {
            var wantdelete = dbSet.Find(key);
            if (wantdelete == null)
            {
                throw new ArgumentNullException();
            }
            dbSet.Remove(wantdelete);
            var delete = myContext.SaveChanges();
            return delete;
        }

        public IEnumerable<Entity> Get()
        {
            if (myContext.Persons.ToList().Count == 0)
            {
                throw new ArgumentNullException();
            }
            return dbSet.ToList();
        }

        public Entity Get(Key key)
        {
            if (dbSet.Find(key) != null)
            {
                return dbSet.Find(key);
            }
            else
            {
                throw new ArgumentNullException();
            }
            //return myContext.Persons.Find(NIK);
        }

        public int Insert(Entity entity)
        {
            dbSet.Add(entity);
            var insert = myContext.SaveChanges();
            return insert;
        }

        public int Update(Entity entity)
        {
            var wantUpdate = dbSet.Find(entity);
            if (wantUpdate != null)
            {
                throw new Exception();
            }
            myContext.Entry(entity).State = EntityState.Modified;
            var update = myContext.SaveChanges();
            return update;
        }
    }
}
