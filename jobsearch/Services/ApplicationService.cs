using JobSearch.Entities;
using Microsoft.EntityFrameworkCore;
using ApplicationModel = JobSearch.Models.Application;

namespace JobSearch.Services;

public class ApplicationService(JobSearchContext jobSearchContext)
{
    public async IAsyncEnumerable<ApplicationModel> ListApplications(string searchId, bool includeDeleted = false)
    {
        var applicationListQuery = jobSearchContext.Applications
            .Where(s => s.SearchId == new Guid(searchId));

        if (!includeDeleted)
        {
            applicationListQuery = applicationListQuery.Where(a => a.Deleted == false);
        }
        
        var applicationList = await applicationListQuery
            .Select(x => new ApplicationModel
            {
                ApplicationDate = x.ApplicationDate.ToDateTime(new TimeOnly(0,0,0)),
                ApplicationTypeId = x.ApplicationTypeId,
                ApplicationSourceTypeId = x.ApplicationSourceTypeId,
                ApplicationId = x.ApplicationId.ToString(),
                CompanyWebSite = x.CompanyWebSite,
                CompanyName = x.CompanyName,
                SearchId = x.SearchId.ToString(),
            })
            .ToListAsync();

        foreach (var application in applicationList)
        {
            yield return application;
        }
    }
    
    public async Task<IResult> CreateApplication(ApplicationModel application)
    {
        var applicationEntity = new Application
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

            return Results.Ok(application);
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }

    public async Task<IResult> DeleteApplication(string applicationId)
    {
        var application = await jobSearchContext.Applications
            .FirstOrDefaultAsync(x => x.ApplicationId == new Guid(applicationId));
        
        if (application != null)
        {
            application.Deleted = true;
            await jobSearchContext.SaveChangesAsync();
        }
        else
        {
            return Results.NotFound();
        }
        
        return Results.Ok();
    }
}