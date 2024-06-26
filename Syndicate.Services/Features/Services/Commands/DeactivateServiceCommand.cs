using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Syndicate.Data;
using Syndicate.Data.Enums;
using Syndicate.Services.Extensions;
using System.Net;

namespace Syndicate.Services.Features.Services.Commands;

public class DeactivateServiceCommand(
    AppDbContext appDbContext,
    IHttpContextAccessor httpContextAccessor,
    ILogger<DeactivateServiceCommand> logger)
{
    private readonly HttpContext _httpContext = httpContextAccessor.HttpContext!;

    public async Task<ApiResponse> ExecuteAsync(Guid serviceId, CancellationToken cancellationToken = default)
    {
        var userId = _httpContext.User.GetId();

        var service = await appDbContext.Services
            .AsTracking()
            .FirstOrDefaultAsync(x => x.OwnerId == userId && x.Id == serviceId, cancellationToken);

        if (service == null)
        {
            logger.LogWarning("Service with Id={Id} is not found", serviceId);
            return new() { Errors = [new() { Message = $"Service with Id={serviceId} is not found", Code = ((int)HttpStatusCode.NotFound).ToString() }] };
        }

        service.Status = ServiceStatus.Inactive;
        await appDbContext.SaveChangesAsync(cancellationToken);

        return new();
    }
}
