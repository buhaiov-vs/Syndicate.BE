using Microsoft.EntityFrameworkCore;
using Syndicate.Data.Models.ServiceFeature;

namespace Syndicate.Data.Models.TagFeature;

public class Tag : IDBConfigurableModel
{
    public required string Name { get; set; }
    
    public required string NormalizedName { get; set; }

    public List<Service> Services { get; set; } = [];

    public static void BuildModel(ModelBuilder builder)
    {
        builder.Entity<Tag>().HasMany(x => x.Services).WithMany(s => s.Tags);
        builder.Entity<Tag>().HasKey(x => x.Name);
    }
}
