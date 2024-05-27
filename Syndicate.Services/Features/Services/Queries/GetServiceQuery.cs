using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Syndicate.Data;
using Syndicate.Services.Exceptions;
using Syndicate.Services.Extensions;
using Syndicate.Services.Features.Services.Models.Responses;
using System.Net;

namespace Syndicate.Services.Features.Services.Queries;

public class GetServiceQuery(IDbContextFactory<AppDbContext> dbContextFactory, IHttpContextAccessor _httpContextAccessor, ILogger<GetServiceQuery> logger)
{
    private readonly HttpContext _httpContext = _httpContextAccessor?.HttpContext ?? throw new MissedHttpContextException();

    public async Task<ApiResponse<ServiceResponse>> ExecuteAsync(Guid id, CancellationToken cancelationToken = default)
    {
        using var db = await dbContextFactory.CreateDbContextAsync(cancelationToken);

        var service = await db.Services
            .Where(x => x.Id == id && x.OwnerId == _httpContext.User.GetId())
            .Include(x => x.Tags)
            .FirstOrDefaultAsync(cancelationToken);

        return service == null
            ? new(HttpStatusCode.NotFound, "No such service was found")
            : new((ServiceResponse)service);
    }
}
