using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PracaDyplomowa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracaDyplomowa
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



            services.AddControllers();


            //Because of icrosoft.AspNetCore.HttpsPolicy.HttpsRedirectionMiddleware => Failed to determine the https port for redirect.
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders =
                    ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });






            //For Authentication
            services.AddAuthentication("Ciastko").AddCookie("Ciastko", options =>
            {
                options.Cookie.Name = "Ciastko";
                options.LoginPath = "/Account/Login";
                //options.AccessDeniedPath =
                //options.ExpireTimeSpan = TimeSpan.FromSeconds(10); //closing the browser deletes the cookie

            });
            services.AddRazorPages();
            //For Ajax
            services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");

            //For background action
            services.AddHostedService<BackgroundProcess>();

            //Configure oracle connection
            services.AddHostedService<OracleConnectionConfiguration>();

            //For realTime communication @signalR
            services.AddSignalR();

            //For accessing logged unit id from Models classes
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            /*
                public string HttpCntx // for accesing unit id
                {
                    get
                    {
                        IHttpContextAccessor _httpContextAccessor = new HttpContextAccessor();
                        return _httpContextAccessor.HttpContext.User.FindFirst(x => x.Type == "Unit").Value;
                    }
                }
             */
            // test for Razor.RuntimeCompilation
            services.AddRazorPages().AddRazorRuntimeCompilation();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            //test for polish characters
            var supportedCultures = new string[] { "en-US", "pl-PL" };
            app.UseRequestLocalization(options =>
                options
                    .AddSupportedCultures(supportedCultures)
                    .AddSupportedUICultures(supportedCultures)
                    .SetDefaultCulture("pl-PL")
            );
            //test for polish characters




            //Because of icrosoft.AspNetCore.HttpsPolicy.HttpsRedirectionMiddleware => Failed to determine the https port for redirect.
            app.UseForwardedHeaders();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //test
            app.Use((context, next) =>
            {
                context.Request.Scheme = "https";
                return next();
            });
            //test

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapHub<Hubs.NotificationHub>("/NotificationHub");  //signalR
            });


        }
    }
}
