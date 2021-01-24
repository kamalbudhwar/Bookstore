using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Repository
{
    public class BookRepository
    {
        public List<BookModel> GetAllBooks()
        {
            return DataSouce();
        }
        public BookModel GetBook(int id)
        {
            return DataSouce().Where(x=>x.Id==id).FirstOrDefault();
        }
        public List<BookModel> SearchBooks(String title,String author)
        {
            return DataSouce().Where(x=>x.Title == title || x.Author == author).ToList();
        }
        private List<BookModel> DataSouce()
        {
            return new List<BookModel>(){
                new BookModel(){Id=1,Title="Onec Upon a Time", Author="Simon"},
                new BookModel(){Id=2,Title="MVC", Author="Bajinder"},
                new BookModel(){Id=3,Title="Java", Author="Kamal"},
                new BookModel(){Id=4,Title="C#", Author="Sandhu"},
                new BookModel(){Id=5,Title="React", Author="Nitish"},
                new BookModel(){Id=6,Title="The Times of India", Author="Kamal"}
            };
        }

    }
}
