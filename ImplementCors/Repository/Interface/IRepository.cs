using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ImplementCors.Repository.Interface
{
    public interface IRepository<Entity, Key> 
        where Entity : class
    {
        Task<List<Entity>> Get();
        Task<Entity> Get(Key key);
        HttpStatusCode Post(Entity entity);
        HttpStatusCode Put(Key key, Entity entity);
        HttpStatusCode Delete(Key key);
    }
}
