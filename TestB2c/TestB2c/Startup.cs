using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphApi;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Repository;
using Repository.GenericRepo;
using Repository.Managers;


namespace TestB2c
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<EmployeeContext>(option =>
            {
                option.UseSqlServer(Configuration.GetConnectionString("connectionString"));
            });
            services.AddScoped<PermanentEmployeeManager>();
            //IGenericRepository<PermanentEmployee
            services.AddTransient(typeof(GenericRepository<>), typeof(GenericRepository<>));
            //services.AddSingleton<B2CGraphClient>(new B2CGraphClient("", "", ""));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
        }
    }
}
