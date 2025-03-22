using System.Text.Json.Serialization;

namespace sample_lab_management.App;

using Microsoft.EntityFrameworkCore;

using sample_lab_management.App.Models;

public class Main
{
  public WebApplication Application { get; }

  public Main(string[] args)
  {
    var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddControllers()
        .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

    builder.Services.AddOpenApiDocument();
    
    builder.Services.AddDbContext<ModelsContext>(opt =>
        // opt.UseInMemoryDatabase("lab_management"));
        opt.UseSqlite("Data Source=./lab_management.db"));

    Application = builder.Build();
    Application.MapControllers();
    Application.UseOpenApi();
    Application.UseSwaggerUi();
    Application.MapGet("/status", () => "ONLINE");

    using var scope = Application.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<ModelsContext>();
    dbContext.Database.Migrate();
  }
}