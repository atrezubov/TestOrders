//A.Trezubov 06.06.2021

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestOrders.BLL.Interfaces;
using TestOrders.BLL.Services;
using TestOrders.DAL.EF;

namespace TestOrders.WEB
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
            
            //EF---------------------------------------------------------------------------
            //Getting connection settings
            string connectionSQL = Configuration.GetConnectionString("SQLConnection");
            //Adding database context MS SQL Server.
            services.AddDbContext<SQLServerContext>(options => options.UseSqlServer(connectionSQL, p => p.MigrationsAssembly("TestOrders.WEB")));
            //DI IOrders interface.
            services.AddTransient<IOrders, OrderSQL>();
            //DI ICustomers interface.
            services.AddTransient<ICustomers, CustomerSQL>();
            //-----------------------------------------------------------------------------


            services.AddControllersWithViews();
            //services.AddControllers();
            services.AddCors();

            //Filling DB directories with primary data.
            Program.FillDatabase(services);

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            //We don't use authorization in this simple app.
            //app.UseAuthorization();

            //We'll use REST API we need the CORS.
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); //for rest api
                endpoints.MapControllerRoute( //for mvc
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
