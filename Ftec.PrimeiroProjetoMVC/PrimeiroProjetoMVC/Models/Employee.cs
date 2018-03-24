using System;

namespace PrimeiroProjetoMVC.Models
{
    public class Employee
    {
        public Guid ID { get; set; }
        public string Name { get; protected set; }
        public string Age { get; protected set; }
        public string ProfessionalPosition { get; protected set; }

        public static Employee Create(string name, string age, string professionalPosition)
        {
            var employee = new Employee();

            employee.ID = Guid.NewGuid();
            employee.Age = age;
            employee.Name = name;
            employee.ProfessionalPosition = professionalPosition;

            return employee;
        }

        public static Employee Create(string name, string age, string professionalPosition, Guid id) {
            var employee = new Employee();

            employee.ID = id;
            employee.Age = age;
            employee.Name = name;
            employee.ProfessionalPosition = professionalPosition;

            return employee;
        }
    }
}
