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
using Microsoft.OpenApi.Models;
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

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            AddAutoMapper();
            AddSwagger(services);
            //services.AddSwaggerGen();



            services.AddDbContext<TestDbContext>(options =>
            {
                options.UseSqlServer(Configuration["ConnectionString:TestDbContext"], sqlServerOptionsAction: sqlOption =>
                {
                    sqlOption.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                    sqlOption.EnableRetryOnFailure(maxRetryCount: 5, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                }

                );
            });

            services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddTransient<IPersonaBusinessRules, PeronaBusinessRules>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Rest API Ejemplo");
            });

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
                setupAction.SwaggerDoc("v1",new OpenApiInfo()
                    {
                        Version = "v1",
                        Title = "Test API",
                        Description = "Rest API Ejemplo",
                        Contact = new Microsoft.OpenApi.Models.OpenApiContact()
                        {
                            Email = "ejemplo@galiciaseguros.com.ar",
                            Name = "Usuario Ejemplo",
                            Url = new Uri("https://www.galiciaseguros.com.ar/")
                        },
                    });


                var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);

                setupAction.IncludeXmlComments(xmlCommentsFullPath);
            });
        }
    }
}
