using Microsoft.AspNetCore.Mvc;
using BookLibrary.Repositories;
using BookLibrary.ViewModel;
using Book = BookLibrary.Models.Book;
using BookLibrary.Services;
using static System.Net.Mime.MediaTypeNames;

namespace BookLibrary.Controllers;

public class BookController : Controller
{
    private readonly BookRepository _bookRepository;
    private readonly FileUploadService _fileUploadService;

    public BookController(BookRepository bookRepos, FileUploadService fileUploadService)
    {
        _bookRepository = bookRepos;
        _fileUploadService = fileUploadService;
    }

    /// <summary>
    /// Посмотреть книги.
    /// </summary>
    /// <returns></returns>
    public async Task<IActionResult> Books()
    {
        var allAsync = _bookRepository.GetAllAsync();
        return View(await allAsync);
    }
    
    /// <summary>
    /// Создать книгу.
    /// </summary>
    /// <returns></returns>
    public async Task<IActionResult> Create()
    {
        var vm = new BookViewModel();
        
        return View(vm);
    }

    /// <summary>
    /// Редактировать книгу.
    /// </summary>
    /// <param name="id">Id книги.</param>
    /// <returns></returns>
    public async Task<IActionResult> Edit(Guid id)
    {
        var byIdAsync = await _bookRepository.GetByIdAsync(id);

        if (byIdAsync == null) return NotFound();

        var vm = new BookViewModel();
        vm.currentBook = byIdAsync;


        return View(vm);
    }

    /// <summary>
    /// Удалить книгу.
    /// </summary>
    /// <param name="id">Id книги.</param>
    /// <returns></returns>
    public async Task<IActionResult> Delete(Guid id)
    {
        var byIdAsync = await _bookRepository.GetByIdAsync(id);

        if (byIdAsync == null) return NotFound();

        return View(byIdAsync);
    }

    /// <summary>
    /// Посмотреть книгу.
    /// </summary>
    /// <param name="id">Id книги.</param>
    /// <returns></returns>
    public async Task<IActionResult> Details(Guid id)
    {
        var book = await _bookRepository.GetByIdAsync(id);

        if (book == null) return NotFound();

        return View(book);
    }

    /// <summary>
    /// Скачать файл.
    /// </summary>
    /// <param name="id">Id книги.</param>
    /// <returns></returns>
    public async Task<IActionResult> Download(Guid id)
    {
        var book = await _bookRepository.GetByIdAsync(id);
        var stream = new FileStream(book.FilePath, FileMode.Open);

        return File(stream, GetContentType(book.FilePath), Path.GetFileName(book.FilePath));
    }

    /// <summary>
    /// Получить контент файла.
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    private string GetContentType(string path)
    {
        var types = GetMimeTypes();
        var ext = Path.GetExtension(path).ToLowerInvariant();
        return types[ext];
    }

    /// <summary>
    /// Словарь контентов.
    /// </summary>
    /// <returns></returns>
    private Dictionary<string, string> GetMimeTypes()
    {
        return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"},
                {".pptx", "application/vnd.openxmlformats-officedocument.presentationml.presentation" }
            };
    }


    /// <summary>
    /// Обновить книгу.
    /// </summary>
    /// <param name="book">Книга.</param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> PostUpdate(BookViewModel book)
    {
        var oldbook = await _bookRepository.GetByIdAsync(book.currentBook.Id);
        var oldbooktitle = oldbook.Title;
        var oldbookfilepath = oldbook.FilePath;

        var toUpdate = new Book();

        toUpdate = await _bookRepository.GetByIdAsync(book.currentBook.Id);
        toUpdate.Title = book.currentBook.Title;
        toUpdate.Author = book.currentBook.Author;
        toUpdate.Description = book.currentBook.Description;
        toUpdate.FilePath = oldbook.FilePath.Replace(oldbooktitle, toUpdate.Title);


        System.IO.File.Move(oldbookfilepath, toUpdate.FilePath);

        var update = _bookRepository.Update(toUpdate);

        if (!update) return View("Error");

        return RedirectToAction("Books");
    }

    /// <summary>
    /// Создать книгу.
    /// </summary>
    /// <param name="book">Книга.</param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> PostCreate(BookViewModel book)
    {
        var toAdd = new Book();
        toAdd = book.currentBook;
        toAdd.FilePath = await _fileUploadService.UploadFile(book.FileUpload, toAdd.Title);
        
        var add = _bookRepository.Update(toAdd);


        if (!add) return View("Error");

        Console.WriteLine();

        return RedirectToAction("Books");
    }

    /// <summary>
    /// Удалить книгу.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> PostDelete(Guid id)
    {
        var book =  await _bookRepository.GetByIdAsync(id);
        System.IO.File.Delete(book.FilePath);
        var delete = _bookRepository.Delete(book);

        if (!delete) return View("Error");

        return RedirectToAction("Books");
    }
}