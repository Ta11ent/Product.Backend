namespace ProductCatalog.Domain
{
    public class Currency
    {
        public Guid CurrencyId { get; set; }
        public string Name { get; set; } = string.Empty;    
        public string Code { get; set; } = string.Empty;
        public IEnumerable<ROE> ROEs { get; set; }
    }
}
