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
                Language = model.Language,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow

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
                        Author= book.Author,
                        Title=book.Title,
                        Description=book.Description,
                        Category=book.Category,
                        TotalPages=book.TotalPages,
                        Language=book.Language,
                        Id=book.Id
                    });
                }
            }
            return modelbooklist;
           
        }
        public async Task<BookModel> GetBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            //return _context.Books.Where(x=>x.Id==id).FirstOrDefaultAsync();
            if (book != null)
            {
                var modelbook = new BookModel(){
                    Author = book.Author,
                Title = book.Title,
                Description = book.Description,
                Category = book.Category,
                TotalPages = book.TotalPages,
                Language = book.Language,
                Id = book.Id
                };
                return modelbook;
            }
            return null;
        }
        public List<BookModel> SearchBooks(String title,String author)
        {
            return DataSouce().Where(x=>x.Title == title || x.Author == author).ToList();
        }
        private  static List<BookModel> DataSouce()
        {
            return new List<BookModel>(){
                new BookModel(){Id=1,Title="Onec Upon a Time", Author="Simon",Description="A story of india before 1947,about people,and Democracy",Category="History",Language="Hindi",TotalPages=1965},
                new BookModel(){Id=2,Title="MVC", Author="Bajinder" ,Description="This will teach you the basic skill of dotnet core MVC application",Category="Web Development",Language="English",TotalPages=165 },
                new BookModel(){Id=3,Title="Java", Author="Kamal",Description="Book to learn coding from beginners to advanced level learners" ,Category="Core Java",Language="English",TotalPages=365},
                new BookModel(){Id=4,Title="C#", Author="Sandhu",Description="Book to learn C# coding from beginners to advanced level learners",Category="Programming",Language="English",TotalPages=1065 },
                new BookModel(){Id=5,Title="Reaction", Author="Nitish", Description="Explains every action has recation in nature",Category="Physics",Language="English",TotalPages=345 },
                new BookModel(){Id=6,Title="The Times of India", Author="Kamal", Description="20th centuary India,people,and politics" ,Category="History",Language="Hindi",TotalPages=765}
            };
        }

    }
}
