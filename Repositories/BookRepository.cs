using BookLibrary.Data;
using BookLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Book = BookLibrary.Models.Book;

namespace BookLibrary.Repositories;

public class BookRepository
{
    public ApplicationDatabaseContext Сontext;

    public BookRepository(ApplicationDatabaseContext context)
    {
        this.Сontext = context;
    }

   
    public async Task<IEnumerable<Book?>> GetAllAsync()
    {

        return await Сontext.Books.ToListAsync();

    }

    public async Task<Book?> GetByIdAsync(Guid id)
    {
        return await Сontext.Books.FirstOrDefaultAsync(t => t.Id == id);
    }

    public bool Add(Book entity)
    {
        Сontext.Books.Add(entity);
        return Save();
    }

    public bool Update(Book entity)
    {
        
        Сontext.Books.Update(entity);

        return Save();
    }

    public bool Delete(Book entity)
    {
        Сontext.Books.Remove(entity);
        return Save();
    }

    public bool Save()
    {
        var saved = Сontext.SaveChanges();
        return saved > 0;
    }
}
