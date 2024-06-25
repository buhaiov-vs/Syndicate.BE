using Microsoft.Extensions.DependencyInjection;
using Syndicate.Services.Features.Services.Commands;
using Syndicate.Services.Features.Services.Queries;

namespace Syndicate.Services.Features.Services;

public static class ServicesModule
{
    public static void Register(IServiceCollection services)
    {
        RegisterCommands(services);
        RegisterQueries(services);
    }

    private static void RegisterCommands(IServiceCollection services)
    {
        services.AddScoped<UpdateServiceCommand>();
        services.AddScoped<DraftServiceCommand>();
        services.AddScoped<DeleteServiceCommand>();
        services.AddScoped<PublishServiceCommand>();
        services.AddScoped<DeactivateServiceCommand>();
    }

    private static void RegisterQueries(IServiceCollection services)
    {
        services.AddScoped<GetServicesForListQuery>();
        services.AddScoped<GetServiceQuery>();
    }
}
