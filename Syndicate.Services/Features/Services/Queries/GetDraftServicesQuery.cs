using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Syndicate.Data;
using Syndicate.Data.Models;
using Syndicate.Services.Extensions;
using Syndicate.Services.Features.Services.Models.Responses;

namespace Syndicate.Services.Features.Services.Queries;

public class GetDraftServicesQuery(IDbContextFactory<AppDbContext> dbContextFactory, IHttpContextAccessor _httpContextAccessor, ILogger<GetDraftServicesQuery> logger)
{
    private readonly HttpContext _httpContext = _httpContextAccessor!.HttpContext;

    public async Task<ApiResponse<IEnumerable<DraftServiceResponse>>> ExecuteAsync(CancellationToken cancelationToken = default)
    {
        using var db = await dbContextFactory.CreateDbContextAsync(cancelationToken);

        var result = await db.Services
            .Where(x => x.OwnerId == _httpContext.User.GetId())
            .Include(x => x.Tags)
            .OrderByDescending(x => x.Status)
            .ThenBy(x => x.Name)
            .Select(x => new Service { Id = x.Id, Name = x.Name, Status = x.Status })
            .ToListAsync(cancelationToken);

        return ApiResponse<IEnumerable<DraftServiceResponse>>.Happy(result.Select(x => (DraftServiceResponse)x));
    }
}
