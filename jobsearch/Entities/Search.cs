using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobSearch.Entities;

[Table("search")]
public class Search 
{
    [Key]
    [Column("searchid")]
    public Guid SearchId { get; init; }

    [Column("userid")]
    public Guid UserId { get; init; }

    [Column("startdate")]
    public DateOnly StartDate { get; set; }

    [Column("enddate")]
    public DateOnly? EndDate { get; set; }

    [Column("searchname")]
    [MaxLength(100)]
    public string SearchName { get; set; } = string.Empty;
    
    [Column("deleted")]
    public bool Deleted { get; init; }

    public IEnumerable<Application> Applications { get; init; } = [];

    public static Search Create(Guid userId, DateOnly startDate, string searchName)
    {
        return new Search()
        {
            UserId = userId,
            StartDate = startDate,
            EndDate = null,
            SearchName = searchName
        };
    }
}