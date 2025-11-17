using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internship_3_OOP
{
    internal class Passenger : Person
    {
        List<Guid> Flights {  get; set; }

        public Passenger(string name, string last_name, string email, string password, DateOnly birthday, Gender gender) : base(name, last_name, email, password, birthday, gender)
        {
            Flights = new List<Guid>();
        }

        public void AddFlights(Guid flight_guid)
        {
            if(!Flights.Contains(flight_guid))
            {
                Flights.Add(flight_guid);
                Update();
            }
        }
        public override void PrintInfo()
        {

        }
    }
}
