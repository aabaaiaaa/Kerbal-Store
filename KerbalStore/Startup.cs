using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using KerbalStore.Data;
using KerbalStore.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace KerbalStore
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<KerbalStoreContext>(cfg =>
            {
                cfg.UseSqlServer(configuration.GetConnectionString("KerbalStoreConnectionString"));
            });

            services.AddAutoMapper();

            services.AddScoped<IKerbalStoreRepository, KerbalStoreRepository>();
            services.AddScoped<ILoginRepository, KerbalStoreRepository>();
            services.AddScoped<ITicketRepository, KerbalStoreRepository>();
            services.AddTransient<KerbalStoreSeeder>();

            services.AddTransient<ITicketService, TicketService>();

            services.AddIdentity<ShopUser, IdentityRole>()
                .AddEntityFrameworkStores<KerbalStoreContext>();

            services.AddMvc()
                .AddJsonOptions(opt =>
                {
                    opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //} else
            //{
            //    app.UseExceptionHandler("/error");
            //}
            app.UseExceptionHandler("/error");

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(cfg => {
                cfg.MapRoute("Default", "{controller}/{action}/{id?}", new { controller = "StorePage", action = "Index" });
            });

            if (env.IsDevelopment())
            {
                // Only seed when in development
                using (var scopedContext = app.ApplicationServices.CreateScope())
                {
                    var seeder = scopedContext.ServiceProvider.GetService<KerbalStoreSeeder>();
                    seeder.Seed().Wait();
                }
            }
        }
    }
}
