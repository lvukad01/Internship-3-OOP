using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internship_3_OOP
{
    internal class Flight
    {
        public Guid Id { get; private set; }
        public static int counter=1;
        public int DisplayId {  get; private set; }
        public string Name { get; set; }
        public DateTime Departure {  get; set; }
        public DateTime Arrival {  get; set; }
        public double Distance {  get; set; }
        public TimeSpan Duration { get; set; }
        public DateTime Created { get;private set; }
        public DateTime Updated { get;private set; }
        public Airplane Airplane { get; set; }
        public Crew Crew { get; set; }
        public Dictionary<SeatCategory, int> ReservedSeats { get; set; }

        public Flight(string name, DateTime departure, DateTime arrival, double distance,Airplane airplane, Crew crew)
        {
            Id = Guid.NewGuid();
            DisplayId = counter;
            counter++;
            Name = name;
            Departure = departure;
            Arrival = arrival;
            Distance = distance;
            Duration = arrival - departure;
            Airplane = airplane;
            Created = DateTime.Now;
            Updated = DateTime.Now;
            ReservedSeats = new Dictionary<SeatCategory, int>()
            {
                {SeatCategory.Economy, 0},
                {SeatCategory.Business, 0},
                {SeatCategory.FirstClass, 0}
            };
            Crew = crew;
        }
        public void Update()
        {
            Updated = DateTime.Now;
        }
        public int getNumberofSeats(SeatCategory category)
        {
            int totalSeats = Airplane.Seat[category];
            int reservedSeats = ReservedSeats[category];
            return totalSeats - reservedSeats;
        }
        public int getNumberofAllSeats()
        {
            int availableSeats = 0;
            foreach (var seat in Airplane.Seat)
            {
                SeatCategory category = seat.Key;
                int total = seat.Value;
                int reserved = ReservedSeats[category];
                availableSeats += (total - reserved);
            }
            return availableSeats;
        }
        public void Print()
        {
            Console.WriteLine($"{DisplayId} - {Name} - {Departure} - {Arrival} - {Distance} km - {Duration} h");
        }
    }
}
