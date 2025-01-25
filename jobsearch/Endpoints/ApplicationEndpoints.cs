using System.Security.Claims;
using JobSearch.Models;
using JobSearch.Services;

namespace JobSearch.Endpoints;

public static class ApplicationEndpoints
{
    public static RouteGroupBuilder RegisterApplicationEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/List/{searchId}", 
            (string searchId, ClaimsPrincipal user, ApplicationService applicationService) => applicationService.ListApplications(searchId));

        group.MapPost("/", 
            (Application application, ClaimsPrincipal user, ApplicationService applicationService) => applicationService.CreateApplication(application));
        
        group.MapGet("/Types", 
            (LookupService lookupService) => Results.Ok(lookupService.GetApplicationTypes()) );

        group.MapGet("/Sources", 
            (LookupService lookupService) => Results.Ok(lookupService.GetApplicationSourceTypes()) );

        group.MapDelete("/{applicationId}",
            (string applicationId, ApplicationService applicationService) => applicationService.DeleteApplication(applicationId));

        return group;
    }
}