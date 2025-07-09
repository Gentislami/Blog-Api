using Blog_Api.src.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NuGet.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Blog_Api.src.Api.Controllers.Authentication;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController(UserManager<ApplicationUser> userManager, IConfiguration configuration) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginModel model)
    {
        var user = await userManager.FindByNameAsync(model.Username);
        if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
        {
            var authClaims = new List<Claim>
            {
                new(ClaimTypes.Name, user.UserName),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Authentication:Jwt:SecretKey"]));

            var token = new JwtSecurityToken(
                issuer: configuration["Authentication:Jwt:Issuer"],
                audience: configuration["Authentication:Jwt:Audience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256Signature)
            );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo,
                userId = user.Id
            });
        }

        return Unauthorized("Invalid credentials.");
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegistrationModel model)
    {
        if (model.Password != model.ConfirmPassword)
        {
            return BadRequest("Passwords do not match.");
        }

        var existingUserByUsername = await userManager.FindByNameAsync(model.UserName);
        if (existingUserByUsername != null)
        {
            return BadRequest("Username already exists.");
        }

        var user = new ApplicationUser
        {
            UserName = model.UserName,
            Email = model.Email,
            Name = model.Name,
            Surname = model.Surname,
            CreatedAt = DateTime.UtcNow
        };

        var result = await userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors.Select(e => e.Description));
            
        }

        return await this.Login(new LoginModel { Username = model.UserName, Password = model.Password });
    }

    [Authorize]
    [HttpGet("check-login")]
    public void CheckState()
    {
        // basically either fail or dont. Nothing needs to *happen*
    }

    //[HttpGet("logout")]
    //public async Task<IResult> Logout(SignInManager<IdentityUser> signInManager, [FromBody] object empty)
    //{
    //    if (empty != null)
    //    {
    //        await signInManager.SignOutAsync();
    //        return Results.Ok();
    //    }
    //    return Results.Unauthorized();
    //}
}

public class LoginModel
{
    public string Username { get; set; }
    public string Password { get; set; }
}

public class RegistrationModel : ApplicationUser
{
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}