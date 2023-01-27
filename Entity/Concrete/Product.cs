using Core.Entity;

namespace Entity.Concrete
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public uint StockAmount { get; set; }
        public string About { get; set; }
        public int CategoryId { get; set; }
    }
}
