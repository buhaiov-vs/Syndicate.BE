using Microsoft.AspNetCore.Mvc;
using Syndicate.Data.Enums;
using Syndicate.Services.Features.Categories.Queries;
using Syndicate.Services.Features.Identity.Commands;
using Syndicate.Services.Features.Identity.Models.Requests;
using Syndicate.Services.Features.Identity.Queries;
using Syndicate.Services.Features.Services.Commands;
using Syndicate.Services.Features.Services.Models.Requests;
using Syndicate.Services.Features.Services.Queries;

namespace Syndicate.API;

public static class Endpoints
{
    public static void Register(WebApplication app)
    {
        RegisterServicesEndpoints(app);
        RegisterIdentityEndpoints(app);
        RegisterCategoriesEndpoints(app);
    }

    private static void RegisterServicesEndpoints(WebApplication app)
    {
        app.MapGet(Routes.Services.Exact("id"),
            ([FromServices] GetServiceQuery query,
            [FromRoute] Guid id,
            CancellationToken cancellationToken)
            => query.ExecuteAsync(id, cancellationToken))
            .AllowAnonymous();

        app.MapPost(Routes.Services.Base,
            ([FromServices] UpdateServiceCommand command,
            [FromBody] UpdateServiceRequest request,
            CancellationToken cancellationToken)
            => command.ExecuteAsync(request, cancellationToken))
            .RequireAuthorization();

        app.MapDelete(Routes.Services.Base,
            ([FromServices] DeleteServiceCommand command,
            [FromBody] DeleteServiceRequest request,
            CancellationToken cancellationToken)
            => command.ExecuteAsync(request, cancellationToken))
            .RequireAuthorization();

        app.MapGet(Routes.Services.Base,
            ([FromServices] GetServicesForListQuery query,
            CancellationToken cancellationToken)
            => query.ExecuteAsync(cancellationToken))
            .RequireAuthorization();

        app.MapPost(Routes.Services.Draft,
            ([FromServices] DraftServiceCommand command,
            [FromBody] DraftServiceRequest request,
            CancellationToken cancellationToken)
            => command.ExecuteAsync(request, cancellationToken))
            .RequireAuthorization();

        app.MapPost(Routes.Services.Publish("id"),
            ([FromServices] PublishServiceCommand command,
            [FromRoute] Guid id,
            CancellationToken cancellationToken)
            => command.ExecuteAsync(id, cancellationToken))
            .RequireAuthorization();

        app.MapPost(Routes.Services.Deactivate("id"),
            ([FromServices] DeactivateServiceCommand command,
            [FromRoute] Guid id,
            CancellationToken cancellationToken)
            => command.ExecuteAsync(id, cancellationToken))
            .RequireAuthorization();
    }

    private static void RegisterCategoriesEndpoints(WebApplication app)
    {
        app.MapGet(Routes.Categories.Base,
            ([FromServices] GetCategoriesQuery query,
            CancellationToken cancellationToken)
            => query.ExecuteAsync(cancellationToken))
            .RequireAuthorization();
    }

    private static void RegisterIdentityEndpoints(WebApplication app)
    {
        app.MapPost(Routes.Identity.Signin,
            ([FromServices] LoginQuery query,
            [FromBody] LoginRequest request)
            => query.ExecuteAsync(request))
            .AllowAnonymous();

        app.MapPost(Routes.Identity.Signup,
            ([FromServices] SignupCommand command,
            [FromBody] SignupRequest request)
            => command.ExecuteAsync(request))
            .AllowAnonymous();
    }
}
