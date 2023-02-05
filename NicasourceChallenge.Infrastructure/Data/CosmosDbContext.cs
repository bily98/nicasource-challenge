using Microsoft.EntityFrameworkCore;
using NicasourceChallenge.Core.Entities;

namespace NicasourceChallenge.Infrastructure.Data;

public class CosmosDbContext : DbContext
{
    public CosmosDbContext(DbContextOptions<CosmosDbContext> option) : base(option)
    {
    }

    public DbSet<File> Files { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<File>(e =>
        {
            e.ToContainer("File");
            e.Property(p => p.Id);
            e.HasPartitionKey<File, string>(p => p.PartitionKey);
        });
    }
}