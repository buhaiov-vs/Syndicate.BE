using Syndicate.Data.Enums;

namespace Syndicate.Services.Features.Services.Models.Responses;

public class UpdateServiceResponse
{
    public Guid Id { get; set; }

    public ServiceStatus Status { get; set; }
}
