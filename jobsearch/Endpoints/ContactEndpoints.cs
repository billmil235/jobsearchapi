namespace JobSearch.Endpoints;

public static class ContactEndpoints
{
    public static void RegisterContactEndpoints(this WebApplication app)
    {
        app.MapPost("/CreateApplicationContact", (JobSearchContext db) => Results.Ok() )
            .RequireAuthorization("user");
    }
}