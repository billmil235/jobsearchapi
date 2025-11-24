using jobsearch.Context;
using JobSearch.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace jobsearch.Services.Commands.Application;

public class UpdateApplicationCommand(JobSearchContext jobSearchContext)
{
    public async Task<Results<Ok<ApplicationModel>, ProblemHttpResult>> UpdateApplication(ApplicationModel application)
    {
        try
        {
            var applicationEntity = jobSearchContext.Applications.Single(x => x.ApplicationId == new Guid(application.ApplicationId!));

            applicationEntity.Update(application.ApplicationDate,
                application.ApplicationSourceTypeId,
                application.ApplicationTypeId,
                application.CompanyName,
                application.CompanyWebSite,
                application.JobTitle,
                application.LowSalary,
                application.HighSalary,
                application.RequestedSalary);
            
            await jobSearchContext.SaveChangesAsync();

            return TypedResults.Ok(application);
        }
        catch (Exception)
        {
            return TypedResults.Problem(new ProblemDetails
            {
                Detail = "Unable to create application.",
                Status = StatusCodes.Status500InternalServerError,
                Title = "Internal server error"
            });
        }
    }
}