using JobSearch.Models;
using JobSearch.Services;

namespace JobSearch.Endpoints;

public static class UsersEndpoints
{
    public static void RegisterUsersEndpoints(this WebApplication app)
    {
        app.MapPost("/Users/Register", (UserModel user, UsersService usersService) => {
            return usersService.RegisterUser(user);
        });

        app.MapPost("/Login", (AuthenticateUserModel user, UsersService usersService) => {
            return usersService.Login(user);
        });
    }
}