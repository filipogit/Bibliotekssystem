using Bibliotekssystem.Data;
using Bibliotekssystem.Models;
using Microsoft.EntityFrameworkCore;

namespace Bibliotekssystem.Repositories
{
    public class BookRepository
    {
        private readonly LibraryContext _context;

        public BookRepository(LibraryContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Book>> GetAllAsync()
        {
            return await _context.Books.ToListAsync();
        }

        public async Task<List<Book>> SearchAsync(string searchTerm)
        {
            var all = await _context.Books.ToListAsync();
            return all.Where(b => b.Matches(searchTerm)).ToList();
        }
    }
}
