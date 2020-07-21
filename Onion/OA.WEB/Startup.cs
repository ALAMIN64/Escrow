using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using OA.DATA.Entities;
using OA.REPO;
using OA.Service;
using RugerRumble.Services;

namespace OA.WEB
{
    public class Startup
    {
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _env;


        public Startup(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContext<OA.REPO.ApplicationDBContext>(option => option.UseSqlServer(Configuration.GetConnectionString("MSSqlConnection")));
            services.AddIdentity<ApplicationUser, IdentityRole>(
              options =>
              {
                  options.Password.RequireDigit = false;
                  options.Password.RequiredLength = 6;
                  options.Password.RequireLowercase = false;
                  options.Password.RequireUppercase = false;
                  options.Password.RequireNonAlphanumeric = false;
              }).AddEntityFrameworkStores<ApplicationDBContext>()
              .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(
                options =>
                {
                    options.LoginPath = new PathString("/Account/Login");
                    options.AccessDeniedPath = new PathString("/Account/AccessDenied");
                    options.LogoutPath = new PathString("/Account/Logout");
                });
         
            services.Configure<ForwardedHeadersOptions>(options => options.ForwardedHeaders =
                      ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto);
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IDeliveryTypeService, DeliveryTypeService>();
            services.AddTransient<ITransactionService, TransactionService>();
            services.AddTransient<ITransactionLinkService, TransactionLinkService>();
            services.AddTransient<IPaymentService, PaymentService>();
            services.AddTransient<IAboutService, AboutService>();
            services.AddTransient<IPolicyService, PolicyService>();
            services.AddTransient<ITermsService, TermsService>();
            services.AddTransient<IFeeService, FeeService>();
            services.AddTransient<IContactService, ContactService>();
            services.AddTransient<IPinfoService, PinfoService>();
            services.AddTransient<IHomeService, HomeService>();
            services.AddMemoryCache();
            services.AddSession();
            services.AddSession(); services.AddSingleton<EmailSenderHostedService>();
            services.AddSingleton<IHostedService>(serviceProvider => serviceProvider.GetService<EmailSenderHostedService>());
            services.AddSingleton<IEmailSender>(serviceProvider => serviceProvider.GetService<EmailSenderHostedService>());
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

                app.UseHttpsRedirection();

                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();

            }
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
