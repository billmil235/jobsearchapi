using jobsearch.Context;
using JobSearch.Models;
using Microsoft.EntityFrameworkCore;

namespace JobSearch.Services.Queries.Application;

public class GetApplicationByApplicationIdQuery(JobSearchContext context)
{
    public async Task<ApplicationModel> GetApplicationByApplicationId(Guid applicationId, Guid userId)
    {
        var applicationEntity = await context.Applications.SingleAsync(app => app.ApplicationId == applicationId);
        var applicationModel = ApplicationModel.FromApplicationEntity(applicationEntity);
        
        return applicationModel;
    }
}