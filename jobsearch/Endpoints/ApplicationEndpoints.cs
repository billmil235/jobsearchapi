using System.Security.Claims;
using JobSearch.Services;
using Microsoft.EntityFrameworkCore;

namespace JobSearch.Endpoints;

public static class ApplicationEndpoints
{
    public static void RegisterApplicationEndpoints(this WebApplication app)
    {
        app.MapGet("/ListApplications/{searchId}", async (string searchId, ClaimsPrincipal user, JobSearchContext db) =>
            await db.Applications.Where(s => s.SearchId == new Guid(searchId)).ToListAsync())
        .RequireAuthorization("user");

        app.MapGet("/ApplicationTypes", (LookupService lookupService) => Results.Ok(lookupService.GetApplicationTypes()) )
        .RequireAuthorization("user");

        app.MapGet("/ApplicationSources", (LookupService lookupService) => Results.Ok(lookupService.GetApplicationSourceTypes()) )
        .RequireAuthorization("user");

        app.MapPost("/CreateApplicationContact", (JobSearchContext db) => Results.Ok() )
        .RequireAuthorization("user");
    }
}