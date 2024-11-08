namespace JobSearch.Models;

public class Application
{
    public string? ApplicationId { get; set; }
    public string SearchId { get; set; } = new Guid().ToString();
    public DateTime ApplicationDate { get; set; }
    public string CompanyName { get; set; } = string.Empty;
    public string? CompanyWebSite { get; set; }
    public int ApplicationTypeId { get; set; }
    public int ApplicationSourceTypeId { get; set; }
}