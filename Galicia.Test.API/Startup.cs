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

namespace Galicia.Test.API
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


            services.AddDbContext<TestContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:TestDbContext"]), ServiceLifetime.Transient);
            services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddTransient<IPersonaBusinessRules, PeronaBusinessRules>();

        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Galicia.Test.Api v1"));
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
       
    }
}
