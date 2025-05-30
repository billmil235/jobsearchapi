using JobSearch.Entities;
using JobSearch.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace JobSearch.Services.Commands.JobSearch;

public class CreateJobSearchCommand(JobSearchContext jobSearchContext)
{
    public async Task<Results<Ok<Search>, ProblemHttpResult>> CreateNewSearch(SearchModel searchModel,  Guid userId)
    {
        try
        {
            var newSearch = Search.Create(userId, searchModel.StartDate, searchModel.SearchName);
            await jobSearchContext.Searches.AddAsync(newSearch);
            await jobSearchContext.SaveChangesAsync();

            return TypedResults.Ok(newSearch);
        }
        catch (Exception)
        {
            return TypedResults.Problem(new ProblemDetails
            {
                Detail = "Failed to create new search.",
                Status = StatusCodes.Status500InternalServerError,
                Title = "Internal server error"
            });
        }
    }
}