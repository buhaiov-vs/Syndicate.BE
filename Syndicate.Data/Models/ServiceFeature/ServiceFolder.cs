using Microsoft.EntityFrameworkCore;
using Syndicate.Data.Models.Identity;

namespace Syndicate.Data.Models.ServiceFeature;

public class ServiceFolder : IDBConfigurableModel
{
    public Guid Id { get; set; }
    
    public required string Name { get; set; }

    public required string NormalizedName { get; set; }

    public required Guid OwnerId { get; set; }

    public User? Owner { get; set; }

    public List<Service> Services { get; set; } = [];

    public DateTime CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public static void BuildModel(ModelBuilder builder)
    {
        builder.Entity<ServiceFolder>().HasIndex(x => new { x.NormalizedName, x.OwnerId }).IsUnique();
        builder.Entity<ServiceFolder>().HasMany(s => s.Services).WithOne(x => x.Folder);
    }
}
