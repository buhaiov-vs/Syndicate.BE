using Azure;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Syndicate.Data;
using Syndicate.Data.Models.ServiceFeature;
using Syndicate.Data.Models.TagFeature;
using Syndicate.Services.Exceptions;
using Syndicate.Services.Extensions;
using Syndicate.Services.Features.Services.Models.Requests;
using Syndicate.Services.Features.Services.Models.Responses;
using System.Collections.Frozen;
using System.Net;
using System.Threading;

namespace Syndicate.Services.Features.Services.Commands;

/// <see cref="Validators.UpdateServiceRequestValidator"/>
public class UpdateServiceCommand(AppDbContext _appDbContext, IHttpContextAccessor _httpContextAccessor, IValidator<UpdateServiceRequest> _validator, ILogger<UpdateServiceCommand> _logger)
{
    private readonly HttpContext _httpContext = _httpContextAccessor?.HttpContext ?? throw new MissedHttpContextException();

    public async Task<ApiResponse<UpdateServiceResponse>> ExecuteAsync(UpdateServiceRequest request, CancellationToken cancellationToken = default)
    {
        _validator.ValidateAndThrow(request);
        var userId = _httpContext.User.GetId();

        var transaction = await _appDbContext.Database.BeginTransactionAsync(cancellationToken);
        var serviceToUpdate = await _appDbContext.Services.Where(s => s.OwnerId == userId && s.Id == request.Id).Include(x => x.Tags).AsTracking().FirstOrDefaultAsync(cancellationToken);

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
        await UpdateTags(serviceToUpdate, request.Tags);

        await _appDbContext.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);

        return new();
    }

    private async Task UpdateTags(Service serviceToUpdate, List<string> requestTags, CancellationToken cancellationToken = default)
    {
        var requestedTags = requestTags.Select(x => x.Trim()).Distinct().Select(x => new Tag { Name = x, NormalizedName = x.Trim().ToUpper() }).ToList();
        var normalizedTagNames = requestedTags.Select(x => x.NormalizedName);

        serviceToUpdate.Tags.RemoveAll(x => !normalizedTagNames.Contains(x.NormalizedName));

        var dbTags = await _appDbContext.Tags.Where(x => normalizedTagNames.Contains(x.NormalizedName)).ToListAsync(cancellationToken);

        foreach (var requestedTag in requestedTags)
        {
            if (!serviceToUpdate.Tags.Any(x => x.NormalizedName == requestedTag.NormalizedName))
            {
                if (!dbTags.TryGetFirstValue(x => requestedTag.NormalizedName == x.NormalizedName, out var tag))
                {
                    tag = new Tag { Name = requestedTag.Name, NormalizedName = requestedTag.NormalizedName };
                    _appDbContext.Tags.Add(tag);
                }

                serviceToUpdate.Tags.Add(tag);
            }
        }
    }
}
