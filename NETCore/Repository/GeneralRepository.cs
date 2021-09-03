using Microsoft.EntityFrameworkCore;
using NETCore.Context;
using NETCore.Repository.Interface;
using NETCore.ViewModel;
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
            dbSet.Remove(dbSet.Find(key));
            return myContext.SaveChanges();
            throw new ArgumentException("Entity");
        }

        public IEnumerable<Entity> Get()
        {
            return dbSet.ToList();
        }

        public Entity Get(Key key)
        {
            return dbSet.Find(key);
        }

        public string GetPerson(GetPersonVM getPersonVM)
        {
            throw new NotImplementedException();
        }

        public int Insert(Entity entity)
        {
            dbSet.Add(entity);
            return myContext.SaveChanges();
        }

        public int Update(Entity entity)
        {
            dbSet.Update(entity);
            return myContext.SaveChanges();
            throw new ArgumentException("Entity");
        }
    }
}
