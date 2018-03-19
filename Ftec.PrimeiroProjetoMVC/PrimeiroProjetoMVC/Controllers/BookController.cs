using Microsoft.AspNetCore.Mvc;
using PrimeiroProjetoMVC.Models;
using System.Collections.Generic;
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
            ViewData["books"] = _books;
            return View();
        }

        [HttpGet("Search/{search}")]
        public IActionResult Search(string search)
        {
            ViewData["books"] = _books.Where(p => p.Name.StartsWith(search));
            return View();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var bookToRemove =_books.SingleOrDefault(p => p.ID.Equals(id));
            _books.Remove(bookToRemove);

            ViewData["books"] = _books;
            return View();
        }

        [HttpPost()]
        public IActionResult Add(string name, string author, string description)
        {
            _books.Add(Book.Create(name, author, description));

            ViewData["books"] = _books;
            return View();
        }
    }
}
