using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using NicasourceChallenge.Core.Entities;
using NicasourceChallenge.SharedKernel.Extensions;

namespace NicasourceChallenge.Core.Specifications.Documents
{
    public class GetByUserIdSpec : Specification<Document>, ISingleResultSpecification
    {
        public GetByUserIdSpec(string userId)
        {
            Query.Where(x => x.UserId == new Guid(userId));
        }

        public GetByUserIdSpec(string userId, string sortColumn, string sortColumnDirection, string searchValue, int skip, int pageSize)
        {
            Query.Where(x => x.UserId == new Guid(userId));

            if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortColumnDirection))
            {
                Query.OrderBy(sortColumn, sortColumnDirection == "desc");
            }

            if (!string.IsNullOrEmpty(searchValue))
            {
                Query.Where(x => x.Name!.Contains(searchValue)
                                 || x.Description!.Contains(searchValue)
                                 || x.Format!.Contains(searchValue));
            }

            Query.Skip(skip).Take(pageSize);
        }
    }
}
