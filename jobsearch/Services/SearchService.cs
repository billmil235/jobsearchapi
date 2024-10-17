using Microsoft.EntityFrameworkCore;

public class SearchService(JobSearchContext jobSearchContext)
{
    private readonly JobSearchContext _jobSearchContext = jobSearchContext;

    public async IAsyncEnumerable<SearchModel> GetSearchesForUser(Guid userId)
    {
        var searches = await _jobSearchContext.Searches
            .Where(s => s.UserId == userId)
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

    public async Task<IResult> CreateNewSeach(SearchModel searchModel,  Guid userId)
    {
        var newSearch = new Search() 
        {
            UserId = userId,
            SearchName = searchModel.SearchName,
            StartDate = searchModel.StartDate,
            EndDate = null
        };

        _jobSearchContext.Searches.Add(newSearch);
        await _jobSearchContext.SaveChangesAsync();

        return Results.Ok(newSearch);
    }
}