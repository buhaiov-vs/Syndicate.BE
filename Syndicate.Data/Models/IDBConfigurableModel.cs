using Microsoft.EntityFrameworkCore;

namespace Syndicate.Data.Models;

public interface IDBConfigurableModel
{
    public abstract static void BuildModel(ModelBuilder builder);
}
