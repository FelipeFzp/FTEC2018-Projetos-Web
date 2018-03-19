using Microsoft.AspNetCore.Mvc;
using PrimeiroProjetoMVC.Database;
using PrimeiroProjetoMVC.Models;
using System.Linq;

namespace PrimeiroProjetoMVC.Controllers
{
    public class BookController : Controller
    {
        [HttpGet]
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
        public IActionResult Add([FromBody]string name, [FromBody]string author, [FromBody]string description)
        {
            MemoryDatabase.Books.Add(Book.Create(name, author, description));

            ViewData["books"] = MemoryDatabase.Books;
            return View();
        }
    }
}
