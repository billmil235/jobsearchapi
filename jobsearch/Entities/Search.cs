using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobSearch.Entities;

[Table("search")]
public class Search 
{
    [Key]
    [Column("searchid")]
    public Guid SearchId { get; set; }

    [Column("userid")]
    public Guid UserId { get; set; }

    [Column("startdate")]
    public DateOnly StartDate { get; set; }

    [Column("enddate")]
    public DateOnly? EndDate { get; set; }

    [Column("searchname")]
    public string SearchName { get; set; } = "Job Search";
    
    [Column("deleted")]
    public bool Deleted { get; set; } = false;
}