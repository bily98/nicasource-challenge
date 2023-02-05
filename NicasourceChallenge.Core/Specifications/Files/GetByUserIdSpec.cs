using Ardalis.Specification;
using NicasourceChallenge.Core.Entities;

namespace NicasourceChallenge.Core.Specifications.Files;

public class GetByUserIdSpec : Specification<File>, ISingleResultSpecification
{
    public GetByUserIdSpec(string userId)
    {
        Query.Where(x => x.UserId == new Guid(userId));
    }

    public GetByUserIdSpec(string userId, string sortColumn, string sortColumnDirection, string searchValue, int skip,
        int pageSize)
    {
        Query.Where(x => x.UserId == new Guid(userId));

        if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortColumnDirection))
        {
            switch (sortColumn)
            {
                case "Name" when sortColumnDirection == "asc":
                    Query.OrderBy(x => x.Name);
                    break;
                case "Name":
                    Query.OrderByDescending(x => x.Name);
                    break;
                case "Description" when sortColumnDirection == "asc":
                    Query.OrderBy(x => x.Description);
                    break;
                case "Description":
                    Query.OrderByDescending(x => x.Description);
                    break;
                case "Format" when sortColumnDirection == "asc":
                    Query.OrderBy(x => x.Format);
                    break;
                case "Format":
                    Query.OrderByDescending(x => x.Format);
                    break;
                case "Size" when sortColumnDirection == "asc":
                    Query.OrderBy(x => x.Size);
                    break;
                case "Size":
                    Query.OrderByDescending(x => x.Size);
                    break;
                case "Created" when sortColumnDirection == "asc":
                    Query.OrderBy(x => x.Created);
                    break;
                case "Created":
                    Query.OrderByDescending(x => x.Created);
                    break;
                case "Updated" when sortColumnDirection == "asc":
                    Query.OrderBy(x => x.Updated);
                    break;
                case "Updated":
                    Query.OrderByDescending(x => x.Updated);
                    break;
            }
        }

        if (!string.IsNullOrEmpty(searchValue))
            Query.Where(x => x.Name!.Contains(searchValue)
                             || x.Description!.Contains(searchValue)
                             || x.Format!.Contains(searchValue));

        Query.Skip(skip).Take(pageSize);
    }
}