using Microsoft.EntityFrameworkCore;
using Syndicate.Data.Enums;
using Syndicate.Data.Models.Identity;
using Syndicate.Data.Models.TagFeature;

namespace Syndicate.Data.Models.ServiceFeature;

public class Service : IDBConfigurableModel
{
    public Guid Id { get; set; }

    public required string Name { get; set; }

    public required string NormalizedName { get; set; }

    public List<Tag> Tags { get; set; } = [];

    public string? Description { get; set; }

    public required ServiceStatus Status { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public Guid OwnerId { get; set; }

    public User? Owner { get; set; }

    public int? Duration { get; set; }

    public decimal? Price { get; set; }

    public string? FolderName { get; set; }

    public ServiceFolder? Folder { get; set; }

    public static void BuildModel(ModelBuilder builder)
    {
        builder.Entity<Service>().Property(s => s.Price).HasPrecision(6, 2);
        builder.Entity<Service>().HasOne(x => x.Folder).WithMany(x => x.Services).HasForeignKey(x => x.FolderName);
    }
}

