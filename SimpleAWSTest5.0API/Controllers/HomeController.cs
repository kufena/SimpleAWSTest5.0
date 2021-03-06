using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using SimpleAWSTest5._0API.Utils;
using System.Net;

namespace SimpleAWSTest5._0API.Controllers
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
            var r = DateTime.Now;
            
            return Ok(new
            {
                fromConfig = _parameters.Value.SomeParameter,
                hostName = System.Environment.MachineName,
                date = r.ToLongDateString(),
                time = r.ToLongTimeString(),
                message="Hello World!"
            });
        }
    }
}