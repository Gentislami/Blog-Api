using Blog_Api.src.Dtos;

namespace Blog_Api.src.Entities
{
    public class Comment : AbstractEntity
    {
        public string Content { get; set; }
        public DateTime PostedAt { get; set; }
        public string BlogPostId { get; set; }
        public virtual BlogPost? BlogPost { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser? User { get; set; }

        public override bool IsUserAuthorized(string currentUserId)
        {
            return currentUserId == User.Id;
        }

        public override void ApplyDto(IDto dto)
        {
            Content = ((Comment)dto).Content;
        }
    }
}