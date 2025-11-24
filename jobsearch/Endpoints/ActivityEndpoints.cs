using jobsearch.Context;
using JobSearch.Services;

namespace JobSearch.Endpoints;

public static class ActivityEndpoints
{
    public static RouteGroupBuilder RegisterActivityEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/ActivityTypes", 
            async (LookupService lookupService) => Results.Ok(await lookupService.GetApplicationActivityTypes()) );
        
        group.MapPost("/AddActivity", (JobSearchContext db) => Results.Ok() );

        return group;
    }
}