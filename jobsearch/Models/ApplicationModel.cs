namespace JobSearch.Models;

public class ApplicationModel
{
    public string? ApplicationId { get; set; }
    public string SearchId { get; set; } = Guid.Empty.ToString();
    public DateTime ApplicationDate { get; set; }
    public string CompanyName { get; set; } = string.Empty;
    public string? CompanyWebSite { get; set; }
    public int ApplicationTypeId { get; set; }
    public int ApplicationSourceTypeId { get; set; }
    public decimal LowSalaryRange { get; set; }
    public decimal HighSalaryRange { get; set; }
    public decimal RequestedSalary { get; set; }
}