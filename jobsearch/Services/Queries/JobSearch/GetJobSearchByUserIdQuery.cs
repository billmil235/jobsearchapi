using JobSearch.Models;
using Microsoft.EntityFrameworkCore;

namespace JobSearch.Services.Queries.JobSearch;

public class GetJobSearchByUserIdQuery(JobSearchContext jobSearchContext)
{
    public async IAsyncEnumerable<SearchModel> GetSearchesForUser(Guid userId, Guid? searchId = null)
    {
        var searches = await jobSearchContext.Searches
            .Where(s => s.UserId == userId && s.Deleted == false && (s.SearchId == searchId || searchId == null))
            .Select(search => new SearchModel 
            { 
                SearchId = search.SearchId,
                StartDate = search.StartDate,
                EndDate = search.EndDate,
                SearchName = search.SearchName,
                UserId = userId
            })
            .ToListAsync();

        foreach(var search in searches)
        {
            yield return search;
        }
    }
}