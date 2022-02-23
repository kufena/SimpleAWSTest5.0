using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace SimpleAWSTestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private IConfiguration _config;
        private IOptions<Parameters> _parameters;
        
        public HomeController(IConfiguration config, IOptions<Parameters> options)
        {
            this._config = config;
            this._parameters = options;
        }
        
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new { fromConfig = _parameters.Value.SomeParameter });
        }
    }
}