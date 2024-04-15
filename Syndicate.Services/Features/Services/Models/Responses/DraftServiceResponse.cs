using Syndicate.Data.Models;
using System;

namespace Syndicate.Services.Features.Services.Models.Responses;
public class DraftServiceResponse
{
    public required Guid Id { get; set; }

    public required string Name { get; set; }

    public static implicit operator DraftServiceResponse(Service b)
    {
        return new()
        {
            Id = b.Id,
            Name = b.Name
        };
    }
}
