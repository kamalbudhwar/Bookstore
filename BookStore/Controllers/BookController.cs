﻿using BookStore.Models;
using BookStore.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
        //public List<BookModel> SearchBooks(String bookName, String authorName)
        //{
        //   return null;
        //}

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
                
                if (bookModel.GalleryFiles != null)
                {
                     String folder = "books/gallery/";
                    bookModel.Gallery = new List<GalleryModel>();
                    foreach (var file in bookModel.GalleryFiles)
                    {
                        var gallery = new GalleryModel()
                        {
                            Name = file.FileName,
                            Url = await UploadImage(folder, file)
                        };
                        bookModel.Gallery.Add(gallery);

                    }
                }
                if (bookModel.CoverPhoto != null)
                {
                    String folder = "books/cover/";
                    bookModel.CoverImageUrl = await UploadImage(folder, bookModel.CoverPhoto);
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

        private async Task<String> UploadImage(String folderPath,IFormFile file)
        {
            folderPath += Guid.NewGuid().ToString()+"-"+ file.FileName;
            string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderPath);
            try
            {
                await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                throw;
            }
            return "/" + folderPath;
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
