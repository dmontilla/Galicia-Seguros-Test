using Galicia.Test.BusinessRules.Implementation.Person;
using Galicia.Test.BusinessRules.Person;
using Galicia.Test.Core.Base;
using Galicia.Test.Infrastructure.Data;
using Galicia.Test.Infrastructure.Repositories;
using Galicia.Test.Shared.Profiles;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Reflection;

namespace Galicia.Test.RestApi
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

            AddAutoMapper();
            AddSwagger(services);


            services.AddDbContext<TestContext>(options => options.UseSqlServer(Configuration["ConnectionString:TestDbContext"]), ServiceLifetime.Transient);
            services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddTransient<IPersonaBusinessRules, PeronaBusinessRules>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        private void AddAutoMapper() => AutoMapperConfiguration.CreateMapping();
        private static void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc(
                    "TestAPIEspecificaciones",
                    new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = "Test API",
                        Version = "1",
                        Description = "Rest API Ejemplo",
                        Contact = new Microsoft.OpenApi.Models.OpenApiContact()
                        {
                            Email = "ejemplo@galiciaseguros.com.ar",
                            Name = "Usuario Ejemplo",
                            Url = new Uri("www.galiciaseguros.com.ar")
                        },
                    });


                var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);

                setupAction.IncludeXmlComments(xmlCommentsFullPath);
            });
        }
    }
}
