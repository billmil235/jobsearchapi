using System.Security.Claims;
using JobSearch.Models;
using JobSearch.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace JobSearch.Endpoints;

public static class ApplicationEndpoints
{
    public static RouteGroupBuilder RegisterApplicationEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/List/{searchId}", 
            (string searchId, ClaimsPrincipal user, ApplicationService applicationService) => applicationService.ListApplications(searchId));

        group.MapPost("/", 
            (ApplicationModel application, ClaimsPrincipal user, ApplicationService applicationService) => applicationService.CreateApplication(application));
        
        group.MapGet("/Types", 
           async (LookupService lookupService) => Results.Ok(await lookupService.GetApplicationTypes()) );

        group.MapGet("/Sources", 
            async (LookupService lookupService) => Results.Ok(await lookupService.GetApplicationSourceTypes()) );

        group.MapDelete("/{applicationId}",
            (string applicationId, ApplicationService applicationService) => applicationService.DeleteApplication(applicationId));

        group.MapGet("/Preview/{applicationId}",
            (string applicationId, ClaimsPrincipal user, ApplicationService applicationService) => applicationService.GetApplicationPreview(applicationId) );

        return group;
    }
}