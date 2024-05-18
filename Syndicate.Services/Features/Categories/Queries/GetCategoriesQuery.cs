using Microsoft.EntityFrameworkCore;
using Syndicate.Data;
using Syndicate.Services.Features.Categories.Models;

namespace Syndicate.Services.Features.Categories.Queries;

public class GetCategoriesQuery(IDbContextFactory<AppDbContext> _dbContextFactory)
{
    public async Task<ApiResponse<IEnumerable<CategoryResponse>>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        var categories = await dbContext.Categories.ToListAsync(cancellationToken);
        var result = categories.Select(x => (CategoryResponse)x);

        return new ApiResponse<IEnumerable<CategoryResponse>> { Data = result };
    }
}
