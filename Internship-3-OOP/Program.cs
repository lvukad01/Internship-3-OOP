using System.Globalization;

namespace Internship_3_OOP
{
    internal class Program
    {
        static void Main()
        {
            Console.Clear();
            List<Passenger> passengers = new List<Passenger>()
            {
               new Passenger("Lana","Vukadin","lana.vukadin@gmail.com","lanalozinka",new DateOnly(2005,10,03),Gender.Female),
               new Passenger("Arijana","Radeljak","arijana.radeljak@gmail.com","arijanalozinka",new DateOnly(2005,10,02),Gender.Female),
               new Passenger("Andrea","Vukadin","andrea.vukadin@gmail.com","andrealozinka",new DateOnly(2001,10,07),Gender.Female),
               new Passenger("l","l","l","l",new DateOnly(2001,10,07),Gender.Female)

            };
            List<CrewMember>crewMembers = new List<CrewMember>()
            {
                new CrewMember("Ilva", "Peric", "ilva.peric@example.com", "ilvalozinka", new DateOnly(1992, 4, 12), Gender.Female, CrewPosition.Stewardess),
                new CrewMember("Mate", "Matic", "mate.matic@example.com", "matelozinka", new DateOnly(1987, 9, 3), Gender.Male, CrewPosition.Pilot),
                new CrewMember("Petra", "Karadza", "petra.karadza@example.com", "petralozinka", new DateOnly(1995, 2, 17), Gender.Female, CrewPosition.Stewardess),
                new CrewMember("Barbara", "Tomas", "barbara.tomas@example.com", "barbaralozinka", new DateOnly(1990, 8, 22), Gender.Female, CrewPosition.Stewardess),
                new CrewMember("Monika", "Dzaja", "monika.dzaja@example.com", "monikalozinka", new DateOnly(1994, 11, 5), Gender.Female, CrewPosition.Stewardess),
                new CrewMember("Nika", "Istuk", "nika.istuk@example.com", "nikalozinka", new DateOnly(1998, 1, 14), Gender.Female, CrewPosition.Stewardess),
                new CrewMember("Nika", "Vukadin", "nika.vukadin@example.com", "nikalozinka", new DateOnly(1997, 6, 29), Gender.Female, CrewPosition.Pilot),
                new CrewMember("Nikolina", "Tokic", "nikolina.tokic@example.com", "nikolinalozinka", new DateOnly(1993, 12, 20), Gender.Female, CrewPosition.Stewardess),
                new CrewMember("Zoran", "Karaula", "zoran.karaula@example.com", "zoranlozinka", new DateOnly(1985, 3, 10), Gender.Male, CrewPosition.Copilot),
                new CrewMember("Ivan", "Omazic", "ivan.omazic@example.com", "ivanlozinka", new DateOnly(1989, 5, 7), Gender.Male, CrewPosition.Copilot)
            };


            List<Airplane> airplanes = new List<Airplane>()
            {
                new Airplane("Airbus A320", 2015, 842, new Dictionary<SeatCategory,int>{{SeatCategory.Economy,150},{SeatCategory.Business,18},{SeatCategory.FirstClass,0}}),
                new Airplane("Boeing 737-800", 2012, 1094, new Dictionary<SeatCategory,int>{{SeatCategory.Economy,162},{SeatCategory.Business,12},{SeatCategory.FirstClass,0}}),
                new Airplane("Airbus A330-300", 2018, 540, new Dictionary<SeatCategory,int>{{SeatCategory.Economy,250},{SeatCategory.Business,36},{SeatCategory.FirstClass,8}}),
                new Airplane("Boeing 777-300ER", 2020, 320, new Dictionary<SeatCategory,int>{{SeatCategory.Economy,300},{SeatCategory.Business,40},{SeatCategory.FirstClass,8}}),
                new Airplane("Airbus A350-900", 2019, 410, new Dictionary<SeatCategory,int>{{SeatCategory.Economy,260},{SeatCategory.Business,48},{SeatCategory.FirstClass,12}})
            };
            var crews = new List<Crew>()
            {
                new Crew(
                    name:"Posada 1",
                    pilot: crewMembers.First(c => c.Name == "Mate"),
                    copilot: crewMembers.First(c => c.Name == "Zoran"),
                    attendants: new List<CrewMember>()
                    {
                        crewMembers.First(c => c.Name == "Ilva"),
                        crewMembers.First(c => c.Name == "Petra")
                    }
                ),
                new Crew(
                    name:"Posada 2",
                    pilot: crewMembers.First(c => c.Name == "Nika" && c.LastName == "Vukadin"),
                    copilot: crewMembers.First(c => c.Name == "Ivan"),
                    attendants: new List<CrewMember>()
                    {
                        crewMembers.First(c => c.Name == "Barbara"),
                        crewMembers.First(c => c.Name == "Monika")
                    }
                )
};
            List<Flight> flights = new List<Flight>()
            {
                new Flight("St-Zg",new DateTime(2025,12,23,15,30,00),new DateTime(2025,12,23,16,30,00),260,airplanes[0],crews[0]),
                new Flight("Split - Dubrovnik",new DateTime(2026, 8, 2, 6, 20, 0),new DateTime(2025, 8, 2, 7, 05, 0),165,airplanes[2],crews[1]),
                new Flight("Split - Frankfurt",new DateTime(2025, 11, 5, 13, 10, 0),new DateTime(2025, 11, 5, 15, 15, 0),950,airplanes[1],crews[1]),
                new Flight("Split - London",new DateTime(2025, 4, 12, 17, 55, 0),new DateTime(2025, 4, 12, 20, 35, 0),1550,airplanes[3],crews[0])
            };
            passengers[0].AddFlights(flights[0].Id,SeatCategory.Economy);
            passengers[1].AddFlights(flights[3].Id,SeatCategory.Business);
            passengers[2].AddFlights(flights[1].Id,SeatCategory.Economy);
            passengers[2].AddFlights(flights[2].Id, SeatCategory.FirstClass);
            passengers[3].AddFlights(flights[1].Id, SeatCategory.Business);
            passengers[3].AddFlights(flights[2].Id, SeatCategory.FirstClass);
            int input = 0;
            while (input != 5)
            {
                Console.Clear();
                Console.WriteLine("KONZOLNA APLIKACIJA ZA UPRAVLJANJE AERODROMOM\r\n");
                Console.WriteLine("1 - Putnici\r\n2 - Letovi\r\n3 – Avioni\r\n4 – Posada\r\n5 – Izlaz iz programa\r\n");
                input = ReadInt("");
                switch (input)
                {
                    case 1:
                        PassengerMenu(passengers, flights);
                        break;
                    case 2:
                        FlightMenu(flights,airplanes,crews);
                        break;
                    default:
                        break;

                }
            }

        }
        static void PassengerMenu(List<Passenger> passengers, List<Flight> flights)
        {

            int input = -1;
            while (input != 0)
            {
                Console.Clear();
                Console.WriteLine("PUTNICI\r\n1-Registracija\r\n2-Prijava\r\n0-Povratak u glavni izbornik");
                input = ReadInt("");
                switch (input)
                {
                    case 1:
                        RegisterPassenger(passengers);
                        break;
                    case 2:
                        PassengerLogIn(passengers, flights);
                        break;
                    case 0:
                        Console.WriteLine("Povratak u glavni izbornik,pritisnite bilo koju tipku");
                        Console.ReadKey();
                        return;
                    default:
                        Console.WriteLine("Pogrešan unos,pritisnite bilo koju tipku i pokušajte ponovo");
                        Console.ReadKey();
                        break;
                }
            }

        }
        static void RegisterPassenger(List<Passenger> passengers)
        {
            Console.Clear();
            string name = null, surname = null, email = null, password = null;
            int input = 0;
            DateOnly birthday = DateOnly.MinValue;

            name = ReadNonEmpty("Unesite ime: ");

            surname = ReadNonEmpty("Unesite prezime: ");
            while (email == null)
            {
                email = ReadNonEmpty("Unesite email: ");
                if (IsValidEmail(email))
                    continue;
                else
                    Console.WriteLine("Neispravan format emaila,pokušajte ponovo.");

                foreach (var passenger in passengers)
                {

                    if (passenger.Email == email)
                    {
                        Console.WriteLine("Korisnik s tim mailom već postoji.\nAko želite odustati od radnje unesite 0,za promjenu unesenog maila unesite bilo koji drugi broj");
                        {
                            input = ReadInt("");
                            if (input == 0)
                                return;
                            else
                                email = null;
                        }

                    }
                }
            }
            while (password == null)
            {
                password =ReadNonEmpty("Unesite lozinku: ");
                if (password.Length < 8)
                {
                    Console.WriteLine("Lozinka mora imati min 8 znakova\n");
                    password = null;
                }
            }
            while (birthday == DateOnly.MinValue)
            {
                birthday = ReadDate("Unesi datum rođenja (yyyy-mm-dd): ");
                if (birthday.Year < 1925 || birthday.Year > 2025)
                {
                    Console.WriteLine("Unesite godinu veću od 1925 i manju od 2025");
                    birthday = DateOnly.MinValue;
                }
            }            
            Gender gender = ChooseGender();
            Console.Write($"Želite li nastaviti s registracijom korisnika {name} {surname}?(y/n)");
            var confirm = ReadChar("");
            confirm = char.ToLower(confirm);
            if (confirm != 'y')
            {
                Console.WriteLine("Registracija otkazana, pritisnite bilo koju tipku za povratak u prethudni izbornik.");
                Console.ReadKey();
                return;

            }
            Passenger p = new Passenger(name, surname, email, password, birthday, gender);
            passengers.Add(p);
            Console.WriteLine("Registracija dovršena, pritisnite bilo koju tipku za povratak u prethudni izbornik.");
            Console.ReadKey();
            return;
        }
        static Gender ChooseGender()
        {
            while (true)
            {
                Console.WriteLine("Odaberite spol:");
                foreach (var value in Enum.GetValues(typeof(Gender)))
                {
                    Console.WriteLine($"{(int)value} - {value}");
                }

                Console.Write("Unesite broj: ");
                string input = Console.ReadLine();

                if (int.TryParse(input, out int number) && Enum.IsDefined(typeof(Gender), number))
                {
                    return (Gender)number;
                }

                Console.WriteLine("Neispravan unos, pokušajte ponovno.\n");
            }
        }
        static void PassengerLogIn(List<Passenger> passengers, List<Flight> flights)
        {
            Console.Clear();
            int input = -1;
            string email = null, password = null;
            Passenger passenger = null;
            while (input != 0)
            {
                email = ReadNonEmpty("Email: ");
                password = ReadNonEmpty("Lozinka: ");
                foreach (var user in passengers)
                {
                    if (string.Equals(user.Email, email, StringComparison.OrdinalIgnoreCase) && string.Equals(user.Password, password))
                    {
                        passenger = user;
                        input = 0;
                    }
                }
                if (input != 0)
                {
                    Console.WriteLine("Pogrešna lozinka ili korisnički račun ne postoji, ako želite odustati od prijave unesite 0,za nastavak unesite bilo koji drugi broj");
                    input = ReadInt("");
                    if (input == 0)
                    {
                        Console.WriteLine("Vraćanje u početni izbornik,pritisnite bilo koju tipku");
                        Console.ReadKey();
                        return;
                    }
                }
            }
            Console.WriteLine("Prijava u korisnički račun je uspješna,pritisnite bilo koju tipku za nastavak");
            Console.ReadKey();
            while (input != 5)
            {
                Console.Clear();
                Console.WriteLine("Odabarite uslugu:\r\n1. Prikaz svih rezerviranih letova\r\n2.Odabir leta\r\n3.Pretraživanje letova\r\n4.Otkazivanje leta\r\n5.Povratak na glavni izbornik ");
                input = ReadInt("");
                switch (input)
                {
                    case 1:
                        PassengerFlightList(passenger, flights);
                        break;
                    case 2:
                        ReserveFlight(passenger, flights);
                        break;
                    case 3:
                        PassengerSearchFlight(passenger, flights);
                        break;
                    case 4:
                        PassengerCancelFlight(passenger, flights);
                        break;
                    case 5:
                        Console.WriteLine("Povratak u glavni izbornik, pritisnite bilo koju tipku");
                        Console.ReadKey();
                        return;
                }
            }
        }
        static void PassengerFlightList(Passenger passenger, List<Flight> flights)
        {
            Console.Clear();
            if(passenger.Flights.Count==0)
            {
                Console.WriteLine("Korisnik nema rezerviranih letova, za povratak u prethodni izbornik pritisnite bilo koju tipku");
                Console.ReadKey();
                return;
            }
            Console.WriteLine("ID - Naziv - Datum polaska - Datum dolaska - Udaljenost - Vrijeme putovanja\n");
            foreach (var Id in passenger.Flights)
            {
                var flight = flights.FirstOrDefault(f => f.Id ==Id.FlightId);
                flight.Print();
            }
            Console.WriteLine("\n\nZa povratak pritisnite bilo koju tipku");
            Console.ReadKey();
        }
        static void ReserveFlight(Passenger passenger, List<Flight> flights)
        {
            Console.Clear();
            var input = -1;
            int counter = 0;
            Flight chosenFlight=null;
            List<Flight> availableFlights=new List<Flight>();
            foreach(var flight in flights)
            {
                int availableSeats = flight.getNumberofAllSeats();
                if (availableSeats > 0 && flight.Departure> DateTime.Now)
                {
                    availableFlights.Add(flight);
                    counter++;
                }
            }
            if (counter == 0)
            {
                Console.WriteLine("Nema dostupnih letova, za povratak u prethodni izbornik pritisnite bilo koju tipku");
                Console.ReadKey();
                return;
            }
            Console.WriteLine("ID - Naziv - Datum i vrijeme polaska - Datum i vrijeme dolaska - Udaljenost - Vrijeme putovanja\n");

            foreach (var flight in availableFlights)
                    flight.Print();

            Console.WriteLine("Unesite ID leta koji želite rezervirati,ako želite odustati unesite 0: ");
            while (input != 0)
            {
                input = ReadInt("");
                chosenFlight = availableFlights.FirstOrDefault(f => f.DisplayId == input);
                if (chosenFlight == null)
                {
                    Console.WriteLine("Let s tim ID-om ne postoji, ako želite odustati od odabira unesite 0,za nastavak unesite bilo koji drugi broj");
                    input = ReadInt("");
                    if (input == 0)
                    {
                        Console.WriteLine("Vraćanje u prethodni izbornik, pritisnite bilo koju tipku");
                        Console.ReadKey();
                        return;
                    }
                }
                else if(passenger.Flights.Any(f=> f.FlightId == chosenFlight.Id))
                {
                    Console.WriteLine("Taj let ste već rezervirali");
                }
                else if(input==0)
                {
                    Console.WriteLine("Povratak na prethodni izbornik, pritisnite bilo koju tipku");
                    Console.ReadKey();
                    return;
                }
                else
                    input = 0;
            }
            ChooseCategory(passenger,chosenFlight);
        }
        static void ChooseCategory(Passenger passenger,Flight flight)
        {
            int input = -1;
            char choice = '0';
            int freeSeats = 0;
            Console.WriteLine("Odaberite dostupnu kategoriju sjedala koje želite rezervirati");

            foreach (var category in flight.Airplane.Seat)
            {
                freeSeats = flight.getNumberofSeats(category.Key);
                if (category.Value > 0)
                {
                    Console.WriteLine($"{(int)category.Key + 1}.{category.Key}- broj dostupnih sjedala: {freeSeats}");
                }              
            }
            while (!int.TryParse(Console.ReadLine(), out input) || !Enum.IsDefined(typeof(SeatCategory), input - 1))
                {
                    Console.WriteLine("Neispravan unos, unesite ID kategorije:");
                }
               SeatCategory chosenSeat=(SeatCategory)(input-1);
            Console.WriteLine($"Jeste li sigurni da želite rezervirati sjedalo kategorije {chosenSeat}?(y/n)");
            choice = ReadChar("");
            Reservation reserve=new Reservation(flight.Id,chosenSeat);
            if (choice == 'y')
            {
                passenger.Flights.Add(reserve);
                flight.ReservedSeats[chosenSeat]++;
                Console.WriteLine("Rezervacija je uspješna,pritisnite bilo koju tipku");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Odustajanje od rezerviranja");
                return;
            }
        }
        static void PassengerSearchFlight(Passenger passenger,List<Flight> flights)
        {
            Console.Clear();
            Console.WriteLine("Pretraživanje letova:\r\na)po ID-u\r\nb)po nazivu");
            var choice = '0';
            while (choice == '0')
            {
                choice = ReadChar("");
                switch (choice)
                {
                    case 'a':
                        PassengerSearchFlightId(passenger,flights);
                        break;
                    case 'b':
                        PassengerSearchFlightName(passenger,flights);
                        break;
                    default:
                        Console.WriteLine("Pogrešan unos, unesite a ili b,pritisnite bilo koju tipku pa pokušajte ponovo");
                        choice = '0';
                        Console.ReadKey();
                        break;
                }
            }
        }
        static void PassengerSearchFlightId(Passenger passenger, List<Flight> flights)
        {
            int flightID = ReadInt("Unesite ID leta koji želite pretražiti:\n");

            Flight chosenFlight = flights.FirstOrDefault(f => f.DisplayId == flightID);

            if (chosenFlight != null)
            {
                Console.WriteLine("ID - Naziv - Datum i vrijeme polaska - Datum i vrijeme dolaska - Udaljenost - Vrijeme putovanja\n");
                chosenFlight.Print();
            }
            else
            {
                Console.WriteLine("Ne postoji let s tim ID-om");
            }
            Console.WriteLine("Pritisnite bilo koju tipku za povratak u prethodni izbornik");
            Console.ReadKey();

        }
        static void PassengerSearchFlightName(Passenger passenger, List<Flight> flights)
        {
            string flightName = ReadNonEmpty("Unesite naziv leta koji želite pretražiti:\n");
            Flight chosenFlight=flights.FirstOrDefault(f=>string.Equals(flightName.Trim(),f.Name.Trim(), StringComparison.OrdinalIgnoreCase));
            if (chosenFlight != null)
            {
                Console.WriteLine("ID - Naziv - Datum i vrijeme polaska - Datum i vrijeme dolaska - Udaljenost - Vrijeme putovanja\n");
                chosenFlight.Print();
            }
            else
            {
                Console.WriteLine("Ne postoji let s tim ID-om");
            }
            Console.WriteLine("Pritisnite bilo koju tipku za povratak u prethodni izbornik");
            Console.ReadKey();

        }
        static void PassengerCancelFlight(Passenger passenger, List<Flight> flights)
        {
            Console.Clear();
            var input = -1;
            char choice = '0';
            Flight chosenFlight = null;
            if (passenger.Flights.Count == 0)
            {
                Console.WriteLine("Korisnik nema rezerviranih letova, za povratak u prethodni izbornik pritisnite bilo koju tipku");
                Console.ReadKey();
                return;
            }

            while (input != 0)
            {
                Console.Clear();
                Console.WriteLine("Rezervirani letovi:");
                Console.WriteLine("ID - Naziv - Datum i vrijeme polaska - Datum i vrijeme dolaska - Udaljenost - Vrijeme putovanja\n");
                foreach (var Id in passenger.Flights)
                {
                    var flight = flights.FirstOrDefault(f => f.Id == Id.FlightId);
                    flight.Print();
                }
                Console.WriteLine("Unesite ID leta koji želite izbrisati,ako želite odustati unesite 0: ");
                input = ReadInt("");
                chosenFlight =flights.FirstOrDefault(f => f.DisplayId == input);
                if (chosenFlight == null||!passenger.Flights.Any(f => f.FlightId == chosenFlight.Id))
                { 
                    Console.WriteLine("Let s tim ID-om nije na popisu, ako želite odustati od odabira unesite 0,za nastavak unesite bilo koji drugi broj");
                    input = ReadInt("");
                    if (input == 0)
                    {
                        Console.WriteLine("Vraćanje u prethodni izbornik, pritisnite bilo koju tipku");
                        Console.ReadKey();
                        return;
                    }
                }
                else if (input == 0)
                {
                    Console.WriteLine("Povratak na prethodni izbornik, pritisnite bilo koju tipku");
                    Console.ReadKey();
                    return;
                }
                else
                    input = 0;
            }
            TimeSpan timeUntilFlight = chosenFlight.Departure - DateTime.Now;
            if (timeUntilFlight < TimeSpan.FromHours(24))
            {
                Console.WriteLine("Nemoguće otkazati let koji je za manje od 24 sata, pritisnite bilo koju tipku za povratak u prethodni izbornik");
                Console.ReadKey();
                return;
            }
            Reservation deleteReservation = passenger.Flights.FirstOrDefault(f => f.FlightId == chosenFlight.Id);

            Console.WriteLine($"Jeste li sigurni da želite izbrisati rezervaciju za let {chosenFlight.Name}?(y/n)");
            choice = ReadChar("");
            if (choice != 'y')
                Console.WriteLine("Brisanje rezervacije otkazano");
            else
            { 
                passenger.CancelFlight(deleteReservation);
                chosenFlight.ReservedSeats[deleteReservation.Category]--;
                Console.WriteLine("Brisanje rezervacije dovršeno");
            }
            Console.WriteLine("\n\nZa povratak pritisnite bilo koju tipku");
            Console.ReadKey();
        }
        static void FlightMenu(List<Flight> flights,List <Airplane> airplanes,List<Crew> crews)
        {
            int input = 0;
            while (input != 6)
            {
                Console.Clear();
                Console.WriteLine("Letovi:\r\n1. Prikaz svih letova\r\n2.Dodavanje leta\r\n3.Pretraživanje letova\r\n4.Uređivanje leta\r\n5.Brisanje leta\r\n6.Povratak na glavni izbornik ");
                input = ReadInt("");
                switch (input)
                {
                    case 1:
                        FlightList( flights);
                        break;
                    case 2:
                        AddFlight(flights,airplanes,crews);
                        break;
                    case 3:
                        SearchFlight(flights);
                        break;
                    case 4:
                        EditFlight(flights);
                        break;
                    case 5:
                        DeleteFlight(flights);
                        break;
                    case 6:
                        Console.WriteLine("Povratak u glavni izbornik, pritisnite bilo koju tipku");
                        Console.ReadKey();
                        return;
                }
            }
        }
        static void FlightList(List<Flight> flights)
        {
            Console.WriteLine("\nID - Naziv - Datum i vrijeme polaska - Datum i vrijeme dolaska - Udaljenost - Vrijeme putovanja\n");
            foreach (var flight in flights)
            {
                flight.Print();
            }
            Console.WriteLine("\nZa povratak u prethodni izbornik pritisnite bilo koju tipku");
            Console.ReadKey();
        }
        static void AddFlight(List<Flight> flights,List<Airplane> airplanes,List<Crew> crews)
        {
            Console.Clear();
            string departureTown = null;
            string arrivalTown = null;
            string name = null;
            DateTime departure = DateTime.MinValue;
            DateTime arrival= DateTime.MinValue;
            double distance = 0;
            Airplane chosenAirplane = null;
            Crew chosenCrew = null;
            Flight addFlight = null;
            departureTown = ReadNonEmpty("Unesite naziv grada polaska: ");
            arrivalTown = ReadNonEmpty("Unesite naziv grada u koji putujete: ");
            name = departureTown + " - " + arrivalTown;
            while (true)
            {
                departure = ReadDateTime("Unesite datum i vrijeme polaska u formatu (dd.MM.yyyy HH:mm) ");
                arrival = ReadDateTime("Unesite datum i vrijeme polaska u formatu (dd.MM.yyyy HH:mm)");

                if (arrival <= departure)
                    Console.WriteLine("Vrijeme dolaska mora biti nakon vremena polaska, pokušajte ponovo\n");
                else
                    break;
            }
            distance = ReadDouble("Unesite udaljenost u km: ");
            chosenAirplane = ChooseAirplane(airplanes);
            chosenCrew = ChooseCrew(crews);
            addFlight=new Flight(name,departure,arrival,distance, chosenAirplane,chosenCrew);
            flights.Add(addFlight);
            Console.WriteLine("Let uspješno dodan, za povratak na prethodni izbornik pritisnite bilo koju tipku");
            Console.ReadKey();

        }
        static Airplane ChooseAirplane(List<Airplane> airplanes)
        {
            Console.WriteLine("ID - Naziv - Godina Proizvodnje - Broj letova");
            var input = 0;
            Airplane chosenAirplane = null;
            foreach (var airplane in airplanes)
            {
                airplane.Print();
            }
            while (input == 0)
            {
                Console.WriteLine("Odaberite avion za let");
                input = ReadInt("");
                chosenAirplane = airplanes.FirstOrDefault(a => a.DisplayId == input);
                if (chosenAirplane == null)
                {
                    Console.WriteLine("Ne postoji avion s unesenim ID-om, pokušajte ponovo");
                    input = 0;
                }
            }
            return chosenAirplane;
        }
        static Crew ChooseCrew(List<Crew> crews)
        {
            Console.WriteLine("Naziv posade:");
            var input = 0;
            Crew chosenCrew = null;
            foreach (var crew in crews)
            {
                Console.WriteLine($"{crew.DisplayId} - {crew.Name}");
            }
            while (input == 0)
            {
                Console.WriteLine("Odaberite posadu za let");
                input = ReadInt("");
                chosenCrew= crews.FirstOrDefault(c => c.DisplayId == input);
                if (chosenCrew == null)
                {
                    Console.WriteLine("Ne postoji posada s unesenim ID-om, pokušajte ponovo");
                    input = 0;
                }
            }
            return chosenCrew;
        }
        static void SearchFlight(List<Flight> flights)
        {
            Console.Clear();
            char input = ' ';
            Console.WriteLine("Pretraživanje leta:\r\na)po ID-u\r\nb)po nazivu\r\n0)Povratak u prethodni izbornik");
            while(input!='0')
            {

                input = ReadChar("");
                switch(input)
                {
                    case 'a':
                        SearchFlightId(flights);
                        return;
                    case 'b':
                        SearchFlightName(flights);
                        return;
                    case '0':
                        break;
                    default:
                        Console.WriteLine("Pogrešan unos, unesite a ili b, za povratak unesite 0 ");
                        break;
                }

            }
            Console.WriteLine("Povratak u prethodni izbornik,pritisnite bilo koju tipku");
            Console.ReadKey();
        }
        static void SearchFlightId(List<Flight> flights)
        {
            Console.Clear();
            int flightID = ReadInt("Pretraživanje leta po ID-u\n\nUnesite ID leta koji želite pretražiti:\n");

            Flight chosenFlight = flights.FirstOrDefault(f => f.DisplayId == flightID);

            if (chosenFlight != null)
            {
                Console.WriteLine("\nID - Naziv - Datum i vrijeme polaska - Datum i vrijeme dolaska - Udaljenost - Vrijeme putovanja");
                chosenFlight.Print();
            }
            else
            {
                Console.WriteLine("Ne postoji let s tim ID-om");
            }
            Console.WriteLine("Pritisnite bilo koju tipku za povratak u izbornik letova");
            Console.ReadKey();
        }
        static void SearchFlightName(List<Flight> flights)
        {
            Console.Clear();
            string flightName = ReadNonEmpty("Pretraživanje leta po nazivu\n\nUnesite naziv leta koji želite pretražiti:\n");
            Flight chosenFlight = flights.FirstOrDefault(f => string.Equals(flightName.Trim(), f.Name.Trim(), StringComparison.OrdinalIgnoreCase));
            if (chosenFlight != null)
            {
                Console.WriteLine("\nID - Naziv - Datum i vrijeme polaska - Datum i vrijeme dolaska - Udaljenost - Vrijeme putovanja");
                chosenFlight.Print();
            }
            else
            {
                Console.WriteLine("Ne postoji let s tim ID-om");
            }
            Console.WriteLine("Pritisnite bilo koju tipku za povratak u prethodni izbornik");
            Console.ReadKey();

        }
        static void EditFlight(List<Flight> flights)
        {

        }
        static void DeleteFlight(List<Flight> flights)
        {

        }

