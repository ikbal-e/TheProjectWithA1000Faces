using Face1.Context;
using Face1.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Face1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookStoreContext _context;
        public BooksController(BookStoreContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Book>> Get()
        {
            var books = _context.Books
                .Include(x => x.Author)
                .ToList();

            return Ok(books);
        }

        [HttpGet("{id}")]
        public ActionResult<Book> Get(int id)
        {
            var book = _context.Books.Where(x => x.Id == id).FirstOrDefault();

            if (book is null) return NotFound();

            return Ok(book);
        }

        [HttpPost]
        public ActionResult<Book> Post([FromBody] Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();

            _context.Update(book);
            return CreatedAtAction(nameof(Get), new { id = book.Id }, book);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] string name)
        {
            var book = _context.Books.Where(x => x.Id == id).FirstOrDefault();

            if (book is null) return NotFound();

            book.Name = name;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var book = _context.Books.Where(x => x.Id == id).FirstOrDefault();

            if (book is null) return NotFound();

            _context.Books.Remove(book);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
