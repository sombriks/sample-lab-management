using System.ComponentModel.DataAnnotations.Schema;

namespace sample_lab_management.App.Models;

public class Project
{
  public long Id { get; set; }
  public long LaboratoryId { get; set; }
  public string? Name { get; set; }
  public List<Student> Students { get; } = [];
}