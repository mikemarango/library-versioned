using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Api.Helpers;
using Library.Api.Models;
using Library.Api.Models.DTOs;
using Library.Api.Services.LibServices;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        public ILibraryRepository Repository { get; }

        public AuthorsController(ILibraryRepository repository)
        {
            Repository = repository;
        }
        // GET api/authors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorDto>>> Get()
        {
            var authors = await Repository.GetAuthors();

            var authorDtos = authors.Select(author => new AuthorDto
            {
                Id = author.Id,
                Name = $"{author.FirstName} {author.LastName}",
                Genre = author.Genre,
                Age = author.DateOfBirth.GetCurrentAge()
            }).ToList();

            return authorDtos;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetAsync(Guid id)
        {
            var author = await Repository.GetAuthor(id);
            if (author == null)
                return NotFound();
            var authorDto = new AuthorDto()
            {
                Id = author.Id,
                Name = $"{author.FirstName}, {author.LastName}",
                Genre = author.Genre,
                Age = author.DateOfBirth.GetCurrentAge()
            };
            return Ok(authorDto);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
