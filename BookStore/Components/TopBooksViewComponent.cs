﻿using BookStore.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Components
{
    public class TopBooksViewComponent:ViewComponent
    {
        private readonly IBookRepository _bookRepository;

        public TopBooksViewComponent(IBookRepository bookRepository)
        {
            this._bookRepository = bookRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync(int count)
        {
            var book = await _bookRepository.GetTopBooksAsync(count);
            return View(book);
        }
    }
}