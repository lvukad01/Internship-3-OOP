using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Internship_3_OOP
{
    internal class Crew
    {
        public Guid Id { get; private set; }
        public static int counter = 1;
        public string Name {  get; set; }
        public int DisplayId { get; private set; }
        public CrewMember Pilot { get; private set; }
        public CrewMember Copilot { get; private set; }
        public List<CrewMember> Attendants { get; private set; }
        public DateTime Created { get; private set; }
        public DateTime Updated { get; private set; }

        public Crew(string name,CrewMember pilot,CrewMember copilot,List<CrewMember> attendants )
        {
            if(attendants.Count!=2)
            {
                Console.WriteLine("Moraju biti 2 stjuarda/stjuardese");
            }
            DisplayId = counter;
            counter++;
            Name = name;
            Id = Guid.NewGuid();
            Pilot = pilot;
            Copilot = copilot;
            Attendants = attendants;
        }
        public void print()
        {
        }
        public void Update()
        {
            Updated = DateTime.Now;
        }


    }
}
