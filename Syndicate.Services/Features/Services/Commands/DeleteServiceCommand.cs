using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Syndicate.Data;
using Syndicate.Services.Features.Services.Models.Requests;
using Syndicate.Services.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Syndicate.Services.Features.Services.Commands;
public class DeleteServiceCommand(AppDbContext appDbContext, IHttpContextAccessor httpContextAccessor, ILogger<CreateServiceCommand> logger)
{
    private readonly HttpContext _httpContext = httpContextAccessor.HttpContext!;

    public async Task<ApiResponse> ExecuteAsync(DeleteServiceRequest request, CancellationToken cancelationToken = default)
    {
        var userId = _httpContext.User.GetId();

        var service = await appDbContext.Services
            .Include(x => x.Tags)
            .Where(x => x.OwnerId == userId && x.Id == request.Id)
            .FirstOrDefaultAsync(cancelationToken);

        if (service != null)
        {
            appDbContext.Services.Remove(service);
            appDbContext.RemoveRange(service.Tags);

            await appDbContext.SaveChangesAsync(cancelationToken);
        }

        return ApiResponse.Happy();
    }
}
