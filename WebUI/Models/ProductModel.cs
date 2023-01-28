namespace WebUI.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public uint StockAmount { get; set; }
        public string About { get; set; }
        public bool IsActive { get; set; }
    }
}
