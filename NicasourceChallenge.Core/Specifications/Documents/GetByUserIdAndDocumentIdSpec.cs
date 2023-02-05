using Ardalis.Specification;
using NicasourceChallenge.Core.Entities;

namespace NicasourceChallenge.Core.Specifications.Documents;

public class GetByUserIdAndDocumentIdSpec : Specification<Entities.File>, ISingleResultSpecification
{
    public GetByUserIdAndDocumentIdSpec(string userId, int fileId)
    {
        Query.Where(x => x.UserId == new Guid(userId));
        Query.Where(x => x.Id == fileId);
    }
}