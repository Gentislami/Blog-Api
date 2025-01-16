namespace Blog_Api.src.Dtos
{
    public class BlogPostDto : IDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public bool IsPublished { get; set; }
    }
}