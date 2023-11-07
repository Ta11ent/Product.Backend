namespace ProductCatalog.Application.Application.Models.Category
{
    public class UpdateCategoryCommand
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
