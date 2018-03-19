﻿using Microsoft.AspNetCore.Mvc;
using PrimeiroProjetoMVC.Database;
using PrimeiroProjetoMVC.InputModel;
using PrimeiroProjetoMVC.Models;
using System.Linq;

namespace PrimeiroProjetoMVC.Controllers
{
    public class BookController : Controller
    {
        private List<Book> _books = new List<Book>();

        public BookController() {
            //_books.Add(Book.Create());
        }

        public IActionResult Index()
        {
            ViewData["books"] = MemoryDatabase.Books;
            return View();
        }

        [HttpGet("Search/{search}")]
        public IActionResult Search(string search)
        {
            ViewData["books"] = MemoryDatabase.Books.Where(p => p.Name.StartsWith(search));
            return View();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var book = MemoryDatabase.Books.SingleOrDefault(p => p.ID.Equals(id));
            MemoryDatabase.Books.Remove(book);

            ViewData["books"] = MemoryDatabase.Books;
            return View();
        }

        [HttpPost]
        public IActionResult Add([FromBody]BookInputModel input)
        {
            MemoryDatabase.Books.Add(Book.Create(input.Name, input.Author, input.Description));

            ViewData["books"] = MemoryDatabase.Books;
            return View();
        }
    }
}
