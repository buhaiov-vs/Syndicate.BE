using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Syndicate.Services.Features.Categories;
using Syndicate.Services.Features.Services.Validators;

namespace Syndicate.Services;

public static class Services
{
    public static void Register(IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddValidatorsFromAssemblyContaining<UpdateServiceRequestValidator>();

        Categories.Register(services);
        Features.Services.ServicesModule.Register(services);
    }
}
