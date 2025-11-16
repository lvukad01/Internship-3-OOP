using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internship_3_OOP
{
    internal class Flight
    {
        public Guid Id { get;private set; }
        public string Name { get; set; }
        public DateTime DepartureTime {  get; set; }
        public DateTime ArrivalTime {  get; set; }
        public double Distance {  get; set; }
        public TimeOnly Duration {  get; set; }
        public DateTime Created { get;private set; }
        public DateTime Updated { get;private set; }
        public Flight(string name, DateTime departureTime, DateTime arrivalTime, double distance, TimeOnly duration)
        {
            Id= Guid.NewGuid();
            Name = name;
            DepartureTime = departureTime;
            ArrivalTime = arrivalTime;
            Distance = distance;
            Duration = duration;
            Created = DateTime.Now;
            Updated = DateTime.Now;
        }
        public void Update()
        {
            Updated = DateTime.Now;
        }
    }
}
