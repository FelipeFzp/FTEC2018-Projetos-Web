using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using PrimeiroProjetoMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PrimeiroProjetoMVC.Database {
    public class MemoryDatabase {

        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        public MemoryDatabase(IHttpContextAccessor httpContextAccessor) {
            _httpContextAccessor = httpContextAccessor;
        }

        public void SetBooks(ICollection<Book> books) {
            _session.SetString("books", JsonConvert.SerializeObject(books));
        }

        public ICollection<Book> GetBooks() {
            var result = _session.GetString("books");
            var books = result != null ? JsonConvert.DeserializeObject<ICollection<BookJson>>(result) : new List<BookJson>();

            return books.Select(e => Book.Create(e.Name, e.Author, e.Description, e.Id)).ToList();
        }

        public void SetEmployees(ICollection<Employee> employees) {
            _session.SetString("employees", JsonConvert.SerializeObject(employees));
        }

        public ICollection<Employee> GetEmployees() {
            var result = _session.GetString("employees");
            var employees = result != null ? JsonConvert.DeserializeObject<ICollection<EmployeeJson>>(result) : new List<EmployeeJson>();

            return employees.Select(e => Employee.Create(e.Name, e.Age, e.ProfessionalPosition, e.Id)).ToList();
        }

        public void SetStudents(ICollection<Student> students) {
            _session.SetString("students", JsonConvert.SerializeObject(students));
        }

        public ICollection<Student> GetStudents() {
            var result = _session.GetString("students");
            var students = result != null ? JsonConvert.DeserializeObject<ICollection<EmployeeJson>>(result) : new List<EmployeeJson>();

            return students.Select(e => Student.Create(e.Age, e.Name, e.Id)).ToList();
        }

    }

    class BookJson {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
    }

    class StudentJson {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Age { get; set; }
    }

    class EmployeeJson {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Age { get; set; }
        public string ProfessionalPosition { get; set; }
    }
}
