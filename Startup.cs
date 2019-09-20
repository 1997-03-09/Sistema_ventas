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
using Sistem_Ventas.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sistem_Ventas.Servicios.Interface;
using Sistem_Ventas.Library;

namespace Sistem_Ventas
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

            //services.AddDefaultIdentity<IdentityUser>()
            //    .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

           // services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
services.AddMvc(options =>

           options.EnableEndpointRouting = false).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
           
            services.ConfigureApplicationCookie(options => {
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromDays(1);
                options.LoginPath = "/Home/Index";
            });

            services.AddSession(options => {

                options.Cookie.Name = ".SystemVentas.Session";
                options.IdleTimeout = TimeSpan.FromHours(12);
            });
            /*Un objeto Singleton creado cuando lo solicite el primer componente y compartido con todos los componentes que lo requieran a continuación en el contexto de la misma petición. Pero, además, con la particularidad de que será liberado explícitamente por el framework al terminar el ámbito de ejecución, o scope, en el que se enmarca el proceso actual. En la práctica, esto significa que el framework recordará los objetos que ha ido creando según este modelo de funcionamiento y cuando finalice el proceso de la petición invocará a sus respectivos métodos Dispose() para liberar recursos. */
            services.AddScoped<IListObject, ListObject>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSession();
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

            // app.UseStatusCodePages();
            app.UseStatusCodePagesWithReExecute("/Error/Error", "?statusCode={0}");
            // app.UseStatusCodePagesWithRedirects("/Error");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapAreaRoute("Principal", "Principal", "{controller=Principal}/{action=Index}/{id?}");

                routes.MapAreaRoute("Usuarios", "Usuarios", "{controller=Usuarios}/{action=Index}/{id?}");

                routes.MapAreaRoute("Clientes", "Clientes", "{controller=Clientes}/{action=Index}/{id?}");

                routes.MapAreaRoute("Proveedores", "Proveedores", "{controller=Proveedores}/{action=Index}/{id?}");

                routes.MapAreaRoute("Compras", "Compras", "{controller=Compras}/{action=Index}/{id?}");

                routes.MapAreaRoute("Productos", "Productos", "{controller=Productos}/{action=Index}/{id?}");

                routes.MapAreaRoute("Cajas", "Cajas", "{controller=Cajas}/{action=Index}/{id?}");

                routes.MapAreaRoute("Ventas", "Ventas", "{controller=Ventas}/{action=Ventas}/{id?}");
            });
        }
    }
}
 