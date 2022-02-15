using DbHelper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NetCoreDemoService.IService;
using NetCoreDemoService.ServiceImplements;
using Product.API.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton<ILogService, ExceptionLessLogger>();
            DataAccess.ConnectionConfigure(Configuration.GetConnectionString("ProductContext"));

            services.AddCap(x =>
            {
                x.UseMySql(Configuration.GetSection("ConnectionStrings:ProductContext").Value);

                x.UseRabbitMQ(conf =>
                {
                    conf.HostName = "10.112.9.135";
                    conf.UserName = "guest";
                    conf.Password = "pdchi2002$";
                    conf.Port = 5673;
                });

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime lifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.RegisterConsul(Configuration, lifetime);
        }
    }
}
