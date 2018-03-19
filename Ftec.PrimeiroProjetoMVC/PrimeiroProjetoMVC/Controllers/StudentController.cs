using Microsoft.AspNetCore.Mvc;
using PrimeiroProjetoMVC.Database;
using PrimeiroProjetoMVC.Models;
using System.Linq;

namespace PrimeiroProjetoMVC.Controllers
{
    [Route("api/[controller]")]
    public class StudentController : Controller
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            ViewData["students"] = MemoryDatabase.Students;
            return View();
        }

        [HttpGet("Search/{search}")]
        public IActionResult Search(string search)
        {
            ViewData["students"] = MemoryDatabase.Students.Where(p => p.Name.StartsWith(search));
            return View();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var student = MemoryDatabase.Students.SingleOrDefault(p => p.ID.Equals(id));
            MemoryDatabase.Students.Remove(student);

            ViewData["students"] = MemoryDatabase.Students;
            return View();
        }

        [HttpPost]
        public IActionResult Add(string name, string age)
        {
            MemoryDatabase.Students.Add(Student.Create(age, name));

            ViewData["students"] = MemoryDatabase.Students;
            return View();
        }
    }
}
