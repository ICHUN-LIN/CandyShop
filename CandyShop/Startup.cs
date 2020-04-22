using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CandyShop.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CandyShop
{
    //Only run one time When Server is Wakeup
    public class Startup
    {
        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940


        public Startup(IConfiguration configuration) 
        {
            //read appsettings.json
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContext<AppDbContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            /*add scope method means it would exist during all request working 1.with database*/
            //interface, implementation
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICandyRepository, CandyRepository>();
            //create a signgal shopping cart in request period
            services.AddScoped<ShoppingCart>(sc => ShoppingCart.GetCart(sc));
            services.AddScoped<IOrderRepository, OrderRepository>();

            //let session be used by noncontroler class : ex: shopping cart class.
            services.AddHttpContextAccessor();
            //support session in request
            services.AddSession();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //after deployment, it need remove.
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //The order of middlewere is very important because it impact the order the service work
            //use Https Redirection
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            //We would like use session before Rout?
            //use session in middlewere
            app.UseSession();
            app.UseRouting();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=candy}/{action=list}/{id?}"
                    );
                /*
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
                */
            });
        }
    }
}
