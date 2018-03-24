using System;

namespace PrimeiroProjetoMVC.Models
{
    public class Book
    {
        public Guid ID { get; protected set; }
        public string Name { get; protected set; }
        public string Author { get; protected set; }
        public string Description { get; protected set; }

        public static Book Create(string name, string author, string description)
        {
            var book = new Book();

            book.ID = Guid.NewGuid();
            book.Name = name;
            book.Author = author;
            book.Description = description;

            return book;
        }

        public static Book Create(string name, string author, string description, Guid id) {
            var book = new Book();

            book.ID = id;
            book.Name = name;
            book.Author = author;
            book.Description = description;

            return book;
        }
    }
}