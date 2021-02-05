using BookStore.Data;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Repository
{
    public class BookRepository
    {
        private readonly BookStoreContext _context = null;
        public BookRepository(BookStoreContext context)
        {
            _context = context;
        }
        public async Task<int> AddNewBook(BookModel model)
        {
            var newBook = new Books()
            {
                Author = model.Author,
                Title = model.Title,
                Description = model.Description,
                Category = model.Category,
                TotalPages = model.TotalPages!=null? model.TotalPages.Value: 0,
                LanguageId = model.LanguageId,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow,
                CoverImageUrl=model.CoverImageUrl

            };
            await _context.AddAsync(newBook);
            await _context.SaveChangesAsync();
            return newBook.Id;

        }
        public async Task<List<BookModel>> GetAllBooks()
        {
            var modelbooklist = new List<BookModel>();
            var allbooks = await _context.Books.ToListAsync();
            if (allbooks?.Any() == true)
            {
                foreach (var book in allbooks)
                {
                    modelbooklist.Add(new BookModel()
                    {
                        Author = book.Author,
                        Title = book.Title,
                        Description = book.Description,
                        Category = book.Category,
                        TotalPages = book.TotalPages,
                        LanguageId = book.LanguageId,
                        CoverImageUrl=book.CoverImageUrl,
                        Language = await _context.Language.Where(x => x.Id == book.LanguageId).Select(lan => lan.Name).FirstOrDefaultAsync(),
                        Id = book.Id
                    }); ;;
                }
            }
            return modelbooklist;
           
        }
        public async Task<BookModel> GetBook(int id)
        {
            return await _context.Books.Where(x => x.Id == id).Select(book => new BookModel()
            {
                Author = book.Author,
                Title = book.Title,
                Description = book.Description,
                Category = book.Category,
                TotalPages = book.TotalPages,
                LanguageId = book.LanguageId,
                CoverImageUrl=book.CoverImageUrl,
                Language = book.Language.Name,
                Id = book.Id
            }).FirstOrDefaultAsync();
        }
        public List<BookModel> SearchBooks(String title,String author)
         {
            return null;
        }
        

    }
}
