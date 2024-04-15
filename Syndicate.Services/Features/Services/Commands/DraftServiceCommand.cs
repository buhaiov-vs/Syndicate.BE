using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Syndicate.Data;
using Syndicate.Data.Enums;
using Syndicate.Data.Models;
using Syndicate.Services.Extensions;
using Syndicate.Services.Features.Services.Models.Requests;
using Syndicate.Services.Features.Services.Models.Responses;
using System.Net;

namespace Syndicate.Services.Features.Services.Commands;

public class DraftServiceCommand(
    AppDbContext appDbContext,
    IHttpContextAccessor httpContextAccessor,
    IValidator<DraftServiceRequest> validator,
    ILogger<DraftServiceCommand>logger)
{
    private readonly HttpContext _httpContext = httpContextAccessor.HttpContext!;

    public async Task<ApiResponse<CreateServiceResponse>> ExecuteAsync(DraftServiceRequest request, CancellationToken cancelationToken = default)
    {
        validator.ValidateAndThrow(request);

        var userId = _httpContext.User.GetId();

        if(appDbContext.Services.Any(x => x.OwnerId == userId && x.Name.ToLower() == request.Name.ToLowerInvariant()))
        {
            return ApiResponse<CreateServiceResponse>.Fail(HttpStatusCode.BadRequest, "Service with the following name already exists");
        }

        var entity = new Service()
        {
            Name = request.Name,
            OwnerId = userId,
            Status = ServiceStatus.Draft,
            CreatedOn = DateTime.UtcNow
        };

        appDbContext.Services.Add(entity);
        await appDbContext.SaveChangesAsync(cancelationToken);

        return ApiResponse<CreateServiceResponse>.Happy(new() { Id = entity.Id, Status = entity.Status });
    }
}
