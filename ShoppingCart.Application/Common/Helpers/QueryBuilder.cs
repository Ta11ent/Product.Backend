namespace ShoppingCart.Application.Common.Helpers
{
    internal static class QueryBuilder
    {
        internal static string ConvertToIdString (List<Guid> guids, string paramName) =>
            guids.Aggregate("", (first, next) => $"{first}{paramName}={next}&");
    }
}
