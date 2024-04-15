using Syndicate.Data.Enums;

namespace Syndicate.Services.Features.Identity.Models.Requests;

public class SignupRequest
{
    public string? Name { get; set; }

    public required string Email { get; set; }

    public required string Username { get; set; }

    public required string Password { get; set; }

    public UserType Type { get; set; } = UserType.Admin;
}
