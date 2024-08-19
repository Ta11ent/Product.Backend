using ShoppingCart.Application.Common.Pagination;
using System.Text;

namespace ShoppingCart.Application.Common.Helpers
{
    internal class ParamQueryBuilder : IParamQueryBuilder
    {
        private StringBuilder Params;
        internal ParamQueryBuilder() => Params = new StringBuilder();

        public IParamQueryBuilder AddIdAsParams(string paramName, List<Guid> values)
        {
            Params.Append(values.Aggregate("", (first, next) => $"{first}{paramName}={next}&"));
            return this;
        }
        public IParamQueryBuilder AddPaginationAsParams(int itemsPerPage, int startItem = 1)
        {
            Params.Append($"&{nameof(IPaginationParam.Page)}={startItem}&{nameof(IPaginationParam.PageSize)}={itemsPerPage}");
            return this;
        }

        public IParamQueryBuilder AddCustomParam(string paramName, object value)
        {
            Params.Append($"&{paramName}={value}");
            return this;
        }
        public string GetParamAsString() => Params.ToString();
    }
}
