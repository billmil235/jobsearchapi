using System.Security.Claims;
using JobSearch.Extensions;
using JobSearch.Models;
using JobSearch.Services;
using JobSearch.Services.Commands.JobSearch;
using JobSearch.Services.Queries.JobSearch;

namespace JobSearch.Endpoints;

public static class JobSearchEndpoints
{
    public static void RegisterJobSearchEndpoints(this WebApplication app)
    {
        app.MapGet("/Searches", (string? searchId, ClaimsPrincipal user, GetJobSearchByUserIdQuery getJobSearchByUserIdQuery) =>
        {
            Guid? searchIdGuid = null;
            var userId = user.GetGuid();
            
            if (searchId != null)
            {
                searchIdGuid = new Guid(searchId);
            }
            return getJobSearchByUserIdQuery.GetSearchesForUser(userId, searchIdGuid);
        })
        .RequireAuthorization("user");
        
        app.MapPost("/CreateSeach", async (ClaimsPrincipal user, CreateJobSearchCommand createJobSearchCommand, SearchModel search) => 
        {
            var userId = user.GetGuid();
            return await createJobSearchCommand.CreateNewSearch(search, userId);
        })
        .RequireAuthorization("user");
    }
}