using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Syndicate.Services.Features.Identity;

namespace Syndicate.Services;

public class Options
{
    public static void Register(IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions).Replace("Options", "")));
    }
}
