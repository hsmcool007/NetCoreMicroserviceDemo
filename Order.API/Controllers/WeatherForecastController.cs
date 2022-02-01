using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetCoreDemoService.IService;
using NetCoreDemoService.ServiceImplements;

namespace Order.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ITestService _testService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,ITestService testService)
        {
            _logger = logger;
            _testService = testService;
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Ok(_testService.test(1, 2));
        }


    }
}
