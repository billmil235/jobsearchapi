using JobSearch.Models;
using Microsoft.EntityFrameworkCore;

namespace JobSearch.Services.Queries.Application;

public class GetAllApplicationsBySearchIdQuery(JobSearchContext jobSearchContext)
{
    public async IAsyncEnumerable<ApplicationModel> ListApplications(string searchId, bool activeOnly = false, bool includeDeleted = false)
    {
        var applicationListQuery = jobSearchContext.Applications
            .Where(s => s.SearchId == new Guid(searchId));

        if (!includeDeleted)
        {
            applicationListQuery = applicationListQuery.Where(a => a.Deleted == false);
        }

        if (activeOnly)
        {
            applicationListQuery = applicationListQuery.Where(a => a.Active == true);
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
}