using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Api.Models;

namespace Library.Api.Services.LibServices
{
    public interface ILibraryRepository
    {
        Task AddAuthor(Author author);
        Task AddBook(Guid authorId, Guid id);
        Task DeleteAuthor(Author author);
        Task DeleteBook(Author author);
        Task<Author> GetAuthor(Guid id);
        Task<IEnumerable<Author>> GetAuthors();
        Task<IEnumerable<Author>> GetAuthors(IEnumerable<Guid> ids);
        Task<Book> GetBook(Guid authorId, Guid id);
        Task<IEnumerable<Book>> GetBooks(Guid authorId);
        Task<bool> Save();
        Task<bool> AuthorExists(Guid id);
        Task UpdateAuthor(Author author);
        Task UpdateBook(Book book);
    }
}