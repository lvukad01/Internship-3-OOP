using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internship_3_OOP
{
    public abstract class Person
    {
      
        public Guid Id { get; private set; }
        public string Name { get; set; }
        public string LastName {  get; set; }
        public string Email {  get; set; }
        public string Password {  get; set; }
        public DateOnly Birthday { get; set; }
        public Gender Gender { get; set; }
        public  DateTime Created {  get; private  set; }
        public  DateTime Updated { get;private set; }

        public Person(string name, string last_name, string email, string password, DateOnly birthday, Gender gender)
        {
            Id =  Guid.NewGuid();
            Name = name;
            LastName = last_name;
            Email = email;
            Password = password;
            Birthday = birthday;
            Gender = gender;
            Created = DateTime.Now;
            Updated = DateTime.Now;
        }
        public abstract void PrintInfo();
        public void Update()
        {
            Updated = DateTime.Now;
        }

    }
}
