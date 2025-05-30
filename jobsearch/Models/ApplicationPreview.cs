using JobSearch.Entities;

namespace JobSearch.Models;

public record ApplicationPreview
{
    public required Guid ApplicationId { get; set; }
    public required string CompanyName { get; set; }
    public DateOnly ApplicationDate { get; set; }
    public string? Notes { get; set; }
    public decimal? LowSalary { get; set; }
    public decimal? HighSalary { get; set; }
    public decimal? RequestedSalary { get; set; }

    public static ApplicationPreview FromApplicationEntity(Application application)
    {
        return new ApplicationPreview()
        {
            ApplicationId = application.ApplicationId,
            ApplicationDate = application.ApplicationDate,
            CompanyName = application.CompanyName,
            Notes = string.Empty,
            LowSalary = application.LowSalary,
            HighSalary = application.HighSalary,
            RequestedSalary = application.RequestedSalary
        };
    }
}