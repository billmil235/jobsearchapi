using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobSearch.Services.Commands.JobSearch;

public class DeleteSearchBySearchIdCommand(JobSearchContext jobSearchContext)
{
    public async Task<Results<Ok, ProblemHttpResult>> DeleteSearch(Guid searchId, Guid userId)
    {
        var rowsAffected = await jobSearchContext.Searches
            .Where(s => s.SearchId == searchId)
            .ExecuteUpdateAsync(b => b.SetProperty(u => u.Deleted, true));

       if (rowsAffected == 0)
       {
           return TypedResults.Problem(new ProblemDetails
           {
               Detail = "Failed to delete job search.",
               Status = StatusCodes.Status404NotFound,
               Title = "Job search not found"
           });        
       }

       return TypedResults.Ok();

    }
}