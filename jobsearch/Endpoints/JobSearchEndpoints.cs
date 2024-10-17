using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

public static class JobSearchEndpoints
{
    public static void RegisterJobSearchEndpoints(this WebApplication app)
    {
        app.MapGet("/Searches", (ClaimsPrincipal user, SearchService searchService) =>
        {
            var guid = user.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).First().Value;
            var userId = new Guid(guid!);
            return searchService.GetSearchesForUser(userId);
        })
        .RequireAuthorization("user");
        
        app.MapPost("/CreateSeach", async (ClaimsPrincipal user, SearchService searchService, SearchModel search) => 
        {
            var guid = user.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).First().Value;
            var userId = new Guid(guid);
            return await searchService.CreateNewSeach(search, userId);
        })
        .RequireAuthorization("user");
    }
}