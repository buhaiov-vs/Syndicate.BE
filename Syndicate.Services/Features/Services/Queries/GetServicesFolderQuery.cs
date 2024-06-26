using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Syndicate.Data;
using Syndicate.Services.Exceptions;
using Syndicate.Services.Extensions;
using Syndicate.Services.Features.Services.Models.Responses;
using System.Net;

namespace Syndicate.Services.Features.Services.Queries;

public class GetServicesFolderQuery(IDbContextFactory<AppDbContext> dbContextFactory, IHttpContextAccessor _httpContextAccessor)
{
    private readonly HttpContext _httpContext = _httpContextAccessor?.HttpContext ?? throw new MissedHttpContextException();

    public async Task<ApiResponse<ServicesFolderResponse>> ExecuteAsync(string name, CancellationToken cancelationToken = default)
    {
        using var db = await dbContextFactory.CreateDbContextAsync(cancelationToken);

        var folder = await db.ServicesFolders
            .Where(x => x.NormalizedName == name.ToUpper() && x.OwnerId == _httpContext.User.GetId())
            .Include(x => x.Services)
            .FirstOrDefaultAsync(cancelationToken);

        return folder == null
            ? new(HttpStatusCode.NotFound, "No such folder was found")
            : new((ServicesFolderResponse)folder);
    }
}