        static double ReadDouble(string message)
        {
            Console.Write(message);
            double number;

            while (!double.TryParse(Console.ReadLine(), out number)||number<0)
                Console.Write("Neispravan unos, pokušajte ponovno: ");

            return number;
        }
        static DateOnly ReadDate(string message)
        {
            Console.Write(message);
            DateOnly date;

            while (!DateOnly.TryParse(Console.ReadLine(), out date))
                Console.Write("Neispravan datum, unesite u formatu yyyy-mm-dd: ");

            return date;
        }
        static DateTime ReadDateTime(string message)
        {
            Console.Write(message);
            DateTime date;

            while (!DateTime.TryParse(Console.ReadLine(), out date))
                Console.Write("Neispravan datum, unesite u formatu yyyy-mm-dd hour:min : ");

            return date;
        }
        static TimeOnly ReadTime(string message)
        {
            Console.Write(message);
            TimeOnly time;

            while (!TimeOnly.TryParse(Console.ReadLine(), out time))
                Console.Write("Neispravan unos sati, unesite u formatu (hour:min): ");

            return time;
        }
        static string ReadNonEmpty(string message)
        {
            Console.Write(message);
            string input = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(input))
            {
                Console.Write("Polje ne može biti prazno, pokušajte ponovno: ");
                input = Console.ReadLine();
            }

            return input;
        }
        static int ReadInt(string message)
        {
            Console.Write(message);
            int value;

            while (!int.TryParse(Console.ReadLine(), out value))
            {
                Console.Write("Neispravan unos, unesite cijeli broj: ");
            }

            return value;
        }
        static char ReadChar(string message)
        {
            Console.Write(message);
            string input = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(input) || input.Length != 1)
            {
                Console.Write("Neispravan unos, unesite samo jedan znak: ");
                input = Console.ReadLine();
            }

            return input[0];
        }
        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

    }
}
