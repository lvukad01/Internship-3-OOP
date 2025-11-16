using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Internship_3_OOP
{
    internal class Crew
    {
        public Guid Id { get; private set; }
        public CrewMember Pilot { get; private set; }
        public CrewMember Copilot { get; private set; }
        public List<CrewMember> Attendants { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        public Crew(CrewMember pilot,CrewMember copilot,List<CrewMember> attendants )
        {
            if(attendants.Count!=2)
                Console.WriteLine("Moraju biti 2 stjuarda/stjuardese");
            Id = Guid.NewGuid();
            Pilot = pilot;
            Copilot = copilot;
            Attendants = attendants;
        }

        public void Update()
        {
            Updated = DateTime.Now;
        }


    }
}
