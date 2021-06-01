
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Net;
using System.Threading;

namespace GrpcServiceForAngular
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGrpc();
            services.AddCors(o =>
            {
                o.AddPolicy("MyPolicy", builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                    builder.WithExposedHeaders("Grpc-Status", "Grpc-Message");
                });
            });
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseCors("MyPolicy");
            app.UseGrpcWeb();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapGrpcService<CountryService>().RequireCors("MyPolicy").EnableGrpcWeb();
                endpoints.MapGrpcService<Services.DatabaseServerService>().RequireCors("MyPolicy").EnableGrpcWeb();
                endpoints.MapGrpcService<Services.LoginServices>().RequireCors("MyPolicy").EnableGrpcWeb();
            });
            //Stress Testing
            //for (int i = 0; i < 5000; i++)
            //{
            //    Thread.Sleep(50);                
            //    Console.WriteLine( "TESTING: From proxy server Login was: " + new Services.LoginServices().LoginAD(new Proto.LoginRequset(), null).Result.LoginSucsefull);
            //}
        }
    }
}
