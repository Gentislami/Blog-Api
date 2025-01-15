using System.Text.Json.Serialization;

namespace Blog_Api.src.Entities
{
    public abstract class AbstractEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
