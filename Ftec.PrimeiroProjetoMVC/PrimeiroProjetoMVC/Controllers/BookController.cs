using Microsoft.AspNetCore.Mvc;
using PrimeiroProjetoMVC.Database;
using PrimeiroProjetoMVC.InputModel;
using PrimeiroProjetoMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PrimeiroProjetoMVC.Controllers
{
    public class BookController : Controller
    {
        private List<Book> _books;

        public BookController() {
            var list = new List<Book>();
            list.AddRange(MemoryDatabase.Books);
            _books = list;
        }

        public IActionResult Index()
        {
            ViewData["books"] = _books;
            return View();
        }

        public IActionResult Form() {
            return View();
        }

        public IActionResult Search(string search)
        {
            _books = MemoryDatabase.Books.Where(p => p.Name.StartsWith(search)).ToList();

            return Redirect("/Book");
        }

        public IActionResult Delete(Guid id)
        {
            var book = MemoryDatabase.Books.SingleOrDefault(p => p.ID.Equals(id));
            MemoryDatabase.Books.Remove(book);

            ViewData["books"] = MemoryDatabase.Books;

            return Redirect("/Book");
        }

        public IActionResult Add(BookInputModel input)
        {
            MemoryDatabase.Books.Add(Book.Create(input.Name, input.Author, input.Description));

            ViewData["books"] = MemoryDatabase.Books;

            return Redirect("/Book");
        }
    }
}
