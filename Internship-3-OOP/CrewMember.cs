using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internship_3_OOP
{
    internal class CrewMember:Person
    {
        public CrewPosition Position { get; set; }
        public Guid CrewId { get; set; }

        public CrewMember(string name, string last_name, string email, string password, DateOnly birthday, Gender gender,CrewPosition position) : base(name, last_name, email, password, birthday, gender)
        {
                Position = position;
        }


        public override void PrintInfo()
        {

        }
    }
}
