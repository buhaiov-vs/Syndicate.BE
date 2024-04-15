using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Syndicate.Data;
using Syndicate.Services.Extensions;
using Syndicate.Services.Features.Services.Models.Responses;
using System.Net;

namespace Syndicate.Services.Features.Services.Queries;

public class GetServiceQuery(IDbContextFactory<AppDbContext> dbContextFactory, IHttpContextAccessor _httpContextAccessor, ILogger<GetServiceQuery> logger)
{
    private readonly HttpContext _httpContext = _httpContextAccessor!.HttpContext;

    public async Task<ApiResponse<ServiceResponse>> ExecuteAsync(Guid id, CancellationToken cancelationToken = default)
    {
        using var db = await dbContextFactory.CreateDbContextAsync(cancelationToken);

        var result = await db.Services
            .Where(x => x.Id == id)
            .Where(x => x.OwnerId == _httpContext.User.GetId())
            .FirstOrDefaultAsync(cancelationToken);

        if(result == null)
        {
            return ApiResponse<ServiceResponse>.Fail(HttpStatusCode.NotFound, "No such service was found");
        }

        return ApiResponse<ServiceResponse>.Happy((ServiceResponse)result);
    }
}
