using Blog_Api.src.Dtos;
using Microsoft.AspNetCore.Identity;

namespace Blog_Api.src.Entities
{

    public class ApplicationUser : IdentityUser, IEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public virtual ICollection<BlogPost> BlogPosts { get; set; } = new List<BlogPost>();
        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public virtual ICollection<Blog> Blogs { get; set; } = new List<Blog>();

        public bool IsUserAuthorized(string currentUserId)
        {
            return currentUserId == Id;
        }

        public void ApplyDto(IDto dto)
        {
            UserName = ((ApplicationUserDto)dto).UserName;
            Email = ((ApplicationUserDto)dto).Email;
            Name = ((ApplicationUserDto)dto).Name;
            Surname = ((ApplicationUserDto)dto).Surname;
        }
    }
}
