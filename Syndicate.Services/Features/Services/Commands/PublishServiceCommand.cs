using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Syndicate.Data;
using Syndicate.Data.Enums;
using Syndicate.Data.Models.ServiceFeature;
using Syndicate.Services.Extensions;
using System.Net;
using System.Text;

namespace Syndicate.Services.Features.Services.Commands;

public class PublishServiceCommand(
    AppDbContext appDbContext,
    IHttpContextAccessor httpContextAccessor,
    ILogger<PublishServiceCommand> logger)
{
    private readonly HttpContext _httpContext = httpContextAccessor.HttpContext!;

    public async Task<ApiResponse> ExecuteAsync(Guid serviceId, CancellationToken cancelationToken = default)
    {
        var userId = _httpContext.User.GetId();

        var service = await appDbContext.Services
            .Include(x => x.Tags)
            .AsTracking()
            .FirstOrDefaultAsync(x => x.OwnerId == userId && x.Id == serviceId, cancelationToken);

        if (service == null)
        {
            logger.LogWarning("Service with Id={Id} is not found", serviceId);
            return new() { Errors = [GetError($"Service with Id={serviceId} is not found", HttpStatusCode.NotFound)] };
        }

        if(!TryValidateBeforePublish(service, out var response))
        {
            return response;
        }

        service.Status = ServiceStatus.Active;
        await appDbContext.SaveChangesAsync(cancelationToken);

        return new();
    }

    private static bool TryValidateBeforePublish(Service service, out ApiResponse response)
    {
        response = new() { Errors = [] };

        StringBuilder message = new();
        if (service.Price is null)
        {
            message.Append("\nPrice is not set.");
        }

        if (service.Duration is null)
        {
            message.Append("\nDuration is not set.");
        }

        if (service.Tags.IsNullOrEmpty())
        {
            message.Append("\nTags are not set.");
        }

        if (message.Length == 0)
        {
            return true;
        }

        message.Insert(0, $"Unable to publish service \"{service.Name}\".");
        response.Errors.Add(GetError(message.ToString(), HttpStatusCode.BadRequest));

        return false;
    }

    private static ApiError GetError(string message, HttpStatusCode code)
    {
        return new() { Message = message, Code = ((int)code).ToString() };
    }
}
