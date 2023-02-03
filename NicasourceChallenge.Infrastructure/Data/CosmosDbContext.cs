using Microsoft.EntityFrameworkCore;

namespace NicasourceChallenge.Infrastructure.Data;

public class CosmosDbContext : DbContext
{
    public CosmosDbContext(DbContextOptions<CosmosDbContext> option) : base(option)
    {
    }
}