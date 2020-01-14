using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dispatch_system.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Dispatch_system.Models;
using Dispatch_system.Services;

namespace Dispatch_system
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            // wczesne zarejestrowanie tego serwisu pozwoli mi na dostęp
            // do właściwości użytkowników, np. Id
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // wywołanie jakiejkowiek metody z interfejsu IEmployeeService
            // wyzwala metodę z klasy SQLEmployeeService
            services.AddScoped<IEmployeeSerivce, SQLEmployeeService>();

            // wywołanie jakiejkowiek metody z interfejsu IClientParcelService
            // wyzwala metodę z klasy SQLClientParcelService
            services.AddScoped<IClientParcelService, SQLClientParcelService>();

            // wywołanie jakiejkowiek metody z interfejsu IBranchParcelService
            // wyzwala metodę z klasy SQLBranchParcelService
            services.AddScoped<IBranchParcelService, SQLBranchParcelService>();

            // wywołanie jakiejkowiek metody z interfejsu IAccountService
            // wyzwala metodę z klasy SQLAccountService
            services.AddScoped<IAccountService, SQLAccountService>();

            // wywołanie jakiejkowiek metody z interfejsu ICourierService
            // wyzwala metodę z klasy SQLCourierService
            services.AddScoped<ICourierService, SQLCourierService>();

            // wywołanie jakiejkowiek metody z interfejsu IWarehouseService
            // wyzwala metodę z klasy SQLWarehouseService
            services.AddScoped<IWarehouseService, SQLWarehouseService>();

            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 2;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
