using jobsearch.Context;

namespace JobSearch.Endpoints;

public static class ActivityEndpoints
{
    public static void RegisterActivityEndpoints(this WebApplication app)
    {
        app.MapPost("/AddActivity", (JobSearchContext db) => Results.Ok() )
            .RequireAuthorization("user");
    }
}