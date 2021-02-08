using Face2.Entity;
using Face2.Repository.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Face2.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorsController(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> Get()
        {
            var books = await _authorRepository.GetAuthors();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> Get(int id)
        {
            var book = await _authorRepository.GetAuthorById(id);

            if (book is null) return NotFound();

            return Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult<Author>> Post([FromBody] Author author)
        {
            var newAuthor = await _authorRepository.AddAuthor(author);
            return CreatedAtAction(nameof(Get), new { id = newAuthor.Id }, newAuthor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] string name)
        {
            await _authorRepository.ChangeAuthorName(id, name);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _authorRepository.DeleteAuthor(id);

            return NoContent();
        }
    }
}
