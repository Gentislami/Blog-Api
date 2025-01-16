using Blog_Api.src.Dtos;

namespace Blog_Api.src.Entities
{
    public class BlogPost : AbstractEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublishedAt { get; set; }
        public bool IsPublished { get; set; }
        public string BlogId { get; set; }
        public virtual Blog? Blog { get; set; }
        public virtual ApplicationUser? User { get; set; }
        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

        public override bool IsUserAuthorized(string currentUserId)
        {
            return currentUserId == User.Id;
        }

        public override void ApplyDto(IDto dto)
        {
            Title = ((BlogPostDto)dto).Title;
            Content = ((BlogPostDto)dto).Content;
            IsPublished = ((BlogPostDto)dto).IsPublished;
        }
    }
}
