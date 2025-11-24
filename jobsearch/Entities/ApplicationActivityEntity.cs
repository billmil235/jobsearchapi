using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobSearch.Entities;

[Table("applicationactivity")]
public class ApplicationActivity
{
    [Key]
    [Column("applicationactivityid")]
    public Guid ApplicationActivityId { get; set; }
    
    [Column("applicationid")]
    [ForeignKey("Application")]
    public Guid ApplicationId { get; set; }
    
    [Column("applicationactivitytime")]
    public DateTime? ApplicationActivityTime { get; set; }
    
    [Column("applicationactivitytypeid")]
    [ForeignKey("ApplicationActivityType")]
    public int ApplicationActivityTypeId { get; set; }
    
    // Navigation properties
    public Application? Application { get; set; }
    public ApplicationActivityType? ApplicationActivityType { get; set; }
}
