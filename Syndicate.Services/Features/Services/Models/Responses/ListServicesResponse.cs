using Syndicate.Data.Enums;
using Syndicate.Data.Models.ServiceFeature;

namespace Syndicate.Services.Features.Services.Models.Responses;
public class ListServicesResponse
{
    public required Guid Id { get; set; }

    public required string Name { get; set; }

    public required ServiceStatus Status { get; set; }

    public static implicit operator ListServicesResponse(Service b)
    {
        return new()
        {
            Id = b.Id,
            Name = b.Name,
            Status = b.Status,
        };
    }
}
