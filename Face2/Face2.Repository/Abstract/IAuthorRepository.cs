using Face2.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Face2.Repository.Abstract
{
    public interface IAuthorRepository
    {
        Task<List<Author>> GetAuthors();
        Task<Author> GetAuthorById(int id);
        Task<Author> AddAuthor(Author author);
        Task ChangeAuthorName(int id, string name);
        Task DeleteAuthor(int id);
    }
}
