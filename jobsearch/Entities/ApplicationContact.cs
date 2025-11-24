using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobSearch.Entities;

public class ApplicationContact
{
    [Key]
    public int ApplicationContactId { get; set; }
    
    public Guid ApplicationId { get; set; }
    
    [NotMapped]
    public object ContactType { get; set; }
    
    [NotMapped]
    public object ContactValue { get; set; }
}