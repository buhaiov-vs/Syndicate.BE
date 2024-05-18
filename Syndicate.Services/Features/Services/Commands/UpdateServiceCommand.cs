using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Syndicate.Data;
using Syndicate.Data.Models;
using Syndicate.Services.Exceptions;
using Syndicate.Services.Extensions;
using Syndicate.Services.Features.Services.Models.Requests;
using Syndicate.Services.Features.Services.Models.Responses;
using System.Net;

namespace Syndicate.Services.Features.Services.Commands;

/// <see cref="Validators.UpdateServiceRequestValidator"/>
public class UpdateServiceCommand(AppDbContext appDbContext, IHttpContextAccessor httpContextAccessor, IValidator<UpdateServiceRequest> validator, ILogger<UpdateServiceCommand> logger)
{
    private readonly HttpContext _httpContext = httpContextAccessor?.HttpContext ?? throw new MissedHttpContextException();
    private const int TagsMaxCount = 10;

    public async Task<ApiResponse<UpdateServiceResponse>> ExecuteAsync(UpdateServiceRequest request, CancellationToken cancelationToken = default)
    {
        validator.ValidateAndThrow(request);
        var userId = _httpContext.User.GetId();

        var serviceToUpdate = await appDbContext.Services.Where(s => s.OwnerId == userId && s.Id == request.Id).AsTracking().FirstOrDefaultAsync(cancelationToken);

        if (serviceToUpdate is null)
        {
            return new(HttpStatusCode.NotFound, "Requested service not found");
        }

        serviceToUpdate.Name = request.Name;
        serviceToUpdate.Description = request.Description;
        serviceToUpdate.UpdatedOn = DateTime.UtcNow;
        serviceToUpdate.UpdatedBy = userId;
        serviceToUpdate.Duration = request.Duration;
        serviceToUpdate.Price = request.Price;
        UpdateTags(serviceToUpdate, request.Tags);

        await appDbContext.SaveChangesAsync(cancelationToken);

        return new();
    }

    private static void UpdateTags(Service serviceToUpdate, List<string> newTags)
    {
        var nameToTags = serviceToUpdate.Name.Trim().Replace(",", "").Split(" ");
        var tags = nameToTags.Union(newTags.Distinct())
            .Take(nameToTags.Length + TagsMaxCount)
            .Select(x => new Tag { Name = x });

        serviceToUpdate.Tags = tags.ToList();
    }
}
