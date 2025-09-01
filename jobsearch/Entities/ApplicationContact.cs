using System.ComponentModel.DataAnnotations;

namespace JobSearch.Entities;

public class ApplicationContact
{
    [Key]
    public int ApplicationContactId { get; set; }
    
    public Guid ApplicationId { get; set; }
    
    public object ContactType { get; set; }
    
    public object ContactValue { get; set; }
}