using Face1.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Face1.Context
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly BookStoreContext _context;

        public AuthorsController(BookStoreContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Author>> Get()
        {
            var authors = _context.Authors.ToList();

            return Ok(authors);
        }

        [HttpGet("{id}")]
        public ActionResult<Author> Get(int id)
        {
            var author = _context.Authors.Where(x => x.Id == id).FirstOrDefault();

            if (author is null) return NotFound();

            return Ok(author);
        }

        [HttpPost]
        public ActionResult<Author> Post([FromBody] Author author)
        {
            _context.Authors.Add(author);
            _context.SaveChanges();

            return CreatedAtAction(nameof(Get), new { id = author.Id }, author);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] string name)
        {
            var author = _context.Authors.Where(x => x.Id == id).FirstOrDefault();

            if (author is null) return NotFound();

            author.Name = name;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var author = _context.Authors.Where(x => x.Id == id).FirstOrDefault();

            if (author is null) return NotFound();

            _context.Authors.Remove(author);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
