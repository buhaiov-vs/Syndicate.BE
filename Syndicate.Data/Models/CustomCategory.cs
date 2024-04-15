namespace Syndicate.Data.Models;

public class CustomCategory
{
    public Guid Id { get; set; }

    public required string Name { get; set; }

    public string? Description { get; set; }

    public Guid OwnerId { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime UpdatedOn { get; set; }

    public Guid UpdatedBy { get; set; }

    public required Guid MostRelatesToCategoryId { get; set; }

    public Category? MostRelatesToCategory { get; set; }
}
