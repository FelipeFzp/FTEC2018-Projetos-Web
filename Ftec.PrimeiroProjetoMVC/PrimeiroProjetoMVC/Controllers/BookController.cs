using Microsoft.AspNetCore.Mvc;
using PrimeiroProjetoMVC.Database;
using PrimeiroProjetoMVC.InputModel;
using PrimeiroProjetoMVC.Models;
using System;
using System.Linq;

namespace PrimeiroProjetoMVC.Controllers
{
    public class BookController : Controller
    {
        public IActionResult Index()
        {
            ViewData["books"] = MemoryDatabase.Books;
            return View();
        }

        public IActionResult Form() {
            return View();
        }

        public IActionResult Search(string search)
        {
            ViewData["books"] = MemoryDatabase.Books.Where(p => p.Name.StartsWith(search));

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
