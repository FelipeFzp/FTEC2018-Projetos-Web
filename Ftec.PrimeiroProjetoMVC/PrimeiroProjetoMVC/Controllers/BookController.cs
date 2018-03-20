using Microsoft.AspNetCore.Mvc;
using PrimeiroProjetoMVC.Database;
using PrimeiroProjetoMVC.InputModel;
using PrimeiroProjetoMVC.Models;
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

        [HttpGet("Search/{search}")]
        public IActionResult Search(string search)
        {
            ViewData["books"] = MemoryDatabase.Books.Where(p => p.Name.StartsWith(search));

            return RedirectToAction("Index", "Book");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var book = MemoryDatabase.Books.SingleOrDefault(p => p.ID.Equals(id));
            MemoryDatabase.Books.Remove(book);

            ViewData["books"] = MemoryDatabase.Books;

            return RedirectToAction("Index", "Book");
        }

        [HttpPost]
        public IActionResult Add([FromBody]BookInputModel input)
        {
            MemoryDatabase.Books.Add(Book.Create(input.Name, input.Author, input.Description));

            ViewData["books"] = MemoryDatabase.Books;

            return RedirectToAction("Index", "Book");
        }
    }
}
