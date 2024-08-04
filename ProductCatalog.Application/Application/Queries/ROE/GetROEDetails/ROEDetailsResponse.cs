using ProductCatalog.Application.Common.Response;

namespace ProductCatalog.Application.Application.Queries.ROE.GetROEDetails
{
    public class ROEDetailsResponse : Response<ROEDetailsDto>
    {
        public ROEDetailsResponse(ROEDetailsDto _data) : base(_data) { }
    }
}
