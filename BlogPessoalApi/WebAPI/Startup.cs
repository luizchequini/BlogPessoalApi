using Data.Config;
using Data.Entidades;
using Data.Interfaces;
using Data.Repositorios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Threading.Tasks;
using WebAPI.Token;

namespace WebAPI
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
            //services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseSqlServer(
            //        Configuration.GetConnectionString("DefaultConnection")));
            //services.AddDatabaseDeveloperPageExceptionFilter();

            //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //    .AddEntityFrameworkStores<ApplicationDbContext>();
            //services.AddControllersWithViews();

            services.AddDbContext<ContextBase>(options =>
                          options.UseSqlServer(
                              Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ContextBase>();

            services.AddControllersWithViews();

            services.AddSingleton(typeof(IGeneric<>), typeof(RepositoryGenerics<>));
            services.AddSingleton<ICategoria, RepositoryCategoria>();
            services.AddSingleton<INoticia, RepositoryNoticia>();
            services.AddSingleton<INoticiaTag, RepositoryNoticiaTag>();
            services.AddSingleton<ITag, RepositoryTag>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
           .AddJwtBearer(option =>
           {
               option.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuer = false,
                   ValidateAudience = false,
                   ValidateLifetime = true,
                   ValidateIssuerSigningKey = true,

                   ValidIssuer = "Teste.Securiry.Bearer",
                   ValidAudience = "Teste.Securiry.Bearer",
                   IssuerSigningKey = JwtSecurityKey.Create("Secret_Key-12345678")
               };

               option.Events = new JwtBearerEvents
               {
                   OnAuthenticationFailed = context =>
                   {
                       Console.WriteLine("OnAuthenticationFailed: " + context.Exception.Message);
                       return Task.CompletedTask;
                   },
                   OnTokenValidated = context =>
                   {
                       Console.WriteLine("OnTokenValidated: " + context.SecurityToken);
                       return Task.CompletedTask;
                   }
               };
           });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
