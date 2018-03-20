using Microsoft.AspNetCore.Mvc;
using PrimeiroProjetoMVC.Database;
using PrimeiroProjetoMVC.InputModel;
using PrimeiroProjetoMVC.Models;
using System;
using System.Linq;

namespace PrimeiroProjetoMVC.Controllers
{
    public class EmployeeController: Controller
    {
        public IActionResult Index()
        {
            ViewData["employees"] = MemoryDatabase.Employees;
            return View();
        }

        public IActionResult Form()
        {
            return View();
        }

        public IActionResult Search(string search)
        {
            if (search == null || search.Equals(String.Empty))
                return Redirect("/Employee");
            else
            {
                ViewData["employees"] = MemoryDatabase.Employees
                                                .Where(p => p.Name.ToLower().Contains(search.ToLower()) || 
                                                            p.Age.ToString().Contains(search) || 
                                                            p.ProfessionalPosition.ToLower().Contains(search.ToLower())).ToList();
                ViewData["searchText"] = search;                                                                 
            }

            return View("Index");
        }

        public IActionResult Delete(Guid id)
        {
            var student = MemoryDatabase.Employees.SingleOrDefault(p => p.ID.Equals(id));
            MemoryDatabase.Employees.Remove(student);

            ViewData["employees"] = MemoryDatabase.Employees;

            return Redirect("/Employee");
        }

        public IActionResult Add(EmployeeInputModel input)
        {
            MemoryDatabase.Employees.Add(Employee.Create(input.Name, input.Age, input.ProfessionalPosition));

            ViewData["employees"] = MemoryDatabase.Students;

            return Redirect("/Employee");
        }
    }
}