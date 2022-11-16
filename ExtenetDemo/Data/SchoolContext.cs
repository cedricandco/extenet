using Extenet.Models;
using Microsoft.EntityFrameworkCore;

namespace Extenet.Data;

public class SchoolContext : DbContext
{
    public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
    {
    }

    public DbSet<Item> Items { get; set; }
    public DbSet<Sale> Enrollments { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Vendor> Vendors { get; set; }
    public DbSet<OfficeAssignment> OfficeAssignments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Item>().ToTable(nameof(Item))
            .HasMany(c => c.Vendors)
            .WithMany(i => i.Courses);
        modelBuilder.Entity<Client>().ToTable(nameof(Client));
        modelBuilder.Entity<Vendor>().ToTable(nameof(Vendor));
    }
}
