using JobSearch.Entities;

namespace JobSearch.Models;

public record ApplicationModel
{
    public Guid? ApplicationId { get; set; }
    public required Guid SearchId { get; init; }
    public DateTime ApplicationDate { get; init; }
    public required string CompanyName { get; init; }
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
            ApplicationId = application.ApplicationId,
            ApplicationDate = application.ApplicationDate.ToDateTime(new TimeOnly(0, 0, 0)),
            ApplicationTypeId = application.ApplicationTypeId,
            ApplicationSourceTypeId = application.ApplicationSourceTypeId,
            CompanyWebSite = application.CompanyWebSite,
            CompanyName = application.CompanyName,
            SearchId = application.SearchId,
            LowSalary = application.LowSalary,
            HighSalary = application.HighSalary,
            RequestedSalary = application.RequestedSalary,
            JobTitle = application.JobTitle
        };
    }
}