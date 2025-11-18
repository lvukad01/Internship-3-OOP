using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internship_3_OOP
{
    internal class Airplane
    {
        public Guid Id { get; private set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public int NumberofFlights {  get; set; }
        public Dictionary<SeatCategory,int> Seat {  get; set; }
        public DateTime Created { get;private set; }
        public DateTime Updated { get;private set; }
        public  Airplane(string name,int year,int numberofFlights, Dictionary<SeatCategory, int> seat)
        {
            Id= Guid.NewGuid();
            Name = name;
            Year = year;
            NumberofFlights=numberofFlights;
            Seat = seat;
            Created = DateTime.Now;
            Updated = DateTime.Now;
        }
        public void Update()
        {
            Updated = DateTime.Now;
        }
    }
}
