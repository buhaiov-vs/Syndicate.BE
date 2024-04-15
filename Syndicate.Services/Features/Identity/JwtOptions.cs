namespace Syndicate.Services.Features.Identity;

public class JwtOptions
{
    public required string Issuer { get; set; }

    public required string Audience { get; set; }

    public required string Key { get; set; }

    public int LifetimeDays { get; set; }

    public int RefreshLifetimeDays { get; internal set; }
}
