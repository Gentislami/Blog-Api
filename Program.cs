
using Blog_Api.src.Data;
using Blog_Api.src.Entities;
using Blog_Api.src.Repositories;
using Blog_Api.src.Services.Entity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Blog_Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<Blog_ApiContext>(options =>
                options.UseLazyLoadingProxies().UseNpgsql(builder.Configuration.GetConnectionString("Blog_ApiContext") ?? throw new InvalidOperationException("Connection string not found.")));

            RegisterRepositories(builder);
            RegisterServices(builder);

            #region Security

            #region Authentication

            builder.Services.AddIdentityApiEndpoints<ApplicationUser>().AddEntityFrameworkStores<Blog_ApiContext>();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = true;
                        options.SaveToken = true;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = builder.Configuration["Authentication:Jwt:Issuer"],
                            ValidAudience = builder.Configuration["Authentication:Jwt:Audience"],
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Authentication:Jwt:SecretKey"]))
                        };
                    });

            #endregion

            #region CORS

            string corsAllowedOrigins = builder.Configuration["Cors:AllowedOrigins"]!;
            string corsAllowedMethods = builder.Configuration["Cors:AllowedMethods"]!;

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin", builder =>
                {
                    builder.WithOrigins(corsAllowedOrigins)
                           .WithMethods(corsAllowedMethods)
                           .AllowAnyHeader();
                });
            });

            #endregion

            builder.Services.AddAuthorization();


            #endregion

            builder.Services.AddControllers().AddJsonOptions(
                options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
                }
            );

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("AllowSpecificOrigin");
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapIdentityApi<ApplicationUser>();



            app.MapControllers();

            app.Run();
        }

        private static void RegisterRepositories(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IBlogRepository, BlogRepository>();
            builder.Services.AddScoped<IBlogPostRepository, BlogPostRepository>();
            builder.Services.AddScoped<ICommentRepository, CommentRepository>();
            builder.Services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
        }

        private static void RegisterServices(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IApplicationUserEntityService, ApplicationUserEntityService>();
            builder.Services.AddScoped<IBlogEntityService, BlogEntityService>();
            builder.Services.AddScoped<IBlogPostEntityService, BlogPostEntityService>();
            builder.Services.AddScoped<ICommentEntityService, CommentEntityService>();
        }
    }
}