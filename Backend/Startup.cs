using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.Threading.Tasks;
using DDDSample1.Infrastructure;
using DDDSample1.Infrastructure.Categories;
using DDDSample1.Infrastructure.Products;
using DDDSample1.Infrastructure.Families;
using DDDSample1.Infrastructure.Shared;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Categories;
using DDDSample1.Domain.Products;
using DDDSample1.Domain.Families;
using DDDSample1.Users;
using DDDSample1.Infrastructure.Users;
using DDDSample1.Domain.Users;
using Microsoft.AspNetCore.Authentication;

namespace DDDSample1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DDDSample1DbContext>(options =>
                options.UseMySql(Configuration.GetConnectionString("MySqlConnection"),
                    new MySqlServerVersion(new Version(8, 0, 0)))
                .ReplaceService<IValueConverterSelector, StronglyEntityIdValueConverterSelector>());

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
            })
            .AddCookie()
            .AddGoogle(GoogleDefaults.AuthenticationScheme, options =>
            {
                options.ClientId = Configuration.GetValue<string>("GoogleKeys:ClientId");
                options.ClientSecret = Configuration.GetValue<string>("GoogleKeys:ClientSecret");

                options.Events.OnCreatingTicket = async context =>
                {
                    var email = context.Principal.FindFirstValue(ClaimTypes.Email);

                    var userService = context.HttpContext.RequestServices.GetRequiredService<UserService>();
                    var user = await userService.FindByEmailAsync(email);

                    if (user == null)
                    {
                        Console.WriteLine("Login falhou: email não encontrado.");

                        await context.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                        context.Fail("Este email não está registrado no sistema.");
                        context.Response.StatusCode = 302;
                        context.Response.Headers["Location"] = "/api/login";
                        await context.Response.CompleteAsync();

                        return;
                    }else{

                        Console.WriteLine("Login bem-sucedido: email encontrado.");
                    }

                    var identity = (ClaimsIdentity)context.Principal.Identity;
                    identity.AddClaim(new Claim("urn:google:access_token", context.AccessToken));
                    identity.AddClaim(new Claim("urn:google:expires_in", context.ExpiresIn.ToString()));
                    identity.AddClaim(new Claim(ClaimTypes.Role, user.Role.ToString()));
                };
            });
            ConfigureMyServices(services);

            services.AddControllers().AddNewtonsoftJson();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
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

        public void ConfigureMyServices(IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<CategoryService>();

            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<ProductService>();

            services.AddTransient<IFamilyRepository, FamilyRepository>();
            services.AddTransient<FamilyService>();

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<UserService>();
        }
    }
}
