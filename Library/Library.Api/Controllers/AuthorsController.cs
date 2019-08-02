using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
        public IMapper Mapper { get; }

        public AuthorsController(ILibraryRepository repository, IMapper mapper)
        {
            Repository = repository;
            Mapper = mapper;
        }
        // GET api/authors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorDto>>> Get()
        {
            var authors = await Repository.GetAuthors();
            Stopwatch stopwatch = Stopwatch.StartNew();
            //var authorDtos = Mapper.Map<IEnumerable<Author>>(authors);
            var authorDtos = authors.Select(author => new AuthorDto
            {
                Id = author.Id,
                Name = $"{author.FirstName} {author.LastName}",
                Genre = author.Genre,
                Age = author.DateOfBirth.GetCurrentAge()
            }).ToList();
            stopwatch.Stop();

            return authorDtos;
        }
        // GET api/authors/5
        [HttpGet("{id}", Name = "GetAuthor")]
        public async Task<ActionResult<Author>> GetAuthor(Guid id)
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

        [HttpPost]
        public async Task<ActionResult<AuthorCreateDto>> AddAuthor([FromBody] AuthorCreateDto authorCreateDto)
        {
            if (authorCreateDto == null)
                return BadRequest();

            var author = Mapper.Map<Author>(authorCreateDto);

            await Repository.AddAuthor(author);

            if (!await Repository.SaveChanges())
                return StatusCode(500);

            var authorDto = Mapper.Map<AuthorDto>(author);

            return CreatedAtRoute("GetAuthor", new { id = authorDto.Id }, authorDto);
        }
    }
}
