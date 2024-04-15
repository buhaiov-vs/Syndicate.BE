using System;

namespace Syndicate.Data.Models;
public class Tag
{
    public Guid Id { get; set; } 

    public required string Name { get; set; }

    public List<Service> Services { get; set; } = [];
}
