using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Syncfusion.Licensing;
using System.Collections.Generic;
using System.Globalization;
using VinarishMvc.Areas.Identity.Models;
using VinarishMvc.Data;
namespace VinarishMvc
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            SyncfusionLicenseProvider.RegisterLicense("MDAxQDMxMzcyZTMxMmUzMFNaZHJaa0JqRjBNa3RXNi9lMTZBUi92NEl4bEFYcGNuK2c4UDVMTjlxcnM9");
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
            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("fa-IR");
                options.SupportedCultures = new List<CultureInfo> { new CultureInfo("fa-IR") };
            });
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")).UseLazyLoadingProxies());
            services.AddDbContext<Models.VinarishContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")).UseLazyLoadingProxies());
            services.AddIdentity<VinarishUser, IdentityRole<Guid>>()
                .AddDefaultUI(UIFramework.Bootstrap4)
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                                 .RequireAuthenticatedUser()
                                 .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminAccess", policy => policy.RequireRole("Admin"));

                options.AddPolicy("ManagerAccess", policy =>
                    policy.RequireAssertion(context =>
                                context.User.IsInRole("Admin")
                                || context.User.IsInRole("Manager")));

                options.AddPolicy("UserAccess", policy =>
                    policy.RequireAssertion(context =>
                                context.User.IsInRole("Admin")
                                || context.User.IsInRole("Manager")
                                || context.User.IsInRole("User")));
            });
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            var cultureInfo = new CultureInfo("fa-IR");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                  name: "Authentication",
                  template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
