using Ardalis.Specification;

namespace NicasourceChallenge.SharedKernel.Interfaces;

public interface IAsyncRepository<T> : IRepositoryBase<T> where T : class
{
}