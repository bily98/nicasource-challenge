using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NicasourceChallenge.Infrastructure.Data;
using NicasourceChallenge.SharedKernel.Entities;
using NicasourceChallenge.SharedKernel.Interfaces;

namespace NicasourceChallenge.Infrastructure.Repositories;

public class AsyncRepository<T> : RepositoryBase<T>, IAsyncRepository<T> where T : BaseEntity
{
    private readonly CosmosDbContext _dbContext;

    public AsyncRepository(CosmosDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> AnyAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<T>().AnyAsync(e => e.Id == id, cancellationToken);
    }

    public void Attach(object entity)
    {
        _dbContext.Attach(entity);
    }

    public void Clear()
    {
        _dbContext.ChangeTracker.Clear();
    }

    public override Task<T> AddAsync(T entity, CancellationToken cancellationToken = new())
    {
        var lastById = _dbContext.Set<T>().OrderByDescending(x => x.Id).FirstOrDefault();
        
        entity.Id = lastById != null? lastById.Id + 1 : 1;
        entity.PartitionKey = $"{(lastById != null ? lastById.Id + 1 : 1)}";

        return base.AddAsync(entity, cancellationToken);
    }
}