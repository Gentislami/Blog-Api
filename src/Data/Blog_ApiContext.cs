using Blog_Api.src.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blog_Api.src.Data;

public class Blog_ApiContext(DbContextOptions<Blog_ApiContext> options) : IdentityDbContext<ApplicationUser>(options)//DbContext(options)
{
    public DbSet<Blog> Blog { get; set; } = default!;
    public DbSet<BlogPost> BlogPost { get; set; } = default!;
    public DbSet<Comment> Comment { get; set; } = default!;


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        #region blog filtering
        /**
         * 
         * By default the back-end only hides blogs if the blogs and users themselves are deleted,
         * let the front-end decide if/how they want to display posts & comments by deleted users
         * made on blog posts within not deleted blogs.
         * 
         * Filtering out deleted users entirely can be done by appending  <code>|| entity.User.DeletedAt == null</code>
         * to reduce traffic and client side load.
         */
        modelBuilder.Entity<Blog>().HasQueryFilter(blog => blog.DeletedAt == null || blog.User.DeletedAt == null);
        modelBuilder.Entity<BlogPost>().HasQueryFilter(blogpost => blogpost.DeletedAt == null || blogpost.Blog.DeletedAt == null);
        modelBuilder.Entity<Comment>().HasQueryFilter(comment => comment.DeletedAt == null || comment.BlogPost.DeletedAt == null || comment.BlogPost.Blog.DeletedAt == null);
        modelBuilder.Entity<ApplicationUser>().HasQueryFilter(user => user.DeletedAt == null);
        #endregion
    }
}