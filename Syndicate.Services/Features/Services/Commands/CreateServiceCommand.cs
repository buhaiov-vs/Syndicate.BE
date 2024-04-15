using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Syndicate.Data;
using Syndicate.Data.Models;
using Syndicate.Services.Extensions;
using Syndicate.Services.Features.Services.Models.Requests;
using Syndicate.Services.Features.Services.Models.Responses;
using System;

namespace Syndicate.Services.Features.Services.Commands;

public class CreateServiceCommand(AppDbContext appDbContext, IHttpContextAccessor httpContextAccessor, IValidator<CreateServiceRequest> validator, ILogger<CreateServiceCommand> logger)
{
    private readonly HttpContext _httpContext = httpContextAccessor.HttpContext!;
    private const int TagsMaxCount = 10;

    public async Task<CreateServiceResponse> ExecuteAsync(CreateServiceRequest request, CancellationToken cancelationToken = default)
    {
        validator.ValidateAndThrow(request);

        var userId = _httpContext.User.GetId();

        var entity = new Service()
        {
            Name = request.Name,
            Description = request.Description,
            OwnerId = userId,
            Status = request.Status,
            CreatedOn = DateTime.UtcNow,
        };

        var nameToTags = entity.Name.Trim().Replace(",", "").Split(" ");
        foreach (var tagName in nameToTags.Union(request.Tags.Distinct()).Take(nameToTags.Length + TagsMaxCount))
        {
            var tag = await appDbContext.Tags.FirstOrDefaultAsync(t => t.Name == tagName)
                ?? new Tag { Name = tagName };

            entity.Tags.Add(tag);
        }

        appDbContext.Services.Add(entity);
        await appDbContext.SaveChangesAsync(cancelationToken);

        return new() { Id = entity.Id };
    }
}
