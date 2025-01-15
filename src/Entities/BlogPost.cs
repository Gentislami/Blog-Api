namespace Blog_Api.src.Entities
{
    public class BlogPost : AbstractEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublishedAt { get; set; }

        public int BlogId { get; set; }

        public Blog Blog { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
