using Syndicate.Data.Enums;

namespace Syndicate.Services.Features.Services.Models.Requests;

public class CreateServiceRequest
{
    public required string Name { get; set; }

    public required string Description { get; set; }

    public required ServiceStatus Status { get; set; }

    public List<string> Tags { get; set; } = [];
}