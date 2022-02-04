using Web.Framework.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;
using Swashbuckle.AspNetCore.JsonMultipartFormDataSupport;
using System.IO;

namespace Web.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

        //    var builder = new ConfigurationBuilder()
        //.SetBasePath(Directory.GetCurrentDirectory())
        //.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        //.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
        //.AddEnvironmentVariables();
        //    this.Configuration = builder.Build();

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddAutoMapper();
            services.AddFramework(Configuration);

            services.AddControllers();
            services.AddApiVersioningExtension();
            services.AddJsonMultipartFormDataSupport(JsonSerializerChoice.Newtonsoft);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Web.Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                // app.UseSwagger();
                // app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Web.Api v1"));
            }
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Web.Api v1"));
            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());
            app.UseRouting();
            app.UseAuthorization();
            app.UseApiErrorHandlingMiddleware();
            app.UseSerilogRequestLogging();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
