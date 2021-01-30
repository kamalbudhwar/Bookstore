using BookStore.Models;
using BookStore.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;

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
        public async Task<ViewResult> GetAllBooks()
        {
            Title = "Get All Books";
            var data =await _bookRepository.GetAllBooks();
            return View(data);
        }
        public async Task<ViewResult> GetBook(int id)
        {
            Title = "Get Book";
            //dynamic data = new ExpandoObject();
            //data.book = _bookRepository.GetBook(id);
            //data.Name = "Kamal";
            var book = await _bookRepository.GetBook(id);
            return View(book);
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
        public async Task<IActionResult> AddNewBook(BookModel bookModel)
        {
            if (ModelState.IsValid)
            {
                int id = await _bookRepository.AddNewBook(bookModel);
                if (id > 0)
                {
                    return RedirectToAction(nameof(AddNewBook), new { isSuccess = true, bookId = id });
                }
            }
            ModelState.AddModelError("", "Custom msg from model");
            return View();
        }
        public ViewResult Motivation()
        {
            Title = "Motivation";
            return View();
        }
    }
}
