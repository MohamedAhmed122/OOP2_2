using System;
using System.Collections.Generic;
using System.Text;

namespace BankLibrary.Models
{
    public class Person
    {
        public string Name { get; set; }

        public string Age { get; set; }
        
        public string InsurranceNumber { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public Person() { }

        public Person(string name, string age, string insurranceNumb, string email, string phone)
        {
            this.InsurranceNumber =insurranceNumb;
            this.Name =name;
            this.Email = email;
            this.Phone = phone;
            this.Age = age ;
        }

    }
}
