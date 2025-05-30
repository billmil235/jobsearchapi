using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobSearch.Entities;

[Table("application")]
public class Application
{
    private Application() { }

    [Column("applicationid")]
    [Key]
    public Guid ApplicationId { get; set; }

    [Column("searchid")]
    public Guid SearchId { get; set; }

    [Column("applicationdate")]
    public DateOnly ApplicationDate { get; set; }

    [Column("companyname")]
    [MaxLength(200)]
    public string CompanyName { get; set; } = "Company Name";

    [Column("companywebsite")]
    [MaxLength(200)]
    public string? CompanyWebSite { get; set; }

    [Column("applicationtypeid")]
    public int ApplicationTypeId { get; set; }
    
    [Column("applicationsourcetypeid")]
    public int ApplicationSourceTypeId { get; set; }
    
    [Column("deleted")]
    public bool Deleted { get; set; }
    
    [Column("active")]
    public bool Active { get; set; }
    
    [Column("lowsalary")]
    public decimal? LowSalary { get; set; }
    
    [Column("highsalary")]
    public decimal? HighSalary { get; set; } 
    
    [Column("requestedsalary")]
    public decimal? RequestedSalary { get; set; }

    [Column("jobtitle")]
    public string? JobTitle { get; set; }
    
    public static Application Create(DateTime applicationDate, 
        int applicationSourceTypeId, 
        int applicationTypeId, 
        string companyName, 
        string? companyWebSite, 
        string? jobTitle,
        Guid searchId,
        decimal? lowSalary,
        decimal? highSalary,
        decimal? requestedSalary)
    {
        return new Application()
        {
            ApplicationDate = DateOnly.FromDateTime(applicationDate),
            ApplicationSourceTypeId = applicationSourceTypeId,
            ApplicationTypeId = applicationTypeId,
            CompanyName = companyName,
            CompanyWebSite = companyWebSite,
            SearchId = searchId,
            LowSalary = lowSalary,
            HighSalary = highSalary,
            RequestedSalary = requestedSalary,
            Deleted = false,
            JobTitle = jobTitle,
        };
    }
}