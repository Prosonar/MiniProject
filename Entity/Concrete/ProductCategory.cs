using Core.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Concrete
{
    public class ProductCategory : BaseEntity
    {
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
        public int CategoryId { get; set; }
    }
}
