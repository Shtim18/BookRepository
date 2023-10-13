using Microsoft.EntityFrameworkCore;
using BookLibrary.Models;
using Book = BookLibrary.Models.Book;
using Microsoft.Extensions.Options;

namespace BookLibrary.Data;

/// <summary>
/// Контекст БД.
/// </summary>
public class ApplicationDatabaseContext : DbContext
{

  /// <summary>
  /// Книга в БД.
  /// </summary>
  public DbSet<Book> Books { get; set; }

  /// <summary>
  /// Базовый конструктор для настройки БД.
  /// </summary>
  /// <param name="options"></param>
  public ApplicationDatabaseContext(DbContextOptions<ApplicationDatabaseContext> options) : base(options) { }


  protected override void OnModelCreating(ModelBuilder modelBuilder) 
    {

    }
}