using Face2.Entity;
using Face2.Repository.Abstract;
using Face2.Repository.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Face2.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookStoreContext _context;
        public BookRepository(BookStoreContext context)
        {
            _context = context;
        }
        public async Task<Book> GetBookById(int id)
        {
            return await _context.Books
                .Where(x => x.Id == id)
                .Include(x => x.Author)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Book>> GetBooks()
        {
            return await _context.Books
                .Include(x => x.Author)
                .ToListAsync();
        }

        public async Task<Book> AddBook(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return book;
        }

        public async Task ChangeBookName(int id, string name)
        {
            var book = await _context.Books.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (book is null) throw new ArgumentException("Book id is wrong");

            book.Name = name;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBook(int id)
        {
            var book = await _context.Books.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (book is null) throw new ArgumentException("Book id is wrong");

            _context.Remove(book);
            await _context.SaveChangesAsync();
        }
    }
}
