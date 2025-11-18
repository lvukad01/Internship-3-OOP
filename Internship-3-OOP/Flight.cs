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
        public DateTime DepartureTime {  get; set; }
        public DateTime ArrivalTime {  get; set; }
        public double Distance {  get; set; }
        public TimeOnly Duration {  get; set; }
        public DateTime Created { get;private set; }
        public DateTime Updated { get;private set; }
        public Airplane Airplane { get; set; }
        public Dictionary<SeatCategory, int> ReservedSeats { get; set; }

        public Flight(string name, DateTime departureTime, DateTime arrivalTime, double distance, TimeOnly duration,Airplane airplane)
        {
            Id= Guid.NewGuid();
            DisplayId = counter + 1;
            counter++;
            Name = name;
            DepartureTime = departureTime;
            ArrivalTime = arrivalTime;
            Distance = distance;
            Duration = duration;
            Airplane = airplane;
            Created = DateTime.Now;
            Updated = DateTime.Now;
            ReservedSeats = new Dictionary<SeatCategory, int>()
            {
                {SeatCategory.Economy, 0},
                {SeatCategory.Business, 0},
                {SeatCategory.FirstClass, 0}
            };
        }
        public void Update()
        {
            Updated = DateTime.Now;
        }
        public int getNumberofSeats(SeatCategory category)
        {
            int totalSeats = Airplane.Seat[category];

            int reservedSeats = ReservedSeats.Count(r => r.Key == category);
            return totalSeats - reservedSeats;
        }
        public int getNumberofAllSeats()
        {
            int availableSeats=0;
            foreach(var seat in Airplane.Seat)
            {
                foreach (var reservedSeat in ReservedSeats)
                {
                    if (Airplane.Seat.Contains(reservedSeat))
                    {
                        availableSeats += (seat.Value - reservedSeat.Value);
                    }
                }
            }
            return availableSeats;
        }
        public void Print()
        {
            Console.WriteLine($"{DisplayId} - {Name} - {DepartureTime} - {ArrivalTime} - {Distance} - {Duration}");
        }
    }
}
