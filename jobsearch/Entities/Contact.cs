using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobSearch.Entities;

[Table("Contact")]
public class Contact
{

    [Column("contactid")]
    [Key]
    public Guid ContactId { get; set; }

    [Column("userid")]
    public Guid UserId {get;set;}
    
    [Column("contactname")]
    [MaxLength(100)]
    public string ContactName { get; set; } = string.Empty;
    
    [Column("companyname")]
    [MaxLength(100)]
    public string? CompanyName { get; set; }
    
    [Column("phonenumber")]
    [MaxLength(10)]
    public string? PhoneNumber { get; set; }
    
    [Column("emailaddress")]
    [MaxLength(100)]
    public string? EmailAddress { get; set; }
    
    [Column("deleted")]
    public bool Deleted { get; set; }
}