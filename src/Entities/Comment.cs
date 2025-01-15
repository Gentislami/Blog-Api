namespace Blog_Api.src.Entities
{
    public class Comment : AbstractEntity
    {
        public string Text { get; set; }
        public DateTime PostedAt { get; set; }

        public int BlogPostId { get; set; }

        public BlogPost BlogPost { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
    }

}
