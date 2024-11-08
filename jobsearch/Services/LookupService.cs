using System.Collections;
using JobSearch.Entities;
using Microsoft.EntityFrameworkCore;

namespace JobSearch.Services;

public class LookupService(JobSearchContext jobSearchContext)
{
    private readonly JobSearchContext _jobSearchContext = jobSearchContext;

    public async IAsyncEnumerable<ApplicationSourceType> GetApplicationSourceTypes()
    {
        var applicationSourceTypes = await _jobSearchContext.ApplicationSourceTypes.ToListAsync();

        foreach (var sourceType in applicationSourceTypes)
        {
            yield return sourceType;
        }
    }

    public async IAsyncEnumerable<ApplicationType> GetApplicationTypes()
    {
        var applicationTypes = await _jobSearchContext.ApplicationTypes.ToListAsync();

        foreach (var applicationType in applicationTypes)
        {
            yield return applicationType;
        }
    }
}