using BookStore.Models;
using BookStore.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Dynamic;

namespace BookStore.Controllers
{
    public class BookController : Controller
    {
        [ViewData]
        public String Title { get; set; }

        private readonly BookRepository _bookRepository = null;
        public BookController(BookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        public ViewResult GetAllBooks()
        {
            Title = "Get All Books";
            var data = _bookRepository.GetAllBooks();
            return View(data);
        }
        public ViewResult GetBook(int id)
        {
            Title = "Get Book";
            dynamic data = new ExpandoObject();
            data.book = _bookRepository.GetBook(id);
            data.Name = "Kamal";
            return View(data);
        }
        public List<BookModel> SearchBooks(String bookName, String authorName)
        {
            return _bookRepository.SearchBooks(bookName, authorName);

        }

        public ViewResult AddNewBook(bool isSuccess = false,int bookId=0)
        {
            ViewBag.isSuccess = isSuccess;
            ViewBag.bookId = bookId;
            return View();
        }
        [HttpPost]
        public IActionResult AddNewBook(BookModel bookModel)
        {
            int id = _bookRepository.AddNewBook(bookModel);
            if (id > 0)
            {
                return RedirectToAction(nameof(AddNewBook), new { isSuccess = true ,bookId=id});
            }
            return View();
        }
        public ViewResult Motivation()
        {
            Title = "Motivation";
            return View();
        }
    }
}
