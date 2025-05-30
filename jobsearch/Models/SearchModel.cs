using JobSearch.Entities;

namespace JobSearch.Models;

public record SearchModel
{
    public Guid SearchId { get; set; }
    public Guid UserId { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public string SearchName { get; set; } = "Job Search";

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