using JobSearch.Entities;
using Microsoft.EntityFrameworkCore;

namespace jobsearch.Context;

public class JobSearchContext(DbContextOptions<JobSearchContext> options) : DbContext(options)
{
    public DbSet<Search> Searches => Set<Search>();

    public DbSet<Application> Applications => Set<Application>();

    public DbSet<Contact> Contacts => Set<Contact>();

    public DbSet<Users> Users => Set<Users>();
    
    public DbSet<ApplicationContact> ApplicationContacts => Set<ApplicationContact>();
    
    public DbSet<ApplicationType> ApplicationTypes => Set<ApplicationType>();
    public DbSet<ApplicationSourceType> ApplicationSourceTypes => Set<ApplicationSourceType>();
    
    public DbSet<ApplicationActivity> ApplicationActivities { get; set; }
    public DbSet<ApplicationActivityType> ApplicationActivityTypes { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema("public");

        base.OnModelCreating(builder);
    }
}