namespace Blog_Api.src.Entities
{
    public class User : AbstractEntity
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public ICollection<BlogPost> BlogPosts { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Blog> Blogs { get; set; }
    }

}
