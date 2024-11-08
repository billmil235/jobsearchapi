namespace JobSearch.Models;

public class Application
{
    public Guid ApplicationId { get; set; }
    public Guid SearchId { get; set; }
    public DateOnly ApplicationDate { get; set; }
    public string CompanyName { get; set; }
    public string CompanyWebSite { get; set; }
    public int ApplicationTypeId { get; set; }
    public int ApplicationSourceTypeId { get; set; }
}