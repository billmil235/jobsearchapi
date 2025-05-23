using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobSearch.Services.Commands.JobSearch;

public class DeleteSearchBySearchIdCommand(JobSearchContext jobSearchContext)
{
    public async Task<Results<Ok, ProblemHttpResult>> DeleteSearch(Guid searchId, Guid userId)
    {
        var oldSearch = await jobSearchContext.Searches.FirstOrDefaultAsync(s => s.SearchId == searchId && s.UserId == userId);

        if (oldSearch != null)
        {
            oldSearch.Deleted = true;
            await jobSearchContext.SaveChangesAsync();
            return TypedResults.Ok();
        }

        return TypedResults.Problem(new ProblemDetails
        {
            Detail = "Failed to delete job search.",
            Status = StatusCodes.Status404NotFound,
            Title = "Job search not found"
        });
    }
}