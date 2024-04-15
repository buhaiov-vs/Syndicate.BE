using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Syndicate.Data.Models;

public class Role : IdentityRole<Guid>, IDBConfigurableModel
{
    public static void BuildModel(ModelBuilder builder)
    {
        builder.Entity<Role>()
            .ToTable("Role");
    }
}
