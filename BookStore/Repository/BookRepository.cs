using BookStore.Data;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookStoreContext _context = null;
        private readonly IConfiguration _configuration;
        public BookRepository(BookStoreContext context,IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public async Task<int> AddNewBook(BookModel model)
        {
            var newBook = new Books()
            {
                Author = model.Author,
                Title = model.Title,
                Description = model.Description,
                Category = model.Category,
                TotalPages = model.TotalPages != null ? model.TotalPages.Value : 0,
                LanguageId = model.LanguageId,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow,
                CoverImageUrl = model.CoverImageUrl,
                BookPdfUrl = model.BookPdfUrl
            };

            newBook.BookGalleries = new List<BookGallery>();
            foreach (var file in model.Gallery)
            {
                newBook.BookGalleries.Add(new BookGallery()
                {
                    Name = file.Name,
                    Url = file.Url
                });
            }
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
                        CoverImageUrl = book.CoverImageUrl,
                        Language = await _context.Language.Where(x => x.Id == book.LanguageId).Select(lan => lan.Name).FirstOrDefaultAsync(),
                        Id = book.Id,
                        BookPdfUrl = book.BookPdfUrl
                    }); ; ;
                }
            }
            return modelbooklist;

        }

        public async Task<List<BookModel>> GetTopBooksAsync(int count)
        {
            var modelbooklist = new List<BookModel>();
            var allbooks = await _context.Books.Take(count).ToListAsync();
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
                        CoverImageUrl = book.CoverImageUrl,
                        Language = await _context.Language.Where(x => x.Id == book.LanguageId).Select(lan => lan.Name).FirstOrDefaultAsync(),
                        Id = book.Id,
                        BookPdfUrl = book.BookPdfUrl
                    });
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
                CoverImageUrl = book.CoverImageUrl,
                Language = book.Language.Name,
                Id = book.Id,
                BookPdfUrl = book.BookPdfUrl,
                Gallery = book.BookGalleries.Select(g => new GalleryModel()
                {
                    Name = g.Name,
                    Url = g.Url,
                    Id = g.Id
                }).ToList()
            }).FirstOrDefaultAsync();
        }
        //public List<BookModel> SearchBooks(String title,String author)
        // {
        //    return null;
        //}
        public String GetAppName()
        {
            return _configuration["AppName"];
        }

    }
}
