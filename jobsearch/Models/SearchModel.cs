public class SearchModel
{
    public Guid SearchId { get; set; }
    public Guid UserId { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public string SearchName { get; set; } = "Job Search";
}