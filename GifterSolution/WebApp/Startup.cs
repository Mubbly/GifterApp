using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Contracts.DAL.App;
using DAL.App.EF;
using Domain.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace WebApp
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
            services.AddDbContext<AppDbContext>(options =>
                options.UseMySql(
                    Configuration.GetConnectionString("MySqlConnection"))); // CUSTOM DB CONNECTION STRING

            services.AddScoped<IAppUnitOfWork, AppUnitOfWork>();

            services.AddIdentity<AppUser, AppRole>()
                .AddDefaultUI()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
            
            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsAllowAll",
                    builder =>
                    {
                        builder.AllowAnyOrigin();
                        builder.AllowAnyHeader();
                        builder.AllowAnyMethod();
                    });
            });
            
            // =============== JWT support ===============
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims
            services
                .AddAuthentication()
                .AddCookie(options => { options.SlidingExpiration = true; })
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = Configuration["JWT:Issuer"],
                        ValidAudience = Configuration["JWT:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:SigningKey"])),
                        ClockSkew = TimeSpan.Zero // remove delay of token when expire
                    };
                });

            // TODO: Enable later and update ApiControllers with versions
            // services.AddApiVersioning(options =>
            // {
            //     options.ReportApiVersions = true;
            //     // bad idea?:
            //     //options.DefaultApiVersion = new ApiVersion(1,0);
            //     //options.AssumeDefaultVersionWhenUnspecified = false;
            // });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            UpdateDatabase(app, env, Configuration);

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

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            
            app.UseCors("CorsAllowAll");

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
        
        // CUSTOM CODE START
        private static void UpdateDatabase(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration Configuration)
        {
            // Give me the scoped services (everything created by it will be closed at the end of the service scope life)
            using var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope();

            // Use one context for the db in this whole function
            using var ctx = serviceScope.ServiceProvider.GetService<AppDbContext>();

            // Set up user and role managers for Identity
            using var userManager = serviceScope.ServiceProvider.GetService<UserManager<AppUser>>();
            using var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<AppRole>>();
            
            // Here you can do whatever you need .. for example migrate each time so don't have to update db manually
            // ctx.Database.EnsureDeleted(); // Drop current db if you want to start from the scratch every time
            // ctx.Database.Migrate(); // Add the new migration. Will automatically create db if not there. If only this is needed don't do the dropping step.
            // These could also be done in configurations instead - ifs regarding it here

            if (Configuration.GetValue<bool>("AppDataInitialization:DeleteDatabase"))
            {
                DAL.App.EF.Helpers.DataInitializers.DeleteDatabase(ctx);
            }
            if (Configuration.GetValue<bool>("AppDataInitialization:MigrateDatabase"))
            {
                DAL.App.EF.Helpers.DataInitializers.MigrateDatabase(ctx);
            }
            if (Configuration.GetValue<bool>("AppDataInitialization:SeedIdentity"))
            {
                DAL.App.EF.Helpers.DataInitializers.SeedIdentity(userManager, roleManager);
            }
            if (Configuration.GetValue<bool>("AppDataInitialization:SeedData"))
            {
                DAL.App.EF.Helpers.DataInitializers.SeedData(ctx);
            }
            //ctx.Database.EnsureDeleted();
            //ctx.Database.Migrate();
        }
        // CUSTOM CODE END
    }
}