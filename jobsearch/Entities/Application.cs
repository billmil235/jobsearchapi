using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("application")]
public class Application
{
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
}