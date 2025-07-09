using Blog_Api.src.Entities;

namespace Blog_Api.src.Dtos
{
    public class BlogPostDto : IDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public bool IsPublished { get; set; }
        public string BlogId { get; set; }
        public string UserId { get; set; }
    }
}