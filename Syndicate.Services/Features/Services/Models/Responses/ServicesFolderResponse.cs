using Syndicate.Data.Models.ServiceFeature;

namespace Syndicate.Services.Features.Services.Models.Responses;

public class ServicesFolderResponse
{
    public required Guid Id { get; set; }

    public required string Name { get; set; }

    public List<Service> Services { get; set; } = [];


    public static implicit operator ServicesFolderResponse(ServiceFolder b)
    {
        return new()
        {
            Id = b.Id,
            Name = b.Name,
            Services = b.Services,
        };
    }
}