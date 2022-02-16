using DbHelper.MessageDto;
using DotNetCore.CAP;
using Exceptionless;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NetCoreDemoService.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IConfiguration _configuration;
        private readonly ICapPublisher _capBus;
        private readonly ILogService _logService;
        public ProductsController(ILogger<ProductsController> logger,IConfiguration configuration, ILogService logService, ICapPublisher capPublisher)
        {
            _logger = logger;
            _configuration = configuration;
            _capBus = capPublisher;
            _logService = logService;
        }

        [HttpGet]
        public IActionResult Get()
        {

            

            _logService.Info("Called Product service");

            string result = $"【产品服务】{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}——" +
                $"{Request.HttpContext.Connection.LocalIpAddress}:{_configuration["ConsulSetting:ServicePort"]}";
            return Ok(result);
        }

        /// <summary>
        /// 减库存 订阅下单事件
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [NonAction]
        [CapSubscribe("order.services.createorder")]
        public  IActionResult ReduceStock(CreateOrderMessageDto message)
        {

            try
            {
                _logService.Info("Called order.services.createorder");
                var product = DbHelper.DataAccess.GetProductById(message.ProductID);
                product.Stock -= message.Count;
                DbHelper.DataAccess.UpdateProduct(product);

            }
            catch(Exception ex)
            {
                _logService.Error(ex.Message);
            }
            //业务代码
    
            return Ok();
        }


    }
}
