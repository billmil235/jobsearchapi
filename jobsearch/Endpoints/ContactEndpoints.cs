using JobSearch.Models;
using jobsearch.Services.Commands.Contacts;

namespace JobSearch.Endpoints;

public static class ContactEndpoints
{
    public static RouteGroupBuilder RegisterContactEndpoints(this RouteGroupBuilder group)
    {
        group.MapPost("/create",
                async (CreateApplicationContactCommand command, ApplicationContactModel contact) =>
                    await command.ExecuteAsync(contact));

        return group;
    }
}