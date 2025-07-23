using System.Security.Claims;
using JobSearch.Extensions;
using JobSearch.Models;
using JobSearch.Services.Commands.JobSearch;
using JobSearch.Services.Queries.JobSearch;
using Microsoft.AspNetCore.Mvc;

namespace JobSearch.Endpoints;

public static class JobSearchEndpoints
{
    public static void RegisterJobSearchEndpoints(this WebApplication app)
    {
        app.MapGet("/Searches", (Guid? searchId, bool? activeOnly, ClaimsPrincipal user, GetJobSearchByUserIdQuery getJobSearchByUserIdQuery, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10) =>
        {
            var userId = user.GetGuid();
            return getJobSearchByUserIdQuery.GetSearchesForUser(userId, pageNumber, pageSize, activeOnly, searchId);
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