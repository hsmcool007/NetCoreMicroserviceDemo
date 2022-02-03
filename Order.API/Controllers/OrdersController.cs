using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetCoreDemoService.IService;
using Exceptionless;

namespace Order.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {

        private readonly ILogger<OrdersController> _logger;
        private readonly IConfiguration _configuration;
        private readonly ILogService _logService;

        public OrdersController(ILogger<OrdersController> logger, IConfiguration configuration,ILogService logService)
        {
            _logger = logger;
            _configuration = configuration;
            _logService = logService;
    
        }

        [HttpGet]
        public IActionResult Get()
        {

            try
            {
                throw new Exception("测试项目的异常");
            }
            catch (Exception ex)
            {
                ex.ToExceptionless().Submit(); //这个很重要
            }

            _logService.Info("This is a test");

            ExceptionlessClient.Default.CreateLog("hello").Submit();

            ExceptionlessClient.Default.CreateLog("this is a test", Exceptionless.Logging.LogLevel.Debug).Submit();

            string result ="test"+ $"【订单服务】{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}——" +
                $"{Request.HttpContext.Connection.LocalIpAddress}:{_configuration["ConsulSetting:ServicePort"]}";
            return Ok(result);
        }


    }
}
