using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Models;

/// <summary>
/// Книга.
/// </summary>
public class Book
{
  /// <summary>
  /// Id книги.
  /// </summary>
  public Guid Id { get; set; }
  /// <summary>
  /// Название книги.
  /// </summary>
  public string? Title { get; set; }
  /// <summary>
  /// Автор книги.
  /// </summary>
  public string? Author { get; set; }
  /// <summary>
  /// Описание книги.
  /// </summary>
  public string? Description { get; set; }
  
    /// <summary>
  /// Путь файла в каталоге.
  /// </summary>
  public string? FilePath { get; set; }
  
}