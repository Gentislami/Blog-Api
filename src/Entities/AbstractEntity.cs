using Blog_Api.src.Dtos;

namespace Blog_Api.src.Entities
{
    public interface IEntity
    {
        string Id { get; set; }
        DateTime CreatedAt { get; set; }
        DateTime? UpdatedAt { get; set; }
        DateTime? DeletedAt { get; set; }
        bool IsUserAuthorized(string currentUserId);
        void ApplyDto(IDto dto);
    }

    public abstract class AbstractEntity : IEntity
    {
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual bool IsUserAuthorized(string currentUserId)
        {
            return true; // default behaviour
        }

        public virtual void ApplyDto(IDto dto)
        {
            // Do nothing. This ensures this method's implementation is not mandatory for derived classes that don't need it.
        }
    }
}
