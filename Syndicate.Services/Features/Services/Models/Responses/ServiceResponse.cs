using Syndicate.Data.Enums;
using Syndicate.Data.Models.ServiceFeature;

namespace Syndicate.Services.Features.Services.Models.Responses;

public class ServiceResponse
{
    public required Guid Id { get; set; }

    public required string Name { get; set; }

    public string? Description { get; set; }

    public required ServiceStatus Status { get; set; }

    public List<string> Tags { get; set; } = [];

    public decimal? Price { get; set; }

    public int? Duration { get; set; }

    public List<Guid>? Executors { get; set; }

    public static implicit operator ServiceResponse(Service b)
    {
        return new()
        {
            Id = b.Id,
            Name = b.Name,
            Status = b.Status,
            Description = b.Description,
            Tags = b.Tags.Select(t => t.Name).ToList(),
            Price = b.Price,
            Duration = b.Duration
        };
    }
}