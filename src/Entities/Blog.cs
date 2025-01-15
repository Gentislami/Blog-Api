namespace Blog_Api.src.Entities
{
    public class Blog : AbstractEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public ICollection<BlogPost> BlogPosts { get; set; }
    }

}
