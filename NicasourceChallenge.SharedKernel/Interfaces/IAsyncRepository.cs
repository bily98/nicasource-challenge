using Ardalis.Specification;

namespace NicasourceChallenge.SharedKernel.Interfaces;

public interface IAsyncRepository<T> : IRepositoryBase<T> where T : class
{
    public Task<bool> AnyAsync(int id, CancellationToken cancellationToken = default);
    public void Attach(object entity);
    public void Clear();
}