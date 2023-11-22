using eCommerce.Storefront.Repository.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using eCommerce.Storefront.Services;
using Microsoft.AspNetCore.Identity;
using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace eCommerce.Storefront.UI.Web.MVC
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperBootStrapper));
            services.AddHttpContextAccessor();
            services.AddDbContext<ShopDataContext>(options => 
            {
                if (_env.IsDevelopment())
                {
                    options.UseSqlite(_configuration?.GetConnectionString("DefaultConnection"), b => 
                    {
                        b.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                        b.MigrationsAssembly("eCommerce.Storefront.Repository.EntityFrameworkCore");
                    });
                }
                else
                {
                    options.UseSqlServer(_configuration?.GetConnectionString("DefaultConnection"), b => 
                    {
                        b.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                        b.MigrationsAssembly("eCommerce.Storefront.Repository.EntityFrameworkCore");
                    });
                }
            });
            services.AddDefaultIdentity<IdentityUser>().AddRoles<IdentityRole>().AddEntityFrameworkStores<ShopDataContext>();
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
                        options.ExpireTimeSpan = TimeSpan.FromMinutes(double.Parse(_configuration["CookieAuthenticationTimeout"]));
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
            services.AddControllersWithViews(options => 
            {
                options.ModelBinderProviders.RemoveType<DateTimeModelBinderProvider>();
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            }).AddNewtonsoftJson();
            services.ConfigureDependencies();
            services.AddAntiforgery(options =>
            {
                options.HeaderName = "RequestVerificationToken";
            });
            services.AddLogging(configure => 
            {
                configure.AddConfiguration(_configuration.GetSection("Logging"));
                configure.AddConsole();
                configure.AddDebug();
                configure.AddEventSourceLogger();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
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
                if (!context.Response.Headers.ContainsKey("X-content-type-options"))
                {
                    context.Response.Headers.Append("X-content-type-options", "nosniff");
                }

                if (!context.Response.Headers.ContainsKey("Cache-control"))
                {
                    context.Response.Headers.Append("Cache-control", "no-cache, no-store");
                }

                if (!context.Response.Headers.ContainsKey("Pragma"))
                {
                    context.Response.Headers.Append("Pragma", "no-cache");
                }

                if (!context.Response.Headers.ContainsKey("X-XSS-Protection"))
                {
                    context.Response.Headers.Append("X-XSS-Protection", "1; mode=block");
                }

                if (context.Request.Path.Value.Contains("/admin"))
                {
                    context.Response.Redirect($"{context.Request.Scheme}://{context.Request.Host.Value}/index.html");
                
                    return;
                }

                await next();
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            
            logger.LogInformation("Application Started");
        }
    }
}
