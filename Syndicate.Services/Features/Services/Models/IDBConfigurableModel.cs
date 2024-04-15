using Microsoft.EntityFrameworkCore;

namespace Syndicate.Services.Features.Services.Models;

internal interface IDBConfigurableModel
{
    internal void ConfigureModel(ModelBuilder builder);
}
