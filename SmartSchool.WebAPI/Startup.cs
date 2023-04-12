using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore.Sqlite;
using SmartSchool.WebAPI.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Microsoft.Extensions.Options;

namespace SmartSchool.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

       
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SmartContext>(context => context.UseSqlite(Configuration.GetConnectionString("Default")));
            
            services.AddScoped<IRepository, Repository>();


            services.AddControllers().AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            
            
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("SmartSchoolAPI", 
                new Microsoft.OpenApi.Models.OpenApiInfo() 
                { 
                    Title = "SmartSchool API", 
                    Version = "v1" 
                }
            );            

            var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlCommentsFullPath = System.IO.Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);

            options.IncludeXmlComments(xmlCommentsFullPath);

            });
        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseSwagger()
                .UseSwaggerUI(options => 
            {
                options.SwaggerEndpoint("/swagger/SmartSchoolAPI/swagger.json", "SmartSchoolAPI");
                options.RoutePrefix = "";
            });
                            

            // app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}