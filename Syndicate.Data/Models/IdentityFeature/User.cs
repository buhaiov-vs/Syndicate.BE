using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Syndicate.Data.Enums;

namespace Syndicate.Data.Models.Identity;

public class User : IdentityUser<Guid>, IDBConfigurableModel
{
    public new required string Email { get; set; }

    public required string Name { get; set; }

    public required UserType Type { get; set; }

    public List<RefreshToken> RefreshTokens { get; set; } = [];

    public static void BuildModel(ModelBuilder builder)
    {
        builder.Entity<User>()
            .ToTable("User");

        builder.Entity<User>()
            .HasKey(x => x.Id);

        builder.Entity<User>()
            .HasDiscriminator(x => x.Type)
            .HasValue<User>(UserType.None)
            .HasValue<Admin>(UserType.Admin)
            .HasValue<Customer>(UserType.Customer);
    }
}