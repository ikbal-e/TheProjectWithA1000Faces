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
    public class AuthorRepository : IAuthorRepository
    {
        private readonly BookStoreContext _context;
        public AuthorRepository(BookStoreContext context)
        {
            _context = context;
        }
        public async Task<Author> GetAuthorById(int id)
        {
            return await _context.Authors.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Author>> GetAuthors()
        {
            return await _context.Authors.ToListAsync();
        }
        public async Task<Author> AddAuthor(Author author)
        {
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();

            return author;
        }

        public async Task ChangeAuthorName(int id, string name)
        {
            var author = await _context.Authors.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (author is null) throw new ArgumentException("Author id is wrong");

            author.Name = name;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAuthor(int id)
        {
            var author = await _context.Authors.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (author is null) throw new ArgumentException("Author id is wrong");

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
        }
    }
}
