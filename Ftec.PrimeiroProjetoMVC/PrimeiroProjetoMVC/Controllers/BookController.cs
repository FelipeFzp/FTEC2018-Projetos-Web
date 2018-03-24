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
        public IActionResult Index() {
            ViewBag.Books = MemoryDatabase.Books;
            return View();
        }

        public IActionResult CreateNew() {
            return View("Form");
        }

        public IActionResult Edit(Guid id) {
            var book = MemoryDatabase.Books.SingleOrDefault(p => p.ID.Equals(id));
            ViewBag.Book = book;

            return View("Form");
        }

        public IActionResult Search(string search) {
            if (search == null || search.Equals(String.Empty))
                return Redirect("/Book");
            else {
                search = search.ToLower();
                ViewBag.Books = MemoryDatabase.Books.Where(p => p.Name.ToLower().Contains(search)
                                                                 || p.Author.ToLower().Contains(search)
                                                                 || p.Description.ToLower().Contains(search))
                                                                 .ToList();
                ViewBag.SearchText = search;
            }

            return View("Index");
        }

        public IActionResult Delete(Guid id) {
            var book = MemoryDatabase.Books.SingleOrDefault(p => p.ID.Equals(id));
            MemoryDatabase.Books.Remove(book);

            return Redirect("/Book");
        }

        [HttpPost]
        public IActionResult Add(BookInputModel input) {
            MemoryDatabase.Books.Add(Book.Create(input.Name, input.Author, input.Description));

            return Redirect("/Book");
        }

        [HttpPost]
        public IActionResult Update(BookInputModel input) {

            ICollection<Book> books = new List<Book>();

            foreach (var item in MemoryDatabase.Books) {
                if (item.ID == input.ID)
                    books.Add(Book.Create(input.Name, input.Author, input.Description, input.ID));
                else
                    books.Add(item);
            }

            MemoryDatabase.Books = books;

            return Redirect("/Book");
        }
    }
}
