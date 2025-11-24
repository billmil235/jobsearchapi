using jobsearch.Context;
using JobSearch.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobSearch.Services.Queries.Application;

public class GetApplicationPreviewByApplicationIdQuery(JobSearchContext jobSearchContext)
{
    public async Task<Results<Ok<ApplicationPreview>, ProblemHttpResult>> GetApplicationPreview(string applicationId)
    {
        var application = await jobSearchContext.Applications.FirstOrDefaultAsync(x => x.ApplicationId == new Guid(applicationId));

        if (application == null)
        {
            return TypedResults.Problem(new ProblemDetails
            {
                Detail = "Failed to retrieve application.",
                Status = StatusCodes.Status404NotFound,
                Title = "Application not found"
            });
        }

        var preview = new ApplicationPreview()
        {
            CompanyName = application.CompanyName,
            ApplicationId = application.ApplicationId,
            ApplicationDate = application.ApplicationDate,
            LowSalary = application.LowSalary,
            HighSalary = application.HighSalary,
            RequestedSalary = application.RequestedSalary,
            Notes = string.Empty
        };

        return TypedResults.Ok(preview);
    }
}