using System;
using System.Collections.Generic;
using System.Text;
using NetCoreDemoService.IService;

namespace NetCoreDemoService.ServiceImplements
{
    public class TestService : ITestService
    {
        public int test(int a, int b)
        {
            return a + b;
        }
    }
}
