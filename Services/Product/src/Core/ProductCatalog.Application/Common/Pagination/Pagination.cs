namespace ProductCatalog.Application.Common.Pagination
{
    public class Pagination : IPagination
    {
        private readonly int startItemsPerPage = 20;
        private readonly int startPage = 1;
        private int itemsPerPage;
        private int numberOfPage;

        public int Page {
            get => numberOfPage;
            set => numberOfPage = value == 0 ? startPage : value;
        }

        public int PageSize
        {
            get => itemsPerPage;
            set => itemsPerPage = value == 0 ? startItemsPerPage : value;
        }
    }
}
