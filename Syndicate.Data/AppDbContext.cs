using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Syndicate.Data.Models;
using System.Reflection;

namespace Syndicate.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<User, Role, Guid>(options)
{
    public DbSet<Category> Categories { get; set; }

    public DbSet<Service> Services { get; set; }

    public DbSet<Tag> Tags { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        MapIdentity(builder);
        CallModelBuilders(builder);
    }

    private static void MapIdentity(ModelBuilder builder)
    {
        builder.Entity<IdentityUserRole<Guid>>().ToTable("UserRole");
        builder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaim");
        builder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogin");
        builder.Entity<IdentityUserToken<Guid>>().ToTable("UserToken");
        builder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaim");
    }

    private void CallModelBuilders(ModelBuilder builder)
    {
        var types = Assembly.GetExecutingAssembly().GetTypes();
        var implementations = types.Where(t => typeof(IDBConfigurableModel).IsAssignableFrom(t) && t.IsClass);

        foreach (var impl in implementations)
        {
            var methodInfo = impl.GetMethod(nameof(IDBConfigurableModel.BuildModel), BindingFlags.Static | BindingFlags.Public);

            methodInfo?.Invoke(null, [builder]);
        }
    }
}
