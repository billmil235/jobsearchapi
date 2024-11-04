using Microsoft.EntityFrameworkCore;

namespace JobSearch.Endpoints;

public static class ApplicationEndpoints
{
    public static void RegisterApplicationEndpoints(this WebApplication app)
    {
        app.MapGet("/ListApplications", async (JobSearchContext db) =>
            await db.Applications.ToListAsync())
        .RequireAuthorization("user");

        app.MapGet("/ApplicationTypes", (JobSearchContext db) => Results.Ok() )
        .RequireAuthorization("user");

        app.MapGet("/ApplicationSources", (JobSearchContext db) => Results.Ok() )
        .RequireAuthorization("user");

        app.MapPost("/CreateApplicationContact", (JobSearchContext db) => Results.Ok() )
        .RequireAuthorization("user");
    }
}