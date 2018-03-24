using Microsoft.AspNetCore.Mvc;
using PrimeiroProjetoMVC.Database;
using PrimeiroProjetoMVC.InputModel;
using PrimeiroProjetoMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PrimeiroProjetoMVC.Controllers {
    public class EmployeeController : Controller {
        public IActionResult Index() {
            ViewBag.Employees = MemoryDatabase.Employees;
            return View();
        }

        public IActionResult CreateNew() {
            return View("Form");
        }

        public IActionResult Edit(Guid id) {
            var employee = MemoryDatabase.Employees.SingleOrDefault(p => p.ID.Equals(id));
            ViewBag.Employee = employee;

            return View("Form");
        }

        public IActionResult Search(string search) {
            if (search == null || search.Equals(String.Empty))
                return Redirect("/Employee");
            else {
                ViewBag.Employees = MemoryDatabase.Employees
                                                .Where(p => p.Name.ToLower().Contains(search.ToLower()) ||
                                                            p.Age.ToString().Contains(search) ||
                                                            p.ProfessionalPosition.ToLower().Contains(search.ToLower())).ToList();
                ViewBag.SearchText = search;
            }

            return View("Index");
        }

        public IActionResult Delete(Guid id) {
            var student = MemoryDatabase.Employees.SingleOrDefault(p => p.ID.Equals(id));
            MemoryDatabase.Employees.Remove(student);

            return Redirect("/Employee");
        }

        [HttpPost]
        public IActionResult Add(EmployeeInputModel input) {
            MemoryDatabase.Employees.Add(Employee.Create(input.Name, input.Age, input.ProfessionalPosition));

            return Redirect("/Employee");
        }

        [HttpPost]
        public IActionResult Update(EmployeeInputModel input) {

            ICollection<Employee> employees = new List<Employee>();

            foreach (var item in MemoryDatabase.Employees) {
                if (item.ID == input.ID)
                    employees.Add(Employee.Create(input.Name, input.Age, input.ProfessionalPosition));
                else
                    employees.Add(item);
            }

            MemoryDatabase.Employees = employees;

            return Redirect("/Employee");
        }
    }
}