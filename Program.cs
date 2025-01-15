
using Blog_Api.src.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Blog_Api.src.Data;

namespace Blog_Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<Blog_ApiContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("Blog_ApiContext") ?? throw new InvalidOperationException("Connection string 'Blog_ApiContext' not found.")));
            // Add services to the container.

            RegisterRepositories(builder);

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }

        private static void RegisterRepositories(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IBlogRepository, BlogRepository>();
            builder.Services.AddScoped<IBlogPostRepository, BlogPostRepository>();
            builder.Services.AddScoped<ICommentRepository, CommentRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
