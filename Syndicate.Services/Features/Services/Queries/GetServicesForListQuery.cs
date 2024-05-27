using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Syndicate.Data;
using Syndicate.Services.Exceptions;
using Syndicate.Services.Extensions;
using Syndicate.Services.Features.Services.Models.Responses;

namespace Syndicate.Services.Features.Services.Queries;

public class GetServicesForListQuery(IDbContextFactory<AppDbContext> dbContextFactory, IHttpContextAccessor _httpContextAccessor, ILogger<GetServicesForListQuery> logger)
{
    private readonly HttpContext _httpContext = _httpContextAccessor?.HttpContext ?? throw new MissedHttpContextException();

    public async Task<ApiResponse<IEnumerable<ListServicesResponse>>> ExecuteAsync(CancellationToken cancelationToken = default)
    {
        var userId = _httpContext.User.GetId();
        using var db = await dbContextFactory.CreateDbContextAsync(cancelationToken);

        var services = await db.Services
            .Where(x => x.OwnerId == userId)
            .Select(x => new ListServicesResponse { Id = x.Id, Name = x.Name, Status = x.Status })
            .ToListAsync(cancelationToken);

        return new(services);
    }
}
