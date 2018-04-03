using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrimeiroProjetoMVC.Database;
using PrimeiroProjetoMVC.InputModel;
using PrimeiroProjetoMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PrimeiroProjetoMVC.Controllers {
    public class StudentController : Controller {

        private MemoryDatabase _memoryDatabase;

        public StudentController(IHttpContextAccessor httpContextAccessor) {
            _memoryDatabase = new MemoryDatabase(httpContextAccessor);
        }

        public IActionResult Index() {
            ViewData["students"] = _memoryDatabase.GetStudents();
            return View();
        }

        public IActionResult CreateNew() {
            return View("Form");
        }

        public IActionResult Edit(Guid id) {
            var student = _memoryDatabase.GetStudents().SingleOrDefault(p => p.ID.Equals(id));
            ViewBag.Student = student;

            return View("Form");
        }

        public IActionResult Search(string search) {
            if (search == null || search.Equals(String.Empty))
                return Redirect("/Student");
            else {
                search = search.ToLower();
                ViewData["students"] = _memoryDatabase.GetStudents().Where(p => p.Name.ToLower().Contains(search)
                                                                    || p.Age.ToLower().Contains(search))
                                                                 .ToList();
                ViewData["searchText"] = search;
            }

            return View("Index");
        }

        public IActionResult Delete(Guid id) {
            var students = _memoryDatabase.GetStudents();
            var student = students.SingleOrDefault(p => p.ID.Equals(id));
            students.Remove(student);
            _memoryDatabase.SetStudents(students);

            return Redirect("/Student");
        }

        [HttpPost]
        public IActionResult Add(StudentInputModel input) {
            var students = _memoryDatabase.GetStudents();
            students.Add(Student.Create(input.Age, input.Name));
            _memoryDatabase.SetStudents(students);

            return Redirect("/Student");
        }

        [HttpPost]
        public IActionResult Update(StudentInputModel input) {

            ICollection<Student> students = new List<Student>();

            foreach (var item in _memoryDatabase.GetStudents()) {
                if (item.ID == input.ID)
                    students.Add(Student.Create(input.Age, input.Name, input.ID));
                else
                    students.Add(item);
            }

            _memoryDatabase.SetStudents(students);

            return Redirect("/Student");
        }
    }
}
