using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobSearch.Entities;

[Table("applicationtype")]
public class ApplicationType
{
    [Key]
    [Column("applicationtypeid")]
    public int ApplicationTypeId { get; set; }
    
    [Column("applicationtypename")]
    public string ApplicationTypeName { get; set; }
}