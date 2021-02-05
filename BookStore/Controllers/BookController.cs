using BookStore.Models;
using BookStore.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    public class BookController : Controller
    {
        [ViewData]
        public String Title { get; set; }

        private readonly BookRepository _bookRepository = null;
        private readonly LanguageRepository _languageRepository = null;
        private readonly IWebHostEnvironment _webHostEnvironment;
       public BookController(BookRepository bookRepository,LanguageRepository languageRepository,IWebHostEnvironment webHostEnvironment)
        {
            _bookRepository = bookRepository;
            _languageRepository = languageRepository;
            _webHostEnvironment = webHostEnvironment;
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
            var book = await _bookRepository.GetBook(id);
            return View(book);
        }
        public List<BookModel> SearchBooks(String bookName, String authorName)
        {
           return null;

        }

        public async Task<ViewResult> AddNewBook(bool isSuccess = false, int bookId = 0)
        {
            var model = new BookModel();
            ViewBag.languages = new SelectList(await _languageRepository.GetLanguages(), "Id", "Name");
            ViewBag.isSuccess = isSuccess;
            ViewBag.bookId = bookId;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddNewBook(BookModel bookModel)
        {
            if (ModelState.IsValid)
            {
                if (bookModel.CoverPhoto != null)
                {
                    string folder = "books/cover/";
                    folder += Guid.NewGuid().ToString()+bookModel.CoverPhoto.FileName;
                    bookModel.CoverImageUrl = "/"+folder;
                    string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath,folder);
                    await bookModel.CoverPhoto.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                }
                int id = await _bookRepository.AddNewBook(bookModel);
                if (id > 0)
                {
                    return RedirectToAction(nameof(AddNewBook), new { isSuccess = true, bookId = id });
                }
            }
            ModelState.AddModelError("", "Custom msg from model");
             ViewBag.languages =new SelectList(await _languageRepository.GetLanguages(),"Id","Name");
            return View();
        }

        private static List<LanguageModel> GetLanguages()
        {
            return null;
            
        }
        public ViewResult Motivation()
        {
            Title = "Motivation";
            return View();
        }
    }
}
