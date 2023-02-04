using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;
using NicasourceChallenge.Core.Entities;

namespace NicasourceChallenge.Core.Specifications.Documents
{
    public class GetByUserIdSpec : Specification<Document>, ISingleResultSpecification
    {
        public GetByUserIdSpec(string userId)
        {
            Query.Where(x => x.UserId == new Guid(userId));
        }
    }
}
