using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Syndicate.Services.Features.Services.Commands;
using Syndicate.Services.Features.Services.Queries;

namespace Syndicate.Services.Features.Services;

public static class ServicesModule
{
    public static void Register(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<GetDraftServicesQuery>();
        services.AddScoped<GetServiceQuery>();
        services.AddScoped<UpdateServiceCommand>();
        services.AddScoped<DraftServiceCommand>();
        services.AddScoped<DeleteServiceCommand>();
    }
}
