using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobSearch.Entities;

[Table("applicationsourcetype")]
public class ApplicationSourceType
{
    [Key]
    [Column("applicationsourcetypeid")]
    public int ApplicationSourceTypeId { get; set; } 
    
    [Column("applicationsourcetypename")]
    public string ApplicationSourceTypeName { get; set; }
}