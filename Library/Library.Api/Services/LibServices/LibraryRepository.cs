using Library.Api.Data;
using Library.Api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Api.Services.LibServices
{
    public class LibraryRepository : ILibraryRepository
    {
        private readonly LibraryContext context;

        public LibraryRepository(LibraryContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Author>> GetAuthors(IEnumerable<Guid> ids)
        {
            return await context.Authors
                .Where(a => ids.Contains(a.Id))
                .OrderBy(a => a.FirstName).OrderBy(a => a.LastName)
                .ToListAsync();
        }

        public async Task<IEnumerable<Author>> GetAuthors()
        {
            return await context.Authors
                .OrderBy(a => a.FirstName)
                .ThenBy(a => a.LastName).ToListAsync();
        }

        public async Task<Author> GetAuthor(Guid id)
        {
            return await context.Authors
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task AddAuthor(Author author)
        {
            author.Id = Guid.NewGuid();
            context.Authors.Add(author);

            if (author.Books.Any())
            {
                foreach (var book in author.Books)
                    book.Id = Guid.NewGuid();
            }

            await Task.CompletedTask;
        }

        public async Task UpdateAuthor(Author author)
        {
            await Task.FromException(new NotImplementedException());
        }

        public async Task DeleteAuthor(Author author)
        {
            context.Authors.Remove(author);
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<Book>> GetBooks(Guid authorId)
        {
            return await context.Books
                .Where(b => b.AuthorId == authorId).ToListAsync();
        }

        public async Task<Book> GetBook(Guid authorId, Guid id)
        {
            //return await context.Books.Where(b => b.AuthorId == authorId && b.Id == id).FirstOrDefaultAsync();
            return await context.Books.SingleOrDefaultAsync(b => b.AuthorId == authorId && b.Id == id);
        }

        public async Task AddBook(Guid authorId, Guid id)
        {
            await Task.Yield();
            throw new NotImplementedException();
            //await Task.FromException(new NotImplementedException());
        }

        public async Task UpdateBook(Book book)
        {
            await Task.FromException(new NotImplementedException());
        }

        public async Task DeleteBook(Author author)
        {
            context.Authors.Remove(author);
            await Task.CompletedTask;
        }

        public async Task<bool> Save()
        {
            return await context.SaveChangesAsync() >= 0;
        }
    }
}
