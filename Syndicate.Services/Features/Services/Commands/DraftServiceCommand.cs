using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Syndicate.Data;
using Syndicate.Data.Enums;
using Syndicate.Data.Models.ServiceFeature;
using Syndicate.Services.Extensions;
using Syndicate.Services.Features.Services.Models.Requests;
using Syndicate.Services.Features.Services.Models.Responses;
using System.Net;

namespace Syndicate.Services.Features.Services.Commands;

public class DraftServiceCommand(
    AppDbContext appDbContext,
    IHttpContextAccessor httpContextAccessor,
    IValidator<DraftServiceRequest> validator, /// <see cref="Validators.DraftServiceRequestValidator"/>
    ILogger<DraftServiceCommand> logger)
{
    private readonly HttpContext _httpContext = httpContextAccessor.HttpContext!;

    public async Task<ApiResponse<UpdateServiceResponse>> ExecuteAsync(DraftServiceRequest request, CancellationToken cancelationToken = default)
    {
        validator.ValidateAndThrow(request);

        var userId = _httpContext.User.GetId();

        if (appDbContext.Services.Any(x => x.OwnerId == userId && x.NormalizedName == request.Name.ToUpper()))
        {
            return new(HttpStatusCode.BadRequest, "Service with the following name already exists");
        }

        var entity = new Service()
        {
            Name = request.Name,
            NormalizedName = request.Name.ToUpper(),
            OwnerId = userId,
            Status = ServiceStatus.Draft,
            CreatedOn = DateTime.UtcNow
        };

        appDbContext.Services.Add(entity);
        await appDbContext.SaveChangesAsync(cancelationToken);

        return new(new() { Id = entity.Id, Status = ServiceStatus.Draft });
    }
}
