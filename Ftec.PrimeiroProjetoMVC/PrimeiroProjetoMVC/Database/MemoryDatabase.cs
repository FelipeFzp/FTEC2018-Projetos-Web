using PrimeiroProjetoMVC.Models;
using System.Collections.Generic;

namespace PrimeiroProjetoMVC.Database
{
    public static class MemoryDatabase
    {
        public static ICollection<Book> Books = new List<Book>() {
            Book.Create("Livro Preto", "Marcos", "Desc 1"),
            Book.Create("Livro Azul", "Mariana", "Desc 2"),
            Book.Create("Livro Branco", "Mário", "Desc 3"),
            Book.Create("Livro Amarelo", "Mercedes", "Desc 4"),
            Book.Create("Livro Vermelho", "Mirela", "Desc 5"),
        };
        public static ICollection<Student> Students = new List<Student>();
        public static ICollection<Employee> Employees = new List<Employee>();
    }
}
