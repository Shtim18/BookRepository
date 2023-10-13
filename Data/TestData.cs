
namespace BookLibrary.Data;

/// <summary>
/// Заполнение БД тестовыми данными.
/// </summary>
public static class TestData
{
  public static void TData(IApplicationBuilder applicationBuilder)
  {
    using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();
    var context = serviceScope.ServiceProvider.GetService<ApplicationDatabaseContext>();

    context.Database.EnsureCreated();

  }
}