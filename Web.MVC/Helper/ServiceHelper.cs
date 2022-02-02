using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.MVC.Helper
{
    public class ServiceHelper : IServiceHelper
    {
        public async Task<string> GetOrder()
        {
            string serviceUrl = "http://10.112.9.230:9050";//订单服务的地址，可以放在配置文件或者数据库等等...

            var Client = new RestClient(serviceUrl);
            var request = new RestRequest("/api/orders", Method.Get);

            var response = await Client.ExecuteAsync(request);
            return response.Content;
        }

        public async Task<string> GetProduct()
        {
            string serviceUrl = "http://10.112.9.230:9060";//产品服务的地址，可以放在配置文件或者数据库等等...

            var Client = new RestClient(serviceUrl);
            var request = new RestRequest("/api/products", Method.Get);

            var response = await Client.ExecuteAsync(request);
            return response.Content;
        }
    }
}
