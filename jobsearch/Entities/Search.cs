using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JobSearch.Services.Commands.JobSearch;

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

    public IEnumerable<Application> Applications { get; set; } = [];

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