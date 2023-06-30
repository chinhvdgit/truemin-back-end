using AspNetCoreHero.Boilerplate.Api.Extensions;
using AspNetCoreHero.Boilerplate.Api.Middlewares;
using AspNetCoreHero.Boilerplate.Application.Extensions;
using AspNetCoreHero.Boilerplate.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;

namespace AspNetCoreHero.Boilerplate.Api
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IConfiguration _configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationLayer();
            services.AddContextInfrastructure(_configuration);
            services.AddPersistenceContexts(_configuration);
            services.AddRepositories();
            services.AddSharedInfrastructure(_configuration);
            services.AddEssentials();
            services.AddControllers();

            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  builder =>
                                  {
                                      builder.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost");

                                      builder.WithOrigins(
                                          "http://localhost:3000",
                                          "http://localhost:2999",
                                          "http://localhost:8081",
                                          "http://localhost:8088",
                                                          "http://localhost:8082",
                                                          "http://localhost:3011",
                                                          "http://172.16.4.163:8088",
                                                          "http://192.168.1.28:8088",
                                                          "http://asiasoftdn.ddns.net:8088"
                                                          )
                                              .AllowAnyMethod()
                                                .AllowAnyHeader()
                                                .AllowCredentials();
                                      //builder
                                      //      .AllowAnyOrigin()
                                      //      .AllowAnyMethod()
                                      //      .AllowAnyHeader()
                                      //      .AllowCredentials();
                                      //builder.WithOrigins("http://localhost:8080",
                                      //    "http://localhost:8081",
                                      //    "http://localhost:8088",
                                      //                    "http://localhost:8082",
                                      //                    "http://localhost:3011")
                                      //        .AllowAnyMethod()
                                      //          .AllowAnyHeader()
                                      //          .AllowCredentials();
                                  });
            });

            //services.AddCors(options =>
            //{
            //    options.AddDefaultPolicy(
            //        builder =>
            //        {
            //            builder.WithOrigins("https://h5.zdn.vn/zapps/4512663114183583577", "http://localhost:3000")
            //                                .AllowAnyHeader()
            //                                .AllowAnyMethod();
            //        });
            //});

            //// Named Policy
            //services.AddCors(options =>
            //{
            //    options.AddPolicy(name: "AllowOrigin",
            //        builder =>
            //        {
            //            builder.WithOrigins("https://localhost:44351", "http://localhost:3000")
            //                                .AllowAnyHeader()
            //                                .AllowAnyMethod();
            //        });
            //});

            services.AddMvc(o =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                o.Filters.Add(new AuthorizeFilter(policy));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseFileServer(new FileServerOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")),
                RequestPath = "/wwwroot",
                EnableDefaultFiles = true
            });

            app.ConfigureSwagger();
            app.UseHttpsRedirection();
            app.UseMiddleware<ErrorHandlerMiddleware>();
            app.UseRouting();

            app.UseRouting();

            //// Shows UseCors with CorsPolicyBuilder.
            //app.UseCors(builder =>
            //{
            //    builder
            //    .AllowAnyOrigin()
            //    .AllowAnyMethod()
            //    .AllowAnyHeader();
            //});

            app.UseCors(MyAllowSpecificOrigins);


            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}