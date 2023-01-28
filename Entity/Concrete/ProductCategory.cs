using Core.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Entity.Concrete
{
    public class ProductCategory : BaseEntity
    { 
        [JsonIgnore]
        [ForeignKey("ProductId")]
        public Product? Product { get; set; }
        public int ProductId { get; set; }


        [JsonIgnore]
        [ForeignKey("CategoryId")]  
        public Category? Category { get; set; }
        public int CategoryId { get; set; }
    }
}
