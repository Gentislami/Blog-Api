using Blog_Api.src.Dtos;
using Blog_Api.src.Entities;
using Blog_Api.src.Repositories;
using Blog_Api.src.Services.Entity;
using Microsoft.AspNetCore.Mvc;

namespace Blog_Api.src.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostsController(IBlogPostEntityService blogPostEntityService) : AbstractController<BlogPost, BlogPostRepository, IBlogPostEntityService, BlogPostDto>(blogPostEntityService)
    {
    }
}
