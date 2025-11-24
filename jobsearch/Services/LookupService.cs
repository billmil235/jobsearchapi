using jobsearch.Context;
using JobSearch.Entities;
using JobSearch.Models.Lookups;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;

namespace JobSearch.Services;

public class LookupService(JobSearchContext jobSearchContext, HybridCache cache)
{
    public async ValueTask<List<ApplicationSourceTypeLookup>> GetApplicationSourceTypes()
    {
        return await cache.GetOrCreateAsync(
            $"LookUps:ApplicationSourceTypes",
            Factory,
            null,
            null,
            CancellationToken.None
        );

        async ValueTask<List<ApplicationSourceTypeLookup>> Factory(CancellationToken cancel) => 
            await jobSearchContext.ApplicationSourceTypes.Select(appType => new ApplicationSourceTypeLookup
            {
                ApplicationSourceTypeId = appType.ApplicationSourceTypeId,
                ApplicationSourceTypeName = appType.ApplicationSourceTypeName,
            }).ToListAsync(cancellationToken: cancel);
    }

    public async ValueTask<List<ApplicationTypeLookup>> GetApplicationTypes()
    {
        
        return await cache.GetOrCreateAsync(
            $"LookUps:ApplicationTypes",
            Factory,
            null,
            null,
            CancellationToken.None
        );
        
        async ValueTask<List<ApplicationTypeLookup>> Factory(CancellationToken cancel) => 
            await jobSearchContext.ApplicationTypes.Select(appType => new ApplicationTypeLookup
            {
                ApplicationTypeId = appType.ApplicationTypeId,
                ApplicationTypeName = appType.ApplicationTypeName,
            }).ToListAsync(cancellationToken: cancel);
    }
    
    public async ValueTask<List<ApplicationActivityTypeLookup>> GetApplicationActivityTypes()
    {
        return await cache.GetOrCreateAsync(
            $"LookUps:ApplicationActivityTypes",
            Factory,
            null,
            null,
            CancellationToken.None
        );

        async ValueTask<List<ApplicationActivityTypeLookup>> Factory(CancellationToken cancel) => 
            await jobSearchContext.ApplicationActivityTypes.Select(appType => new ApplicationActivityTypeLookup
            {
                ApplicationActivityTypeId = appType.ApplicationActivityTypeId,
                ApplicationActivityTypeName = appType.ApplicationActivityTypeName,
            }).ToListAsync(cancellationToken: cancel);
    }
}