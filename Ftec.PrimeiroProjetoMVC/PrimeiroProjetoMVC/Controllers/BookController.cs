using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrimeiroProjetoMVC.Database;
using PrimeiroProjetoMVC.InputModel;
using PrimeiroProjetoMVC.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PrimeiroProjetoMVC.Controllers {
    public class BookController : Controller {

        private MemoryDatabase _memoryDatabase;

        public BookController(IHttpContextAccessor httpContextAccessor) {
            _memoryDatabase = new MemoryDatabase(httpContextAccessor);
        }

        public IActionResult Index() {
            ViewBag.Books = _memoryDatabase.GetBooks();
            return View();
        }

        public IActionResult CreateNew() {
            return View("Form");
        }

        public IActionResult Edit(Guid id) {
            var book = _memoryDatabase.GetBooks().SingleOrDefault(p => p.ID.Equals(id));
            ViewBag.Book = book;

            return View("Form");
        }

        public IActionResult Search(string search) {
            if (search == null || search.Equals(String.Empty))
                return Redirect("/Book");
            else {
                search = search.ToLower();
                ViewBag.Books = _memoryDatabase.GetBooks().Where(p => p.Name.ToLower().Contains(search)
                                                                 || p.Author.ToLower().Contains(search)
                                                                 || p.Description.ToLower().Contains(search))
                                                                 .ToList();
                ViewBag.SearchText = search;
            }

            return View("Index");
        }

        public IActionResult Delete(Guid id) {
            var books = _memoryDatabase.GetBooks();
            var book = books.SingleOrDefault(p => p.ID.Equals(id));
            books.Remove(book);
            _memoryDatabase.SetBooks(books);

            return Redirect("/Book");
        }

        [HttpPost]
        public IActionResult Add(BookInputModel input) {
            var books = _memoryDatabase.GetBooks();
            books.Add(Book.Create(input.Name, input.Author, input.Description));
            _memoryDatabase.SetBooks(books);

            return Redirect("/Book");
        }

        [HttpPost]
        public IActionResult Update(BookInputModel input) {

            ICollection<Book> books = new List<Book>();

            foreach (var item in _memoryDatabase.GetBooks()) {
                if (item.ID == input.ID)
                    books.Add(Book.Create(input.Name, input.Author, input.Description, input.ID));
                else
                    books.Add(item);
            }

            _memoryDatabase.SetBooks(books);

            return Redirect("/Book");
        }
    }
}
