using System;

namespace PrimeiroProjetoMVC.Models
{
    public class Student
    {
        public Guid ID { get; set; }
        public string Name { get; protected set; }
        public string Age { get; protected set; }

        public static Student Create(string age, string name)
        {
            var student = new Student();

            student.ID = Guid.NewGuid();
            student.Age = age;
            student.Name = name;

            return student;
        }

        public static Student Create(string age, string name, Guid id) {
            var student = new Student();

            student.ID = id;
            student.Age = age;
            student.Name = name;

            return student;
        }
    }
}
