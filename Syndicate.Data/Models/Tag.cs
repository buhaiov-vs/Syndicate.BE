using Microsoft.EntityFrameworkCore;

namespace Syndicate.Data.Models;

public class Tag : IDBConfigurableModel
{
    public required string Name { get; set; }

    public List<Service> Services { get; set; } = [];

    public static void BuildModel(ModelBuilder builder)
    {
        builder.Entity<Tag>().HasMany(x => x.Services).WithMany(s => s.Tags);
        builder.Entity<Tag>().HasKey(x => x.Name);
    }
}
