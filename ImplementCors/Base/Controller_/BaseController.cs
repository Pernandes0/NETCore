using ImplementCors.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImplementCors.Base.Controller_
{
    public class BaseController<Entity, TRepository, Key> : Controller
            where Entity : class
            where TRepository : IRepository<Entity, Key>
    {
        private readonly TRepository repository;

        public BaseController(TRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<JsonResult> GetAll()
        {
            var result = await repository.Get();
            return Json(result);
        }

        [HttpGet]
        public async Task<JsonResult> Get(Key key)
        {
            var result = await repository.Get(key);
            return Json(result);
        }

        [HttpPost]
        public JsonResult Post(Entity entity)
        {
            var result = repository.Post(entity);
            return Json(result);
        }

        [HttpPut]
        public JsonResult Put(Key key, Entity entity)
        {
            var result = repository.Put(key, entity);
            return Json(result);
        }

        [HttpDelete]
        public JsonResult Delete(Key key)
        {
            var result = repository.Delete(key);
            return Json(result);
        }
    }
}
