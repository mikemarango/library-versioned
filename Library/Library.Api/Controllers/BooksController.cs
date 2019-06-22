using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Api.Models;
using Library.Api.Models.DTOs;
using Library.Api.Services.LibServices;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Library.Api.Controllers
{
    [Route("api/authors/{authorId}/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        public ILibraryRepository Repository { get; }

        public BooksController(ILibraryRepository repository)
        {
            Repository = repository;
        }
        // GET: api/authors/1/books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetBooks(Guid authorId)
        {
            if (!await Repository.AuthorExists(authorId))
                return NotFound();
            var books = await Repository.GetBooks(authorId);
            var bookDtos = books.Select(book => new BookDto()
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
            }).ToList();

            return bookDtos;
        }

        // GET: api/authors/1/books/1
        [HttpGet("{id}", Name = "GetBook")]
        public async Task<ActionResult<BookDto>> GetBook(Guid authorId, Guid id)
        {
            if (!await Repository.AuthorExists(authorId))
                return NotFound();
            var book = await Repository.GetBook(authorId, id);
            if (book == null)
                return NotFound();
            var bookDto = new BookDto()
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description
            };

            return bookDto;
        }
    }
}
