using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OnlineBookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBookStore
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }
        
        public Startup (IConfiguration temp)
        {
            Configuration = temp;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContext<BookstoreContext>(options =>
            {
                options.UseSqlite(Configuration["ConnectionStrings:BookstoreDBConnection"]);
            });
            services.AddScoped<IBookstoreRepository, EFBookstoreRepository>();
            // Above is where we will do the repo stuff        
            services.AddRazorPages();
            services.AddDistributedMemoryCache();
            services.AddSession();

            // If cart has already been set up it will grab it. If not, it will create a new one
            services.AddScoped<Cart>(x => SessionCart.GetCart(x));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                // Catches case when category and page are passed
                endpoints.MapControllerRoute("categorypage", "{category}/Page{page_num}",
                new { Controller = "Home", action = "Index" });

                // Catches case when there is only a page passed
                endpoints.MapControllerRoute("paging", "Page{page_num}",
                    new { Controller = "Home", action = "Index", page_num =1 });

                // Catches case when only category passed
                endpoints.MapControllerRoute("category", "{category}",
                    new { Controller = "Home", action = "Index", page_num = 1 });

                endpoints.MapDefaultControllerRoute();
                endpoints.MapRazorPages();
            });
        }
    }
}
