using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Syndicate.Services.Features.Categories.Queries;

namespace Syndicate.Services.Features.Categories;

public static class Categories
{
    public static void Register(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<GetCategoriesQuery>();
    }
}
