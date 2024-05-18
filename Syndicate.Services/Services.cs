using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Syndicate.Services.Features.Categories;
using Syndicate.Services.Features.Services.Validators;

namespace Syndicate.Services;

public static class Services
{
    public static void Register(IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();
        services.AddValidatorsFromAssemblyContaining<UpdateServiceRequestValidator>();

        Categories.Register(services, configuration);
        Features.Services.ServicesModule.Register(services, configuration);
    }
}
