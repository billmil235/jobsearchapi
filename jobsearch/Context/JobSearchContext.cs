using System.Data.Common;
using Microsoft.EntityFrameworkCore;

public class JobSearchContext : DbContext
{
    public JobSearchContext(DbContextOptions<JobSearchContext> options) : base(options) { }

    public DbSet<Search> Searches => Set<Search>();

    public DbSet<Application> Applications => Set<Application>();

    public DbSet<Contact> Contacts => Set<Contact>();

    public DbSet<Users> Users => Set<Users>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema("public");

        base.OnModelCreating(builder);
    }
}