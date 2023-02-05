using Ardalis.Specification;
using NicasourceChallenge.Core.Entities;

namespace NicasourceChallenge.Core.Specifications.Files;

public class GetByUserIdAndFileIdSpec : Specification<File>, ISingleResultSpecification
{
    public GetByUserIdAndFileIdSpec(string userId, int fileId)
    {
        Query.Where(x => x.UserId == new Guid(userId));
        Query.Where(x => x.Id == fileId);
    }
}