using Microsoft.AspNetCore.Mvc;
using PrimeiroProjetoMVC.Database;
using PrimeiroProjetoMVC.Models;
using System.Linq;

namespace PrimeiroProjetoMVC.Controllers
{
    [Route("api/[controller]")]
    public class EmployeeController: Controller
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            ViewData["employees"] = MemoryDatabase.Employees;
            return View();
        }

        [HttpGet("Search/{search}")]
        public IActionResult Search(string search)
        {
            ViewData["employees"] = MemoryDatabase.Employees.Where(p => p.Name.StartsWith(search));
            return View();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var student = MemoryDatabase.Employees.SingleOrDefault(p => p.ID.Equals(id));
            MemoryDatabase.Employees.Remove(student);

            ViewData["employees"] = MemoryDatabase.Employees;
            return View();
        }

        [HttpPost]
        public IActionResult Add(string name, string age, string professionalPosition)
        {
            MemoryDatabase.Employees.Add(Employee.Create(name, age, professionalPosition));

            ViewData["employees"] = MemoryDatabase.Students;
            return View();
        }
    }
}
