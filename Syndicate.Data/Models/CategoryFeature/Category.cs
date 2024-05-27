using Microsoft.EntityFrameworkCore;

namespace Syndicate.Data.Models.CategoryFeature;

public class Category : IDBConfigurableModel
{
    public Guid Id { get; set; }

    public required string Name { get; set; }

    public required string Description { get; set; }

    public List<Category>? Children { get; set; }

    public static void BuildModel(ModelBuilder builder)
    {
        builder
            .Entity<RefreshToken>()
            .ToTable("Category");

        builder.Entity<Category>()
            .HasData([new()
                {
                    Id = Guid.Parse("e0023d58-4374-4ef1-b40e-e90d3c0961b0"),
                    Name = "First",
                    Description = "First category",
                },
                new()
                {
                    Id = Guid.Parse("8dd0d8f2-5e0c-453e-9ef5-61e72b940ca6"),
                    Name = "Second",
                    Description = "Second desciption",
                },
                new()
                {
                    Id = Guid.Parse("30c1287d-296d-44fd-955d-96d4a78ac69f"),
                    Name = "3",
                    Description = "Thried desc",
                },
                new()
                {
                    Id = Guid.Parse("fb82f219-f411-4886-ba5d-86dac5723603"),
                    Name = "4",
                    Description = "four",
                }]);
    }
}
