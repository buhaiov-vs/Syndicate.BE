namespace Syndicate.Services.Features.Services.Models.Requests;

public class UpdateServiceRequest
{
    public required Guid Id { get; set; }

    public required string Name { get; set; }

    public string? Description { get; set; }

    public List<string> Tags { get; set; } = [];

    public required int Duration { get; set; }

    public required decimal Price { get; set; }
}