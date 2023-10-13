using Microsoft.AspNetCore.Mvc.Rendering;
using Book = BookLibrary.Models.Book;

namespace BookLibrary.ViewModel;

public class BookViewModel
{
  public Book currentBook { get; set; }
  public IFormFile FileUpload { get; set; }

}