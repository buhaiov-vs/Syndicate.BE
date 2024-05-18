using Syndicate.Data.Models;

namespace Syndicate.Services.Features.Categories.Models;

public class CategoryResponse
{
    public required Guid Id { get; set; }

    public required string Name { get; set; }

    public required string Description { get; set; }

    public List<Category>? Children { get; set; }


    public static implicit operator CategoryResponse(Category b)
    {
        return new()
        {
            Id = b.Id,
            Name = b.Name,
            Description = b.Description,
            Children = b.Children,
        };
    }
}
