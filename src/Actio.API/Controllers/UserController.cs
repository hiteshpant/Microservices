using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Actio.API.Model;
using Actio.Common.Commands;
using Actio.Common.Events;
using EventBus.Abstratction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RawRabbit;

namespace Actio.API.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IEventBus _Busclient;
        public UserController(IEventBus bus)
        {
            _Busclient = bus;
        }

        [HttpPost("register")]
        public IActionResult Post([FromBody] User command)
        {
            var userCreatedEvent = new UserCreated(command.Name, command.Email, command.Password);
            _Busclient.Publish(userCreatedEvent);
            return Accepted("User Creation Published");
        }
    }
}