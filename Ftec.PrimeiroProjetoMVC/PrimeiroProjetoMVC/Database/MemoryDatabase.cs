using PrimeiroProjetoMVC.Models;
using System.Collections.Generic;

namespace PrimeiroProjetoMVC.Database
{
    public static class MemoryDatabase
    {
        public static ICollection<Book> Books = new List<Book>() {
            Book.Create("Bunda Preta", "Marcos", "Lalala alaalala alala ala"),
            Book.Create("Bunda Peluda", "Mariana", "Lalala alaalala alala ala"),
            Book.Create("Bunda Branca", "Mário", "Lalala alaalala alala ala"),
            Book.Create("Bunda Lisa", "Mercedes", "Lalala alaalala alala ala"),
            Book.Create("Bunda Feia", "Mirela", "Lalala alaalala alala ala"),
        };
        public static ICollection<Student> Students = new List<Student>();
        public static ICollection<Employee> Employees = new List<Employee>();
    }
}
