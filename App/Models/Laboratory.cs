namespace sample_lab_management.App.Models;

public class Laboratory
{
  public long Id { get; set; }
  public string? Name { get; set; }
  public List<Project> Projects { get; } = [];
  public List<Student> Students { get; } = [];
}