using jobsearch.Context;
using JobSearch.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace jobsearch.Services.Commands.Application;

public class CreateApplicationCommand(JobSearchContext jobSearchContext)
{
    public async Task<Results<Ok<ApplicationModel>, ProblemHttpResult>> CreateApplication(ApplicationModel application)
    {
        var applicationEntity = JobSearch.Entities.Application.Create(
            application.ApplicationDate,
            application.ApplicationSourceTypeId,
            application.ApplicationTypeId,
            application.CompanyName,
            application.CompanyWebSite,
            application.JobTitle,
            new Guid(application.SearchId),
            application.LowSalary,
            application.HighSalary,
            application.RequestedSalary
        );

        try
        {
            await jobSearchContext.Applications.AddAsync(applicationEntity);
            application.ApplicationId = applicationEntity.ApplicationId.ToString();
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