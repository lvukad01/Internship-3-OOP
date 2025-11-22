

namespace Internship_3_OOP
{
    internal class Passenger : Person
    {
        public List<Reservation> Flights {  get; set; }

        public Passenger(string name, string last_name, string email, string password, DateOnly birthday, Gender gender) : base(name, last_name, email, password, birthday, gender)
        {
            Flights = new List<Reservation>();
        }

        public void AddFlights(Guid flight_guid,SeatCategory seatCategory)
        {
            Flights.Add(new Reservation(flight_guid, seatCategory));
        }
        public void CancelFlight(Reservation reservation)
        {
            Flights.Remove(reservation);
        }
        public override void PrintInfo()
        {

        }
    }
}
