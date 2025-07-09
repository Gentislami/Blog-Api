using Blog_Api.src.Dtos;

namespace Blog_Api.src.Entities
{
    public class Blog : AbstractEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser? User { get; set; }
        public virtual ICollection<BlogPost> BlogPosts { get; set; } = new List<BlogPost>();

        public override bool IsUserAuthorized(string currentUserId)
        {
            return currentUserId == User.Id;
        }

        public override void ApplyDto(IDto dto)
        {
            Name = ((BlogDto)dto).Name;
            Description = ((BlogDto)dto).Description;
        }
    }

}
