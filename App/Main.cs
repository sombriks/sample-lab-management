namespace sample_lab_management.App;

using Microsoft.EntityFrameworkCore;

using sample_lab_management.App.Models;

public class Main
{
  public WebApplication app { get; }

  public Main(string[] args)
  {
    var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddControllers();
    builder.Services.AddDbContext<ModelsContext>(opt =>
        // opt.UseInMemoryDatabase("lab_management"));
        opt.UseSqlite("Data Source=./lab_management.db"));

    app = builder.Build();
    app.MapControllers();
    app.MapGet("/status", () => "ONLINE");

    using (var scope = app.Services.CreateScope())
    {
      var dbContext = scope.ServiceProvider.GetRequiredService<ModelsContext>();
      dbContext.Database.Migrate();
    }
  }
}