using System.Security.Cryptography.X509Certificates;
using JobSearch.Entities;
using JobSearch.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobSearch.Services;

public class SearchService(JobSearchContext jobSearchContext)
{
    public async IAsyncEnumerable<SearchModel> GetSearchesForUser(Guid userId, Guid? searchId = null)
    {
        var searches = await jobSearchContext.Searches
            .Where(s => s.UserId == userId && s.Deleted == false && (s.SearchId == searchId || searchId == null))
            .Select(search => new SearchModel 
            { 
                SearchId = search.SearchId,
                StartDate = search.StartDate,
                EndDate = search.EndDate,
                SearchName = search.SearchName,
                UserId = userId
            })
            .ToListAsync();

        foreach(var search in searches)
        {
            yield return search;
        }
    }

    public async Task<IResult> CreateNewSearch(SearchModel searchModel,  Guid userId)
    {
        var newSearch = new Search() 
        {
            UserId = userId,
            SearchName = searchModel.SearchName,
            StartDate = searchModel.StartDate,
            EndDate = null
        };

        try
        {
            await jobSearchContext.Searches.AddAsync(newSearch);
            await jobSearchContext.SaveChangesAsync();

            return Results.Ok(newSearch);
        }
        catch (Exception)
        {
            return Results.Problem(new ProblemDetails
            {
                Detail = "Failed to create new search.",
                Status = StatusCodes.Status500InternalServerError,
                Title = "Internal server error"
            });
        }
    }

    public async Task<IResult> DeleteSearch(Guid searchId, Guid userId)
    {
        var oldSearch = await jobSearchContext.Searches.FirstOrDefaultAsync(s => s.SearchId == searchId && s.UserId == userId);

        if (oldSearch != null)
        {
            oldSearch.Deleted = true;
            await jobSearchContext.SaveChangesAsync();
            return Results.Ok();
        }

        return Results.Problem(new ProblemDetails
        {
            Detail = "Failed to delete job search.",
            Status = StatusCodes.Status404NotFound,
            Title = "Job search not found"
        });
    }

    public async Task<IResult> UpdateSearch(SearchModel searchModel, Guid userId)
    {
        var oldSearch = await jobSearchContext.Searches.FirstOrDefaultAsync(s => s.SearchId == searchModel.SearchId && s.UserId == userId);

        if (oldSearch != null)
        {
            oldSearch.SearchName = searchModel.SearchName;
            oldSearch.StartDate = searchModel.StartDate;
            oldSearch.EndDate = searchModel.EndDate;

            await jobSearchContext.SaveChangesAsync();
            
            return Results.Ok();
        }

        return Results.Problem(new ProblemDetails
        {
            Detail = "Failed to update job search.",
            Status = StatusCodes.Status404NotFound,
            Title = "Job search not found"
        });
    }
}