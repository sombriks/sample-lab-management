namespace App;

public class Main
{
  private WebApplication app;

  public Main(string[] args)
  {
    var builder = WebApplication.CreateBuilder(args);

    app = builder.Build();

    app.MapGet("/", () => "Hello World!");

  }

  public void Run()
  {
    app.Run();
  }

}