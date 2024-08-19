using System.Text;

namespace ShoppingCart.Application.Common.Helpers
{
    public interface IParamQueryBuilder
    {
        IParamQueryBuilder AddIdAsParams(string paramName, List<Guid> values);
        IParamQueryBuilder AddPaginationAsParams(int itemsPerPage, int startItem = 1);
        IParamQueryBuilder AddCustomParam(string paramName, object value);
        string GetParamAsString();
    }
}
