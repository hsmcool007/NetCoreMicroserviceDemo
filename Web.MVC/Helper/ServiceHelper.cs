using Consul;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.MVC.Helper
{
    public class ServiceHelper : IServiceHelper
    {
        private readonly IConfiguration _configuration;

        public ServiceHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> GetOrder()
        {
            //string[] serviceUrls = { "http://localhost:9060", "http://localhost:9061", "http://localhost:9062" };//订单服务的地址，可以放在配置文件或者数据库等等...

            var consulClient = new ConsulClient(c =>
            {
                //consul地址
                c.Address = new Uri(_configuration["ConsulSetting:ConsulAddress"]);
            });

            //consulClient.Catalog.Services().Result.Response;
            //consulClient.Agent.Services().Result.Response;
            var services = consulClient.Health.Service("OrderService", null, true, null).Result.Response;//健康的服务

            string[] serviceUrls = services.Select(p => $"http://{p.Service.Address + ":" + p.Service.Port}").ToArray();//订单服务地址列表

            if (!serviceUrls.Any())
            {
                return await Task.FromResult("【订单服务】服务列表为空");
            }

            //每次随机访问一个服务实例
            var Client = new RestClient(serviceUrls[new Random().Next(0, serviceUrls.Length)]);
            var request = new RestRequest("/api/orders", Method.Get);

            var response = await Client.ExecuteAsync(request);
            return response.Content;
        }

        public async Task<string> GetProduct()
        {
            //string[] serviceUrls = { "http://localhost:9050", "http://localhost:9051", "http://localhost:9052" };//产品服务的地址，可以放在配置文件或者数据库等等...

            var consulClient = new ConsulClient(c =>
            {
                //consul地址
                c.Address = new Uri(_configuration["ConsulSetting:ConsulAddress"]);
            });

            //consulClient.Catalog.Services().Result.Response;
            //consulClient.Agent.Services().Result.Response;
            var services = consulClient.Health.Service("ProductService", null, true, null).Result.Response;//健康的服务

            string[] serviceUrls = services.Select(p => $"http://{p.Service.Address + ":" + p.Service.Port}").ToArray();//产品服务地址列表

            if (!serviceUrls.Any())
            {
                return await Task.FromResult("【产品服务】服务列表为空");
            }

            //每次随机访问一个服务实例
            var Client = new RestClient(serviceUrls[new Random().Next(0, serviceUrls.Length)]);
            var request = new RestRequest("/api/products", Method.Get);

            var response = await Client.ExecuteAsync(request);
            return response.Content;
        }
    }
}
