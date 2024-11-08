using System.Security.Claims;
using JobSearch.Models;
using JobSearch.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace JobSearch.Endpoints;

public static class ApplicationEndpoints
{
    public static void RegisterApplicationEndpoints(this WebApplication app)
    {
        app.MapGet("/ListApplications/{searchId}", (string searchId, ClaimsPrincipal user, ApplicationService applicationService) => applicationService.ListApplications(searchId))
        .RequireAuthorization("user");

        app.MapPost("/CreateApplication", (Application application, ClaimsPrincipal user, ApplicationService applicationService) => applicationService.CreateApplication(application))
        .RequireAuthorization("user");
        
        app.MapGet("/ApplicationTypes", (LookupService lookupService) => Results.Ok(lookupService.GetApplicationTypes()) )
        .RequireAuthorization("user");

        app.MapGet("/ApplicationSources", (LookupService lookupService) => Results.Ok(lookupService.GetApplicationSourceTypes()) )
        .RequireAuthorization("user");

        app.MapPost("/CreateApplicationContact", (JobSearchContext db) => Results.Ok() )
        .RequireAuthorization("user");
    }
}