
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNet.SignalR.Hubs;

using Nekretnine.Data.Models;
using Agencija_za_promet_nekretninama.Models;
using Microsoft.AspNetCore.Identity;
using System;

using Microsoft.AspNet.SignalR;
using Microsoft.AspNetCore.Http;
using Nekretnine.Web.Hubs;


namespace Nekretnine
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
            services.AddControllersWithViews();
            services.AddDbContext<MyContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Azure")));
            services.AddSignalR().AddHubOptions<NotificationHub>(options =>
            {
                options.EnableDetailedErrors = true;
            });
            services.AddRazorPages()
                 .AddMvcOptions(options =>
                 {
                     options.MaxModelValidationErrors = 50;
                     options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(
                         _ => "Ovo polje je obavezno! ");
                 });
           
            services.AddDistributedMemoryCache();
            services.AddSession();
           



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        [Obsolete]
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
              

            });
            app.UseSignalR(routes =>
            {
                
                routes.MapHub<NotificationHub>("/notificationHub"); //Primjer
            });



        }
    }
}
