
using System.Text.Json.Serialization;

namespace Core.Entity
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
    }
}
