using eCommerce.Storefront.Repository.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;
using eCommerce.Storefront.Services;
using Microsoft.AspNetCore.Identity;
using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using Infrastructure.EntityFrameworkCore;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace eCommerce.Storefront.UI.Web.MVC
{
    public class Startup
    {
        private readonly IApplicationSettings _applicationSettings;
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _applicationSettings = new AppConfigSettings();
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperBootStrapper));
            services.AddHttpContextAccessor();
            services.AddDbContext<DataContext, ShopDataContext>(options => 
            {
                options.UseSqlServer(_applicationSettings?.ConnectionString, b => b.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
            });
            services.AddDefaultIdentity<IdentityUser>().AddRoles<IdentityRole>().AddEntityFrameworkStores<DataContext>();
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 1;
                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;
                // User settings.
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
            });
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(options =>
                    {
                        // Cookie settings
                        options.Cookie.HttpOnly = true;
                        options.ExpireTimeSpan = TimeSpan.FromMinutes(_applicationSettings?.CookieAuthenticationTimeout ?? 30);
                        options.LoginPath = "/AccountLogOn/LogOn";
                        options.AccessDeniedPath = "/AccountRegister/Register";
                        options.SlidingExpiration = true;
                    }).AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = _configuration["Jwt:Issuer"],
                            ValidAudience = _configuration["Jwt:Audience"],
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecurityKey"]))
                        };
                    });
            services.AddControllersWithViews(options => options.ModelBinderProviders.RemoveType<DateTimeModelBinderProvider>()).AddNewtonsoftJson();
            services.AddSingleton(_applicationSettings);
            services.ConfigureDependencies();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
                              IWebHostEnvironment env, 
                              ILoggerFactory loggerFactory,
                              Infrastructure.Logging.ILogger logger,
                              DataContext dataContext)
        {
            loggerFactory.AddLog4Net(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Logging", "log4net.config"));

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();            
            app.UseStaticFiles();

            var supportedCultures = new[] { "en-GB", "en-US", "it-IT" };
            var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[0])
                                                                      .AddSupportedCultures(supportedCultures)
                                                                      .AddSupportedUICultures(supportedCultures);

            app.UseRequestLocalization(localizationOptions);
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
                context.Response.Headers.Add("Cache-control", "no-store");
                context.Response.Headers.Add("Pragma", "no-cache");
                context.Response.Headers.Add("X-XSS-Protection", "1; mode=block");
                await next();
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllers();
            });
            
            AutoMigration autoMigration = new AutoMigration(dataContext, logger);
            
            autoMigration.Migrate().GetAwaiter().GetResult();
            logger.Log("Application Started");
        }
    }
}
