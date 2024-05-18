namespace Syndicate.Services.Features.Identity.Models.Responses;

public class LoginResponse
{
    public Guid? UserId { get; set; }

    public bool? IsLocked { get; set; }

    public bool? Requires2FA { get; set; }
}

