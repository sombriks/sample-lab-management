using Microsoft.EntityFrameworkCore;

namespace sample_lab_management.App.Models;

public class ModelsContext : DbContext
{
  public ModelsContext(DbContextOptions<ModelsContext> options)
          : base(options)
  {
  }

  public DbSet<Student> Students { get; set; }
  public DbSet<Laboratory> Laboratories { get; set; }
  public DbSet<Project> Projects { get; set; }

}