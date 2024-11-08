using System.Security.Cryptography;
using System.Text;
using JobSearch.Entities;
using JobSearch.Models;
using Microsoft.EntityFrameworkCore;

namespace JobSearch.Services;

public class UsersService(
    JobSearchContext jobSearchContext,
    TokenService tokenService)
{
    private readonly JobSearchContext _jobSearchContext = jobSearchContext;
    private readonly TokenService _tokenService = tokenService;

    public async Task<IResult> RegisterUser(UserModel user)
    {
        var dbUser = await _jobSearchContext.Users.FirstOrDefaultAsync(u => u.UserName == user.Username);

        if(dbUser == null) 
        {
            Users newUser = new()
            {
                UserName = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Password = string.Empty,
                DateOfBirth = user.DateOfBirth,
                EMailVerified = false
            };

            await _jobSearchContext.Users.AddAsync(newUser);
            await _jobSearchContext.SaveChangesAsync();

            user.Password = HashPassword(user.Password, newUser.UserId);
            
            await _jobSearchContext.SaveChangesAsync();

            return Results.Ok();
        }
        else
            return Results.BadRequest(new { message = "Username already in use." });
    }

    public async Task<IResult> Login(AuthenticateUserModel user)
    {
        var dbUser = await _jobSearchContext.Users.FirstOrDefaultAsync(u => u.UserName == user.Username);

        if(dbUser == null)
            return Results.NotFound(new { message = "Invalid username or password." });

        var verified = VerifyUser(user.Password, dbUser.Password, dbUser.UserId);

        if (!verified)
            return Results.NotFound(new { message = "Invalid username or password" });

        var token = _tokenService.GenerateToken(user, dbUser.UserId);

        var userModel = new UserModel()
        {
            FirstName = dbUser.FirstName,
            LastName = dbUser.LastName,
            DateOfBirth = dbUser.DateOfBirth
        };

        return Results.Ok(new { User = userModel, Token = token });
    }

    private static bool VerifyUser(string submittedPassword, string storedPassword, Guid userId)
    {
        var submittedPasswordHash = HashPassword(submittedPassword, userId);
        return submittedPasswordHash == storedPassword;
    }

    private static string HashPassword(string password, Guid userId) 
    {
        var hashedPasswordBytes = Encoding.UTF8.GetBytes($"{userId.ToString()}{password}");

        for(int round = 0; round < 10000; round++) 
            hashedPasswordBytes = SHA512.HashData(hashedPasswordBytes);

        var hashedInputStringBuilder = new System.Text.StringBuilder(128);

        foreach (var b in hashedPasswordBytes)
            hashedInputStringBuilder.Append(b.ToString("X2"));
        
        return hashedInputStringBuilder.ToString();
    }
}