using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Syndicate.Data;
using Syndicate.Data.Models;
using Syndicate.Services.Features.Identity.Commands;
using Syndicate.Services.Features.Identity.Queries;

namespace Syndicate.Services.Features.Identity;
public static class IdentityModule
{
    public static void Register(IServiceCollection services, IConfiguration configuration)
    {
        services.AddIdentity<User, Role>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddSignInManager<SignInManager<User>>()
            .AddUserManager<UserManager<User>>();

        services.Configure<IdentityOptions>(options =>
        {
            if (configuration.GetSection("Environment").GetValue("Local", false))
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 1;
            }
        });

        services.ConfigureApplicationCookie(options =>
        {
            options.Cookie.Name = "SyndicateAuth";
            options.LoginPath = "/identity/login";
            options.AccessDeniedPath = "/identity/error";
            options.Cookie.HttpOnly = true;
            options.Cookie.SecurePolicy = configuration.GetSection("Environment").GetValue("Local", false) ? CookieSecurePolicy.None : CookieSecurePolicy.Always;
            options.Cookie.SameSite = SameSiteMode.Lax;
        });

        services.AddAuthorization();

        services.AddScoped<LoginQuery>();
        services.AddScoped<SignupCommand>();

    }
}
