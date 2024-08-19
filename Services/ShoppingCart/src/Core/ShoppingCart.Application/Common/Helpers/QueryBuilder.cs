using Microsoft.Extensions.Primitives;
using ShoppingCart.Application.Common.Pagination;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace ShoppingCart.Application.Common.Helpers
{
    internal static class QueryBuilder
    {
        internal static string ConvertToIdString(List<Guid> guids, string paramName) =>
            guids.Aggregate("", (first, next) => $"{first}{paramName}={next}&");
                
        internal static string GeneratePaginationParam(int itemsPerPage) =>
            $"&{nameof(IPaginationParam.Page)}=1&{nameof(IPaginationParam.PageSize)}={itemsPerPage}";

    }
}
