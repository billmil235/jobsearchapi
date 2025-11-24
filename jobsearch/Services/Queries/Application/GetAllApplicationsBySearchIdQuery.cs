using jobsearch.Context;
using JobSearch.Models;
using Microsoft.EntityFrameworkCore;

namespace JobSearch.Services.Queries.Application;

public class GetAllApplicationsBySearchIdQuery(JobSearchContext jobSearchContext)
{
    public async IAsyncEnumerable<ApplicationModel> ListApplications(Guid searchId, int pageNumber, int pageSize, bool activeOnly = false, bool includeDeleted = false)
    {
        var applicationListQuery = jobSearchContext.Applications
            .Where(s => s.SearchId == searchId);

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
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        foreach (var application in applicationList)
        {
            yield return application;
        }
    }
}