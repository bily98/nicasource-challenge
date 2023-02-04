using Microsoft.EntityFrameworkCore;
using NicasourceChallenge.Core.Entities;

namespace NicasourceChallenge.Infrastructure.Data;

public class CosmosDbContext : DbContext
{
    public CosmosDbContext(DbContextOptions<CosmosDbContext> option) : base(option)
    {
    }

    public DbSet<Document> Documents { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Document>(e =>
        {
            e.ToContainer("Document");
            e.Property(p => p.Id);
            e.HasPartitionKey(p => p.PartitionKey);
        });
    }
}