using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.MVC.Helper
{
    /// <summary>
    /// 通过gateway调用服务
    /// </summary>
    public class GatewayServiceHelper : IServiceHelper
    {
        public async Task<string> GetOrder()
        {
            var Client = new RestClient("http://10.112.9.230:9080");
            var request = new RestRequest("/api/orders", Method.Get);

            var response = await Client.ExecuteAsync(request);
            return response.Content;
        }

        public async Task<string> GetProduct()
        {
            var Client = new RestClient("http://10.112.9.230:9080");
            var request = new RestRequest("/api/products", Method.Get);

            var response = await Client.ExecuteAsync(request);
            return response.Content;
        }

        public void GetServices()
        {
            throw new NotImplementedException();
        }
    }
}
