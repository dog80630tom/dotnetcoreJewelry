using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Jewelry.Respository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Jewelry.Models;

namespace Jewelry
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
            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.HttpOnly = true;
                // Make the session cookie essential
                options.Cookie.IsEssential = true;
            });
            services.AddDbContext<DataContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DatabaseContext")));
            services.AddScoped(typeof(ICRUD<>), typeof(CRUDRespository<>));
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
         
            app.UseAuthorization();
            app.UseCookiePolicy();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "SearchProducts",
                    defaults: "Products/{query}",
                    pattern: "{controller=FrontEnd}/{action=ShowProducts}/{id?}");
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "Earrings",
                    defaults: "Products/Earrings/{id}",
                    pattern: "{controller=FrontEnd}/{action=ProductIndex}/{id?}");
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "Bracelets",
                    defaults: "Products/Bracelets/{id}",
                    pattern: "{controller=FrontEnd}/{action=ProductIndex}/{id?}");
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "Necklaces",
                    defaults: "Products/Necklaces/{id}",
                    pattern: "{controller=FrontEnd}/{action=ProductIndex}/{id?}");
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "Cart",
                    defaults: "Cart",
                    pattern: "{controller=FrontEnd}/{action=Cart}/{id?}");
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "Checkout",
                    defaults: "Checkout",
                    pattern: "{controller=FrontEnd}/{action=Checkout}/{id?}");
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "Login",
                    defaults: "Login",
                    pattern: "{controller=FrontEnd}/{action=LoginPage}/{id?}");
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "ForgetPassword",
                    defaults: "Login/ForgotPassword",
                    pattern: "{controller=FrontEnd}/{action=ForgetPassword}/{id?}");
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "Order",
                    defaults: "Order",
                    pattern: "{controller=FrontEnd}/{action=MemberPageOrder}/{id?}");
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "OrderDetail",
                    defaults: "Order/{id}",
                    pattern: "{controller=FrontEnd}/{action=MemberPageOrderDetail}/{id?}");
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "MemberSetting",
                    defaults: "Member",
                    pattern: "{controller=FrontEnd}/{action=MemberPageSetting}/{id?}");
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "MemberRegist",
                    defaults: "Regist",
                    pattern: "{controller=FrontEnd}/{action=MemberRegist}/{id?}");
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "MemberWishList",
                    defaults: "WishList",
                    pattern: "{controller=FrontEnd}/{action=MemberPageWishlist}/{id?}");
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "ProductManagement",
                    defaults: "AdminProduct",
                    pattern: "{controller=NewProduct}/{action=Index}");
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "MemberManagement",
                    defaults: "AdminMember",
                    pattern: "{controller=Members}/{action=Index}");
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "CategoryManagement",
                    defaults: "AdminCategory",
                    pattern: "{controller=Category}/{action=Index}");

            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "ChartManagement",
                    defaults: "AdminChart",
                    pattern: "{controller=Home}/{action=Index}");

            });
         
        }
    }
}
