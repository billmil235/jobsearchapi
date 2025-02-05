using JobSearch.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;

namespace JobSearch.Services;

public class LookupService(JobSearchContext jobSearchContext, HybridCache cache)
{
    public async ValueTask<List<ApplicationSourceType>> GetApplicationSourceTypes()
    {
        return await cache.GetOrCreateAsync(
            $"LookUps:ApplicationSourceTypes",
            Factory,
            null,
            null,
            CancellationToken.None
        );

        async ValueTask<List<ApplicationSourceType>> Factory(CancellationToken cancel) => 
            await jobSearchContext.ApplicationSourceTypes.Select(appType => new ApplicationSourceType
            {
                ApplicationSourceTypeId = appType.ApplicationSourceTypeId,
                ApplicationSourceTypeName = appType.ApplicationSourceTypeName,
            }).ToListAsync(cancellationToken: cancel);
    }

    public async ValueTask<List<ApplicationType>> GetApplicationTypes()
    {
        
        return await cache.GetOrCreateAsync(
            $"LookUps:ApplicationTypes",
            Factory,
            null,
            null,
            CancellationToken.None
        );
        
        async ValueTask<List<ApplicationType>> Factory(CancellationToken cancel) => 
            await jobSearchContext.ApplicationTypes.Select(appType => new ApplicationType
            {
                ApplicationTypeId = appType.ApplicationTypeId,
                ApplicationTypeName = appType.ApplicationTypeName,
            }).ToListAsync(cancellationToken: cancel);
    }
}