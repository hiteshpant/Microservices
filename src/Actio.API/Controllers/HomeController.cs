using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Actio.API.Controllers
{
    [Route("api/[controller]")]
    public class HomeController:Controller
    {
        public IActionResult Get() => Content("Hello from action api");

    }
}
