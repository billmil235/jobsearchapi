using System.Security.Claims;
using JobSearch.Extensions;
using JobSearch.Models;
using JobSearch.Services;

namespace JobSearch.Endpoints;

public static class JobSearchEndpoints
{
    public static void RegisterJobSearchEndpoints(this WebApplication app)
    {
        app.MapGet("/Searches", (string? searchId, ClaimsPrincipal user, SearchService searchService) =>
        {
            Guid? searchIdGuid = null;
            var userId = user.GetGuid();
            
            if (searchId != null)
            {
                searchIdGuid = new Guid(searchId);
            }
            return searchService.GetSearchesForUser(userId, searchIdGuid);
        })
        .RequireAuthorization("user");
        
        app.MapPost("/CreateSeach", async (ClaimsPrincipal user, SearchService searchService, SearchModel search) => 
        {
            var userId = user.GetGuid();
            return await searchService.CreateNewSearch(search, userId);
        })
        .RequireAuthorization("user");
    }
}