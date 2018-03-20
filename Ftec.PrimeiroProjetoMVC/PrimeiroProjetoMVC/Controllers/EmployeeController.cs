using Microsoft.AspNetCore.Mvc;
using PrimeiroProjetoMVC.Database;
using PrimeiroProjetoMVC.InputModel;
using PrimeiroProjetoMVC.Models;
using System.Linq;

namespace PrimeiroProjetoMVC.Controllers
{
    [Route("api/[controller]")]
    public class EmployeeController: Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            ViewData["employees"] = MemoryDatabase.Employees;
            return View();
        }

        [HttpGet("Search/{search}")]
        public IActionResult Search(string search)
        {
            ViewData["employees"] = MemoryDatabase.Employees.Where(p => p.Name.StartsWith(search));

            return RedirectToAction("Index", "Employee");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var student = MemoryDatabase.Employees.SingleOrDefault(p => p.ID.Equals(id));
            MemoryDatabase.Employees.Remove(student);

            ViewData["employees"] = MemoryDatabase.Employees;

            return RedirectToAction("Index", "Employee");
        }

        [HttpPost]
        public IActionResult Add([FromBody] EmployeeInputModel input)
        {
            MemoryDatabase.Employees.Add(Employee.Create(input.Name, input.Age, input.ProfessionalPosition));

            ViewData["employees"] = MemoryDatabase.Students;

            return RedirectToAction("Index", "Employee");
        }
    }
}