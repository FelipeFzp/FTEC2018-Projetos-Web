using PrimeiroProjetoMVC.Models;
using System.Collections.Generic;

namespace PrimeiroProjetoMVC.Database
{
    public static class MemoryDatabase
    {
        public static ICollection<Book> Books = new List<Book>();
        public static ICollection<Student> Students = new List<Student>();
        public static ICollection<Employee> Employees = new List<Employee>();
    }
}
