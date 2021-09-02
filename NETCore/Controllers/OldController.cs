using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NETCore.Models;
using NETCore.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace NETCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OldController : ControllerBase
    {
        private readonly OldRepository personRepository;
        public OldController(OldRepository personRepository)
        {
            this.personRepository = personRepository;
        }

    }
}
