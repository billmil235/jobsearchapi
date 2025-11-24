using jobsearch.Context;
using JobSearch.Models;
using Microsoft.EntityFrameworkCore;

namespace JobSearch.Services.Queries.JobSearch;

public class GetJobSearchByUserIdQuery(JobSearchContext jobSearchContext)
{
    public async IAsyncEnumerable<SearchModel> GetSearchesForUser(Guid userId, int pageNumber, int pageSize, bool? activeOnly, Guid? searchId = null)
    {
        var searches = await jobSearchContext.Searches
            .Where(s => s.UserId == userId)
            .Where(s => s.Deleted == false)
            .Where(s => s.SearchId == searchId || searchId == null)
            .Where(s => activeOnly != true || s.EndDate == null)
            .Select(search => SearchModel.FromSearch(search))
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        foreach(var search in searches)
        {
            yield return search;
        }
    }
}