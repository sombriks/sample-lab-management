namespace App;

using Microsoft.EntityFrameworkCore;

using Models;

public class Main
{
  private WebApplication app;

  public Main(string[] args)
  {
    var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddControllers();
    builder.Services.AddDbContext<StudentContext>(opt =>
        opt.UseInMemoryDatabase("lab_management"));

    app = builder.Build();
    app.MapControllers();

    app.MapGet("/status", () => "ONLINE");

  }

  public void Run()
  {
    app.Run();
  }

}