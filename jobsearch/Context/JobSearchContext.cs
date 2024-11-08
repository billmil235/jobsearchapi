using System.Data.Common;
using JobSearch.Entities;
using Microsoft.EntityFrameworkCore;

public class JobSearchContext : DbContext
{
    public JobSearchContext(DbContextOptions<JobSearchContext> options) : base(options) { }

    public DbSet<Search> Searches => Set<Search>();

    public DbSet<Application> Applications => Set<Application>();

    public DbSet<Contact> Contacts => Set<Contact>();

    public DbSet<Users> Users => Set<Users>();
    
    public DbSet<ApplicationType> ApplicationTypes => Set<ApplicationType>();
    public DbSet<ApplicationSourceType> ApplicationSourceTypes => Set<ApplicationSourceType>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema("public");

        base.OnModelCreating(builder);
    }
}