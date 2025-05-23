using JobSearch.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace jobsearch.Services.Commands.Application;

public class CreateApplicationCommand(JobSearchContext jobSearchContext)
{
    public async Task<Results<Ok<ApplicationModel>, ProblemHttpResult>> CreateApplication(ApplicationModel application)
    {
        var applicationEntity = new JobSearch.Entities.Application
        {
            ApplicationDate = DateOnly.FromDateTime(application.ApplicationDate),
            ApplicationSourceTypeId = application.ApplicationSourceTypeId,
            ApplicationTypeId = application.ApplicationTypeId,
            CompanyName = application.CompanyName,
            CompanyWebSite = application.CompanyWebSite,
            SearchId = new Guid(application.SearchId),
            Deleted = false
        };

        try
        {
            var savedApplication = await jobSearchContext.Applications.AddAsync(applicationEntity);
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