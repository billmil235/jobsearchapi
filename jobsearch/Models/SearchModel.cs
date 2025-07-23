using JobSearch.Entities;

namespace JobSearch.Models;

public record SearchModel
{
    public required Guid SearchId { get; init; }
    public required Guid UserId { get; init; }
    public required DateOnly StartDate { get; init; }
    public DateOnly? EndDate { get; init; }
    public required string SearchName { get; init; }

    public static SearchModel FromSearch(Search search)
    {
        return new SearchModel
        {
            SearchId = search.SearchId,
            StartDate = search.StartDate,
            EndDate = search.EndDate,
            SearchName = search.SearchName,
            UserId = search.UserId
        };
    }
}