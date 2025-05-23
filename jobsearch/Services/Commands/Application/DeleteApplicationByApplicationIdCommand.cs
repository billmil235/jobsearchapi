using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace jobsearch.Services.Commands.Application;

public class DeleteApplicationByApplicationIdCommand(JobSearchContext jobSearchContext)
{
    public async Task<IResult> DeleteApplication(string applicationId)
    {
        var application = await jobSearchContext.Applications
            .FirstOrDefaultAsync(x => x.ApplicationId == new Guid(applicationId));

        if (application == null)
        {
            return Results.Problem(new ProblemDetails {
                Detail = "Failed to delete application.",
                Status = StatusCodes.Status404NotFound,
                Title = "Application not found"
            });
        }
        
        application.Deleted = true;
        await jobSearchContext.SaveChangesAsync();
        return Results.Ok();

    }
}