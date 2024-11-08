using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.VisualBasic;

namespace JobSearch.Entities;

[Table("applicationtype")]
public class ApplicationType
{
    [Key]
    [Column("applicationtypeid")]
    public int ApplicationTypeId { get; set; }

    [Column("applicationtypename")] 
    [MaxLength(50)]
    public string ApplicationTypeName { get; set; } = string.Empty;
}