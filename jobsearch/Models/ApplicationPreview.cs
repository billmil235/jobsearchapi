namespace JobSearch.Models;

public class ApplicationPreview
{
    public required Guid ApplicationId { get; set; }
    public required string CompanyName { get; set; }
    public DateOnly ApplicationDate { get; set; }
    public string? Notes { get; set; }
}