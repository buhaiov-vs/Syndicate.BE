using FluentValidation;
using Microsoft.AspNetCore.Http;
using Syndicate.Data;
using Syndicate.Data.Models.ServiceFeature;
using Syndicate.Services.Extensions;
using Syndicate.Services.Features.Services.Models.Requests;
using System.Net;

namespace Syndicate.Services.Features.Services.Commands;

public class CreateServicesFolderCommand(
    AppDbContext appDbContext,
    IHttpContextAccessor httpContextAccessor,
    IValidator<CreateServicesFolderRequest> validator /// <see cref="Validators.CreateFolderServiceRequestValidator"/>
    )
{
    private readonly HttpContext _httpContext = httpContextAccessor.HttpContext!;

    public async Task<ApiResponse> ExecuteAsync(CreateServicesFolderRequest request, CancellationToken cancellationToken = default)
    {
        validator.ValidateAndThrow(request);
        var userId = _httpContext.User.GetId();

        if (appDbContext.ServicesFolders.Any(x => x.OwnerId == userId && x.NormalizedName == request.Name.ToUpper()))
        {
            return ApiResponse.Fail(HttpStatusCode.BadRequest, "Folder with the following name already exists");
        }

        var entity = new ServiceFolder()
        {
            Name = request.Name,
            NormalizedName = request.Name.ToUpper(),
            OwnerId = userId,
            CreatedOn = DateTime.UtcNow
        };

        appDbContext.ServicesFolders.Add(entity);
        await appDbContext.SaveChangesAsync(cancellationToken);

        return ApiResponse.Happy();
    }
}
