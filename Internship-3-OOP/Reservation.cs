using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internship_3_OOP
{
    public class Reservation
    {
        public Guid FlightId { get; set; }
        public SeatCategory Category { get; set; }
        public DateTime Created { get; private set; }
        public DateTime Updated { get; private set; }
        public Reservation(Guid flightId, SeatCategory category)
        {
            FlightId = flightId;
            Category = category;
            Created = DateTime.Now;
            Updated = DateTime.Now;
        }
        public void Update()
        {
            Updated = DateTime.Now;
        }
    }
}
