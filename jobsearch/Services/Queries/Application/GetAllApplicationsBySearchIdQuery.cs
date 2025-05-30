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
            .Select(x => ApplicationModel.FromApplicationEntity(x))
            .ToListAsync();

        foreach (var application in applicationList)
        {
            yield return application;
        }
    }
}