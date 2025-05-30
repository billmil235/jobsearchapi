using JobSearch.Entities;

namespace JobSearch.Models;

public record ApplicationModel
{
    public string? ApplicationId { get; set; }
    public string SearchId { get; init; } = Guid.Empty.ToString();
    public DateTime ApplicationDate { get; init; }
    public string CompanyName { get; init; } = string.Empty;
    public string? CompanyWebSite { get; init; }
    public int ApplicationTypeId { get; init; }
    public int ApplicationSourceTypeId { get; init; }
    public decimal? LowSalary { get; init; }
    public decimal? HighSalary { get; init; }
    public decimal? RequestedSalary { get; init; }
    public string? JobTitle { get; init; }

    public static ApplicationModel FromApplicationEntity(Application application)
    {
        return new ApplicationModel
        {
            ApplicationId = application.ApplicationId.ToString(),
            ApplicationDate = application.ApplicationDate.ToDateTime(new TimeOnly(0, 0, 0)),
            ApplicationTypeId = application.ApplicationTypeId,
            ApplicationSourceTypeId = application.ApplicationSourceTypeId,
            CompanyWebSite = application.CompanyWebSite,
            CompanyName = application.CompanyName,
            SearchId = application.SearchId.ToString(),
            LowSalary = application.LowSalary,
            HighSalary = application.HighSalary,
            RequestedSalary = application.RequestedSalary,
            JobTitle = application.JobTitle
        };
    }
}