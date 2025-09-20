using MaruMaterialManager.Components;
using MaruMaterialManager.Model;
using MaruMaterialManager.Services;
using Microsoft.EntityFrameworkCore;

namespace MaruMaterialManager;

public class Program
{
  public static void Main(string[] args)
  {
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Services.AddRazorComponents()
      .AddInteractiveServerComponents();

    // Add DbContext with PostgreSQL
    builder.Services.AddDbContext<PartsDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")),
        ServiceLifetime.Scoped);

    // Register services
    builder.Services.AddScoped<PartService>();
    
    // Add HTTP context accessor for logging and other services that might need it
    builder.Services.AddHttpContextAccessor();
    
    // Add logging
    builder.Services.AddLogging();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
      app.UseExceptionHandler("/Error");
      // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
      app.UseHsts();
    }
    else
    {
      // Apply database migrations in development
      using (var scope = app.Services.CreateScope())
      {
          var services = scope.ServiceProvider;
          try
          {
              var context = services.GetRequiredService<PartsDbContext>();
              context.Database.EnsureCreated();
              // For initial development, you can use this to apply migrations:
              // context.Database.Migrate();
          }
          catch (Exception ex)
          {
              var logger = services.GetRequiredService<ILogger<Program>>();
              logger.LogError(ex, "An error occurred while migrating the database.");
          }
      }
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseAntiforgery();

    app.MapRazorComponents<App>()
      .AddInteractiveServerRenderMode();

    app.Run();
  }
}