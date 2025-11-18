using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internship_3_OOP
{
    internal class Passenger : Person
    {
        public List<Reservation> Flights {  get; set; }

        public Passenger(string name, string last_name, string email, string password, DateOnly birthday, Gender gender) : base(name, last_name, email, password, birthday, gender)
        {
            Flights = new List<Reservation>();
        }

        public void AddFlights(Guid flight_guid)
        {
            foreach (Reservation reservation in Flights)
            {
                if(reservation.FlightId == flight_guid)
                {
                    Flights.Add(reservation);
                }
            }
        }
        public override void PrintInfo()
        {

        }
    }
}
