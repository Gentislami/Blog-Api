using Blog_Api.src.Dtos;

namespace Blog_Api.src.Entities
{
    public class Like : AbstractEntity
    {
        public virtual ApplicationUser? User { get; set; }
        public string BlogPostId { get; set; }
        public virtual BlogPost? BlogPost { get; set; }
        public bool Liked { get; set; }

        public override bool IsUserAuthorized(string currentUserId)
        {
            return currentUserId == User.Id;
        }

        public override void ApplyDto(IDto dto)
        {
            Liked = ((LikeDto)dto).Liked;
        }
    }
}
