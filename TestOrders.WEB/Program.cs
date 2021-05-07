using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using TestOrders.DAL.EF;
using Microsoft.EntityFrameworkCore;
using TestOrders.DAL.Entities;

namespace TestOrders.WEB
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });


        //Fill data in DB when first run app.
        public static void FillDatabase(IServiceCollection services)
        {
            //We certainly shouldn't use a data layer at the application level. 
            //But we run these scripts once and for the sake of this 
            //we would not want to write a separate interface in the test application. 
            //In production, this functionality should be rewritten as a separate interface.

            var db = services.BuildServiceProvider().GetService<SQLServerContext>();

            //Has DB any customers.
            Boolean hasCustomers = db.Customers.Any();
            if (!hasCustomers)
            {
                //ExecuteSqlRaw is used only to demonstrate competence.
                try
                {
                    db.Database.ExecuteSqlRaw("insert into Customers (Name, Address) values ('Twen Mark', 'USA, NY')");
                    db.Database.ExecuteSqlRaw("insert into Customers (Name, Address) values ('Morrison Jim', 'USA, LA')");
                } catch (Exception e)
                {

                }
               
            }

            Boolean hasOrders = db.Orders.Any();
            if (!hasOrders)
            {
                IList<Customer> customers = db.Customers.ToList();
                int i = 1;
                foreach (Customer customer in customers)
                {
                    Order order = new Order() { CustomerId = customer.Id, Number = i, Amount = 3, Description = "Automatic value." };
                    try
                    {
                        db.Orders.Add(order);
                        db.SaveChanges();
                    } catch (Exception e) { }
                   
                    i += 1;
                }
            }

            db.Dispose();

        }
    }
}
