using System.Security.Cryptography;
using System.Text;
using jobsearch.Context;
using JobSearch.Entities;
using JobSearch.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobSearch.Services;

public class UsersService(
    JobSearchContext jobSearchContext,
    TokenService tokenService)
{
    public async Task<IResult> RegisterUser(UserModel user)
    {
        var dbUser = await FindUser(user.Username);

        if (dbUser != null)
        {
            return Results.Problem(new ProblemDetails
            {
                Detail = "Username already in use.",
                Status = StatusCodes.Status409Conflict,
                Title = "Failed to register user"
            });
        }

        var userId = Guid.NewGuid();
        Users newUser = new()
        {
            UserId = userId,
            UserName = user.Username.ToLower(),
            FirstName = user.FirstName,
            LastName = user.LastName,
            Password = HashPassword(user.Password, userId),
            DateOfBirth = DateOnly.FromDateTime(user.DateOfBirth),
            EMailVerified = false
        };
        
        await jobSearchContext.Users.AddAsync(newUser);
        await jobSearchContext.SaveChangesAsync();

        return Results.Ok();

    }

    public async Task<IResult> Login(AuthenticateUserModel user)
    {
        var dbUser = await FindUser(user.Username);

        if (dbUser == null)
        {
            return Results.Problem(new ProblemDetails
            {
                Detail = "Invalid username or bad password.",
                Status = StatusCodes.Status403Forbidden,
                Title = "Failed to login"
            });
        }

        var verified = VerifyUser(user.Password, dbUser.Password, dbUser.UserId);

        if (!verified)
        {
            return Results.Problem(new ProblemDetails
            {
                Detail = "Invalid username or bad password.",
                Status = StatusCodes.Status403Forbidden,
                Title = "Failed to login"
            });
        }

        var token = tokenService.GenerateToken(user, dbUser.UserId);

        var userModel = new UserModel()
        {
            FirstName = dbUser.FirstName,
            LastName = dbUser.LastName,
            DateOfBirth = dbUser.DateOfBirth.ToDateTime(new TimeOnly(0,0,0))
        };

        return Results.Ok(new { User = userModel, Token = token });
    }

    private async Task<Users?> FindUser(string userName)
    { 
        return await jobSearchContext.Users
            .FirstOrDefaultAsync(u => u.UserName == userName.ToLower());
    }
    
    private static bool VerifyUser(string submittedPassword, string storedPassword, Guid userId)
    {
        var submittedPasswordHash = HashPassword(submittedPassword, userId);
        return submittedPasswordHash == storedPassword;
    }

    private static string HashPassword(string password, Guid userId) 
    {
        var hashedPasswordBytes = Encoding.UTF8.GetBytes($"{userId.ToString()}{password}");

        for(var round = 0; round < 10000; round++) 
            hashedPasswordBytes = SHA512.HashData(hashedPasswordBytes);

        var hashedInputStringBuilder = new System.Text.StringBuilder(128);

        foreach (var b in hashedPasswordBytes)
            hashedInputStringBuilder.Append(b.ToString("X2"));
        
        return hashedInputStringBuilder.ToString();
    }
}