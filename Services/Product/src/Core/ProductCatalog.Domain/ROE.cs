namespace ProductCatalog.Domain
{
    public class ROE
    {
        public Guid ROEId { get; set; }
        public Guid CurrecnyId  { get; set; }
        public Currency Currency {  get; set; } 
        public decimal Rate { get; set; }
        public DateTime DateFrom { get; set; }
    }
}
