using Face2.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Face2.Repository.Abstract
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetBooks();
        Task<Book> GetBookById(int id);
        Task<Book> AddBook(Book book);
        Task ChangeBookName(int id, string name);
        Task DeleteBook(int id);
    }
}
