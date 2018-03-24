using Microsoft.AspNetCore.Mvc;
using PrimeiroProjetoMVC.Database;
using PrimeiroProjetoMVC.InputModel;
using PrimeiroProjetoMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PrimeiroProjetoMVC.Controllers {
    public class StudentController : Controller {
        public IActionResult Index() {
            ViewData["students"] = MemoryDatabase.Students;
            return View();
        }

        public IActionResult CreateNew() {
            return View("Form");
        }

        public IActionResult Edit(Guid id) {
            var student = MemoryDatabase.Students.SingleOrDefault(p => p.ID.Equals(id));
            ViewBag.Student = student;

            return View("Form");
        }

        public IActionResult Search(string search) {
            if (search == null || search.Equals(String.Empty))
                return Redirect("/Student");
            else {
                search = search.ToLower();
                ViewData["students"] = MemoryDatabase.Students.Where(p => p.Name.ToLower().Contains(search)
                                                                    || p.Age.ToLower().Contains(search))
                                                                 .ToList();
                ViewData["searchText"] = search;
            }

            return View("Index");
        }

        public IActionResult Delete(Guid id) {
            var student = MemoryDatabase.Students.SingleOrDefault(p => p.ID.Equals(id));
            MemoryDatabase.Students.Remove(student);

            return Redirect("/Student");
        }

        [HttpPost]
        public IActionResult Add(StudentInputModel input) {
            MemoryDatabase.Students.Add(Student.Create(input.Age, input.Name));

            return Redirect("/Student");
        }

        [HttpPost]
        public IActionResult Update(StudentInputModel input) {

            ICollection<Student> students = new List<Student>();

            foreach (var item in MemoryDatabase.Students) {
                if (item.ID == input.ID)
                    students.Add(Student.Create(input.Age, input.Name, input.ID));
                else
                    students.Add(item);
            }

            MemoryDatabase.Students = students;

            return Redirect("/Student");
        }
    }
}
