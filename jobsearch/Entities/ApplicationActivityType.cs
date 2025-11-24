
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobSearch.Entities;

[Table("applicationactivitytype")]
public class ApplicationActivityType
{
    [Key]
    [Column("applicationactivitytypeid")]
    public int ApplicationActivityTypeId { get; set; }

    [Column("applicationactivitytypename")]
    [Required]
    [StringLength(50)]
    public string ApplicationActivityTypeName { get; set; } = null!;

    [Column("applicationendstate")]
    public bool ApplicationEndState { get; set; }
}
