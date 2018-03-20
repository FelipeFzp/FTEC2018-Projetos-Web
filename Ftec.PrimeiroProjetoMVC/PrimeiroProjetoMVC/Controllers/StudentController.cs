using Microsoft.AspNetCore.Mvc;
using PrimeiroProjetoMVC.Database;
using PrimeiroProjetoMVC.InputModel;
using PrimeiroProjetoMVC.Models;
using System.Linq;

namespace PrimeiroProjetoMVC.Controllers
{
    [Route("[controller]")]
    public class StudentController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            ViewData["students"] = MemoryDatabase.Students;
            return View();
        }

        [HttpGet("Search/{search}")]
        public IActionResult Search(string search)
        {
            ViewData["students"] = MemoryDatabase.Students.Where(p => p.Name.StartsWith(search));

            return RedirectToAction("Index", "Student");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var student = MemoryDatabase.Students.SingleOrDefault(p => p.ID.Equals(id));
            MemoryDatabase.Students.Remove(student);

            ViewData["students"] = MemoryDatabase.Students;

            return RedirectToAction("Index", "Student");
        }

        [HttpPost]
        public IActionResult Add([FromBody]StudentInputModel input)
        {
            MemoryDatabase.Students.Add(Student.Create(input.Age, input.Name));

            ViewData["students"] = MemoryDatabase.Students;

            return RedirectToAction("Index", "Student");
        }
    }
}
