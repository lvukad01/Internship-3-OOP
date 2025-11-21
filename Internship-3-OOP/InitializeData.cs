using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internship_3_OOP
{
    internal class InitializeData
    {
        public static List<Passenger> Passengers { get; private set; }
        public static List<CrewMember> CrewMembers { get; private set; }
        public static List<Airplane> Airplanes { get; private set; }
        public static List<Crew> Crews { get; private set; }
        public static List<Flight> Flights { get; private set; }

        static InitializeData()
        {
            InitializeCrewMembers(); 
            InitializeAirplanes();    
            InitializeCrews();      
            InitializeFlights();       
            InitializePassengers();
        }

        private static void InitializePassengers()
        {
            Passengers = new List<Passenger>()
            {
               new Passenger("Lana","Vukadin","lana.vukadin@gmail.com","lanalozinka",new DateOnly(2005,10,03),Gender.Female),
               new Passenger("Arijana","Radeljak","arijana.radeljak@gmail.com","arijanalozinka",new DateOnly(2005,10,02),Gender.Female),
               new Passenger("Andrea","Vukadin","andrea.vukadin@gmail.com","andrealozinka",new DateOnly(2001,10,07),Gender.Female),
               new Passenger("l","l","l","l",new DateOnly(2001,10,07),Gender.Female)
            };
            Passengers[0].AddFlights(Flights[0].Id, SeatCategory.Economy);
            Passengers[1].AddFlights(Flights[3].Id, SeatCategory.Business);
            Passengers[2].AddFlights(Flights[1].Id, SeatCategory.Economy);
            Passengers[2].AddFlights(Flights[2].Id, SeatCategory.FirstClass);
            Passengers[3].AddFlights(Flights[1].Id, SeatCategory.Business);
            Passengers[3].AddFlights(Flights[2].Id, SeatCategory.FirstClass);
        }

        private static void InitializeCrewMembers()
        {
            CrewMembers = new List<CrewMember>()
            {
                new CrewMember("Ilva", "Peric", "ilva.peric@gmail.com", "ilvalozinka", new DateOnly(1992, 4, 12), Gender.Female, CrewPosition.Stewardess),
                new CrewMember("Mate", "Matic", "mate.matic@gmail.com", "matelozinka", new DateOnly(1987, 9, 3), Gender.Male, CrewPosition.Pilot),
                new CrewMember("Petra", "Karadza", "petra.karadza@gmail.com", "petralozinka", new DateOnly(1995, 2, 17), Gender.Female, CrewPosition.Stewardess),
                new CrewMember("Barbara", "Tomas", "barbara.tomas@gmail.com", "barbaralozinka", new DateOnly(1990, 8, 22), Gender.Female, CrewPosition.Stewardess),
                new CrewMember("Monika", "Dzaja", "monika.dzaja@gmail.com", "monikalozinka", new DateOnly(1994, 11, 5), Gender.Female, CrewPosition.Stewardess),
                new CrewMember("Nika", "Istuk", "nika.istuk@gmail.com", "nikalozinka", new DateOnly(1998, 1, 14), Gender.Female, CrewPosition.Stewardess),
                new CrewMember("Nika", "Vukadin", "nika.vukadin@gmail.com", "nikalozinka", new DateOnly(1997, 6, 29), Gender.Female, CrewPosition.Pilot),
                new CrewMember("Nikolina", "Tokic", "nikolina.tokic@gmail.com", "nikolinalozinka", new DateOnly(1993, 12, 20), Gender.Female, CrewPosition.Stewardess),
                new CrewMember("Zoran", "Karaula", "zoran.karaula@gmail.com", "zoranlozinka", new DateOnly(1985, 3, 10), Gender.Male, CrewPosition.Copilot),
                new CrewMember("Ivan", "Omazic", "ivan.omazic@gmail.com", "ivanlozinka", new DateOnly(1989, 5, 7), Gender.Male, CrewPosition.Copilot)
            };
        }

        private static void InitializeAirplanes()
        {
            Airplanes = new List<Airplane>()
            {
                new Airplane("Airbus A320", 2015, 842,
                    new Dictionary<SeatCategory,int>{{SeatCategory.Economy,150},{SeatCategory.Business,18},{SeatCategory.FirstClass,0}}),
                new Airplane("Boeing 737-800", 2012, 1094,
                    new Dictionary<SeatCategory,int>{{SeatCategory.Economy,162},{SeatCategory.Business,12},{SeatCategory.FirstClass,0}}),
                new Airplane("Airbus A330-300", 2018, 540,
                    new Dictionary<SeatCategory,int>{{SeatCategory.Economy,250},{SeatCategory.Business,36},{SeatCategory.FirstClass,8}}),
                new Airplane("Boeing 777-300ER", 2020, 320,
                    new Dictionary<SeatCategory,int>{{SeatCategory.Economy,300},{SeatCategory.Business,40},{SeatCategory.FirstClass,8}}),
                new Airplane("Airbus A350-900", 2019, 410,
                    new Dictionary<SeatCategory,int>{{SeatCategory.Economy,260},{SeatCategory.Business,48},{SeatCategory.FirstClass,12}})
            };
        }

        private static void InitializeCrews()
        {
            Crews = new List<Crew>()
            {
                new Crew(
                    name: "Posada 1",
                    pilot: CrewMembers.First(c => c.Name == "Mate"),
                    copilot: CrewMembers.First(c => c.Name == "Zoran"),
                    attendants: new List<CrewMember>()
                    {
                        CrewMembers.First(c => c.Name == "Ilva"),
                        CrewMembers.First(c => c.Name == "Petra")
                    }
                ),
                new Crew(
                    name: "Posada 2",
                    pilot: CrewMembers.First(c => c.Name == "Nika" && c.LastName == "Vukadin"),
                    copilot: CrewMembers.First(c => c.Name == "Ivan"),
                    attendants: new List<CrewMember>()
                    {
                        CrewMembers.First(c => c.Name == "Barbara"),
                        CrewMembers.First(c => c.Name == "Monika")
                    }
                )
            };
        }

        private static void InitializeFlights()
        {
            Flights = new List<Flight>()
            {
                new Flight("St-Zg",
                    new DateTime(2025,12,23,15,30,00),
                    new DateTime(2025,12,23,16,30,00),
                    260,
                    Airplanes[0],
                    Crews[0]),

                new Flight("Split - Dubrovnik",
                    new DateTime(2025, 8, 2, 6, 20, 0),
                    new DateTime(2025, 8, 2, 7, 05, 0),
                    165,
                    Airplanes[2],
                    Crews[1]),

                new Flight("Split - Frankfurt",
                    new DateTime(2025, 11, 5, 13, 10, 0),
                    new DateTime(2025, 11, 5, 15, 15, 0),
                    950,
                    Airplanes[1],
                    Crews[1]),

                new Flight("Split - London",
                    new DateTime(2025, 4, 12, 17, 55, 0),
                    new DateTime(2025, 4, 12, 20, 35, 0),
                    1550,
                    Airplanes[3],
                    Crews[0])
            };
        }

    }
}
