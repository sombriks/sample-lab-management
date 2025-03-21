namespace sample_lab_management.App.Models;

public class Student
{
  public long Id { get; set; }
  public string? Name { get; set; }
  public List<Laboratory> Laboratories { get; } = [];
  public List<Project> Projects { get; } = [];
}