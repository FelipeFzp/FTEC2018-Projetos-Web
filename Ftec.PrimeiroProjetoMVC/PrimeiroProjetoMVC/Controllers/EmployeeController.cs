using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrimeiroProjetoMVC.Database;
using PrimeiroProjetoMVC.InputModel;
using PrimeiroProjetoMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PrimeiroProjetoMVC.Controllers {
    public class EmployeeController : Controller {

        private MemoryDatabase _memoryDatabase;

        public EmployeeController(IHttpContextAccessor httpContextAccessor) {
            _memoryDatabase = new MemoryDatabase(httpContextAccessor);
        }

        public IActionResult Index() {
            ViewBag.Employees = _memoryDatabase.GetEmployees();
            return View();
        }

        public IActionResult CreateNew() {
            return View("Form");
        }

        public IActionResult Edit(Guid id) {
            var employee = _memoryDatabase.GetEmployees().SingleOrDefault(p => p.ID.Equals(id));
            ViewBag.Employee = employee;

            return View("Form");
        }

        public IActionResult Search(string search) {
            if (search == null || search.Equals(String.Empty))
                return Redirect("/Employee");
            else {
                ViewBag.Employees = _memoryDatabase.GetEmployees()
                                                .Where(p => p.Name.ToLower().Contains(search.ToLower()) ||
                                                            p.Age.ToString().Contains(search) ||
                                                            p.ProfessionalPosition.ToLower().Contains(search.ToLower())).ToList();
                ViewBag.SearchText = search;
            }

            return View("Index");
        }

        public IActionResult Delete(Guid id) {
            var employees = _memoryDatabase.GetEmployees();
            var employee = employees.SingleOrDefault(p => p.ID.Equals(id));
            employees.Remove(employee);
            _memoryDatabase.SetEmployees(employees);

            return Redirect("/Employee");
        }

        [HttpPost]
        public IActionResult Add(EmployeeInputModel input) {
            var employees = _memoryDatabase.GetEmployees();
            employees.Add(Employee.Create(input.Name, input.Age, input.ProfessionalPosition));
            _memoryDatabase.SetEmployees(employees);

            return Redirect("/Employee");
        }

        [HttpPost]
        public IActionResult Update(EmployeeInputModel input) {

            ICollection<Employee> employees = new List<Employee>();

            foreach (var item in _memoryDatabase.GetEmployees()) {
                if (item.ID == input.ID)
                    employees.Add(Employee.Create(input.Name, input.Age, input.ProfessionalPosition));
                else
                    employees.Add(item);
            }

            _memoryDatabase.SetEmployees(employees);

            return Redirect("/Employee");
        }
    }
}