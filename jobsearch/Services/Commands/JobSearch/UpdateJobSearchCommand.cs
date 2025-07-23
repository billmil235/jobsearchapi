using JobSearch.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobSearch.Services.Commands.JobSearch;

public class UpdateJobSearchCommand(JobSearchContext jobSearchContext)
{
    public async Task<Results<Ok, ProblemHttpResult>> UpdateSearch(SearchModel searchModel, Guid userId)
    {
        var oldSearch = await jobSearchContext.Searches.FirstOrDefaultAsync(s => s.SearchId == searchModel.SearchId && s.UserId == userId);

        if (oldSearch == null)
        {
            return TypedResults.Problem(new ProblemDetails
            {
                Detail = "Failed to update job search.",
                Status = StatusCodes.Status404NotFound,
                Title = "Job search not found"
            });
        }
        
        oldSearch.SearchName = searchModel.SearchName;
        oldSearch.StartDate = searchModel.StartDate;
        oldSearch.EndDate = searchModel.EndDate;

        await jobSearchContext.SaveChangesAsync();
            
        return TypedResults.Ok();

    }
}