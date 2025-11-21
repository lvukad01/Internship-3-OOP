using System.Globalization;
using System.Linq;
using System.Xml.Linq;

namespace Internship_3_OOP
{
    internal class Program
    {
        static void Main()
        {
            Console.Clear();
            var crewMembers = InitializeData.CrewMembers;
            var airplanes = InitializeData.Airplanes;
            var crews = InitializeData.Crews;
            var flights = InitializeData.Flights;
            var passengers = InitializeData.Passengers;


            int input = 0;
            while (input != 5)
            {
                Console.Clear();
                Console.WriteLine("KONZOLNA APLIKACIJA ZA UPRAVLJANJE AERODROMOM\r\n");
                Console.WriteLine("1 - Putnici\r\n2 - Letovi\r\n3 – Avioni\r\n4 – Posada\r\n5 – Izlaz iz programa\r\n");
                input = Helper.ReadInt("");
                switch (input)
                {
                    case 1:
                        PassengerMenu(passengers, flights);
                        break;
                    case 2:
                        FlightMenu(flights,airplanes,crews,passengers);
                        break;
                    case 3:
                        AirplaneMenu(airplanes,flights);
                        break;
                    case 4:
                        CrewMenu(crews, crewMembers);
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
                input = Helper.ReadInt("");
                switch (input)
                {
                    case 1:
                        RegisterPassenger(passengers,flights);
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
        static void RegisterPassenger(List<Passenger> passengers,List<Flight> flights)
        {
            Console.Clear();
            string name = null, surname = null, email = null, password = null;
            int input = 0;
            DateOnly birthday = DateOnly.MinValue;

            name = Helper.ReadNonEmpty("Unesite ime: ");

            surname = Helper.ReadNonEmpty("Unesite prezime: ");
            while (email == null)
            {
                email = Helper.ReadNonEmpty("Unesite email: ");
                if (!Helper.IsValidEmail(email))
                    Console.WriteLine("Neispravan format emaila,pokušajte ponovo.");

                foreach (var passenger in passengers)
                {

                    if (string.Equals(passenger.Email, email, StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine("Korisnik s tim mailom već postoji.\nAko želite ići na prijavu unesite 1,za odustajanje unesite 0, a za ponovni pokušaj registracije unesite bilo koji drugi broj");
                        {
                            input = Helper.ReadInt("");
                            if (input == '0')
                            {
                                PassengerLogIn(passengers, flights);
                                return;
                            }
                            else if (input == '1')
                            {
                                Console.WriteLine("Povratak u prethodni izbornik, pritisnite bilo koju tipku");
                                Console.ReadKey();
                                return;
                            }
                            else
                                email = null;
                        }

                    }
                }
            }
            while (password == null)
            {
                password =Helper.ReadNonEmpty("Unesite lozinku: ");
                if (password.Length < 8)
                {
                    Console.WriteLine("Lozinka mora imati min 8 znakova\n");
                    password = null;
                }
            }
            while (birthday == DateOnly.MinValue)
            {
                birthday = Helper.ReadDate("Unesi datum rođenja (yyyy-mm-dd): ");
                if (birthday.Year < 1925 || birthday.Year > 2025)
                {
                    Console.WriteLine("Unesite godinu veću od 1925 i manju od 2025");
                    birthday = DateOnly.MinValue;
                }
            }            
            Gender gender = ChooseGender();
            Console.Write($"Želite li nastaviti s registracijom korisnika {name} {surname}?(y/n)");
            var confirm = Helper.ReadChar("");
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
                email = Helper.ReadNonEmpty("Email: ");
                password = Helper.ReadNonEmpty("Lozinka: ");
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
                    input = Helper.ReadInt("");
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
                input = Helper.ReadInt("");
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
                input = Helper.ReadInt("");
                chosenFlight = availableFlights.FirstOrDefault(f => f.DisplayId == input);
                if (chosenFlight == null)
                {
                    Console.WriteLine("Let s tim ID-om ne postoji, ako želite odustati od odabira unesite 0,za nastavak unesite bilo koji drugi broj");
                    input = Helper.ReadInt("");
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
            choice = Helper.ReadChar("");
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
                choice = Helper.ReadChar("");
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
            int flightID = Helper.ReadInt("Unesite ID leta koji želite pretražiti:\n");

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
            string flightName = Helper.ReadNonEmpty("Unesite naziv leta koji želite pretražiti:\n");
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
                input = Helper.ReadInt("");
                chosenFlight =flights.FirstOrDefault(f => f.DisplayId == input);
                if (chosenFlight == null||!passenger.Flights.Any(f => f.FlightId == chosenFlight.Id))
                { 
                    Console.WriteLine("Let s tim ID-om nije na popisu, ako želite odustati od odabira unesite 0,za nastavak unesite bilo koji drugi broj");
                    input = Helper.ReadInt("");
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
            choice = Helper.ReadChar("");
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
        static void FlightMenu(List<Flight> flights,List <Airplane> airplanes,List<Crew> crews,List<Passenger> passengers)
        {
            int input = 0;
            while (input != 6)
            {
                Console.Clear();
                Console.WriteLine("Letovi:\r\n1. Prikaz svih letova\r\n2.Dodavanje leta\r\n3.Pretraživanje letova\r\n4.Uređivanje leta\r\n5.Brisanje leta\r\n6.Povratak na glavni izbornik ");
                input = Helper.ReadInt("");
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
                        EditFlight(flights,crews);
                        break;
                    case 5:
                        DeleteFlight(flights,passengers);
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
            departureTown = Helper.ReadNonEmpty("Unesite naziv grada polaska: ");
            arrivalTown = Helper.ReadNonEmpty("Unesite naziv grada u koji putujete: ");
            name = departureTown + " - " + arrivalTown;
            while (true)
            {
                departure = Helper.ReadDateTime("Unesite datum i vrijeme polaska u formatu (dd.MM.yyyy HH:mm) ");
                arrival = Helper.ReadDateTime("Unesite datum i vrijeme polaska u formatu (dd.MM.yyyy HH:mm)");

                if (arrival <= departure)
                    Console.WriteLine("Vrijeme dolaska mora biti nakon vremena polaska, pokušajte ponovo\n");
                else
                    break;
            }
            distance = Helper.ReadDouble("Unesite udaljenost u km: ");
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
                input = Helper.ReadInt("");
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
                input = Helper.ReadInt("");
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

                input = Helper.ReadChar("");
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
            int flightID = Helper.ReadInt("Pretraživanje leta po ID-u\n\nUnesite ID leta koji želite pretražiti:\n");

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
            string flightName = Helper.ReadNonEmpty("Pretraživanje leta po nazivu\n\nUnesite naziv leta koji želite pretražiti:\n");
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
        static void EditFlight(List<Flight> flights,List<Crew> crews)
        {
            Console.Clear();
            var input = 0;
            char? choice = null;
            input = Helper.ReadInt("Unesite ID leta kojeg želite urediti: ");
            var chosenFlight=flights.FirstOrDefault(f=>f.DisplayId == input);
            if (chosenFlight != null)
            {
                Console.WriteLine("Ne postoji let s tim ID-om, pritisnite bilo koju tipku za vraćanje u prethodni izbornik");
                Console.ReadKey();
                return;
            }
            Console.WriteLine($"Uređivanje leta {chosenFlight.Name}\r\na)Uredi vrijeme polaska\r\nb)Uredi vrijeme dolaska\r\nc)Promijeni posadud)Sve od navedenog0)Odustani");
            choice = Helper.ReadChar("");
            DateTime departure = chosenFlight.Departure;
            DateTime arrival=chosenFlight.Arrival;
            Crew crew = chosenFlight.Crew;
            while (choice != '0')
            {
                choice = Helper.ReadChar("");
                switch (choice)
                {
                    case 'a':
                        Console.WriteLine($"\nStaro vrijeme polaska: {chosenFlight.Departure}\nNovo vrijeme polaska: ");
                        departure = Helper.ReadDateTime("");
                        break;
                    case 'b':
                        Console.WriteLine($"\nStaro vrijeme dolaska: {chosenFlight.Arrival}\nNovo vrijeme dolaska: ");
                        arrival = Helper.ReadDateTime("");
                        break;
                    case 'c':
                       crew= EditFlightCrew(chosenFlight,crews);
                        break;
                    case 'd':
                        Console.WriteLine($"\nStaro vrijeme polaska: {chosenFlight.Departure}\nNovo vrijeme polaska: ");
                        departure = Helper.ReadDateTime("");
                        Console.WriteLine($"\nStaro vrijeme dolaska: {chosenFlight.Arrival}\nNovo vrijeme dolaska: ");
                        arrival = Helper.ReadDateTime("");
                        crew = EditFlightCrew(chosenFlight, crews);
                        break;
                    case '0':
                        break;
                    default:
                        Console.WriteLine("Pogrešan unos, pokušajte ponovo");
                        choice = null;
                        break;
                }
            }
            Console.WriteLine($"Želite li spremiti unesene promjene za let {chosenFlight}? (y/n)");
            choice = Helper.ReadChar("");
            if (choice != 'y')
            {
                Console.WriteLine("Promjene otkazane, pritisnite bilo koju tipku za povratak u prethodni izbornik");
                Console.ReadKey();
                return;
            }
            chosenFlight.Departure= departure;
            chosenFlight.Arrival= arrival;
            chosenFlight.Crew= crew;
            Console.WriteLine("Promjene uspješno spremljene, pritisnite bilo koju tipku za povratak u prethodni izbornik");
            Console.ReadKey();
        }
        static Crew  EditFlightCrew(Flight flight,List<Crew> crews)
        {
            Crew crew= null;
            var input = 0;
            Console.WriteLine($"\nStara posada: {flight.Crew.Name}");
            Console.WriteLine("\nPopis ostalih posada: ");

            Console.WriteLine("ID - Naziv posade");
            foreach(var c in crews)
            {
                if (flight.Crew == c)
                    break;
                Console.WriteLine($"{c.DisplayId} - {c.Name}");
            }
            Console.WriteLine("Unesite Id posade koju želite odabrati: ");
            while (true)
            {
                input = Helper.ReadInt("");
                crew = crews.FirstOrDefault(c => c.DisplayId == input);
                if(crew==null)
                {
                    Console.WriteLine("Ne postoji posada s tim ID-om, pokušajte ponovo");
                }
                else
                    break;
            }
            return crew;
        }
        static void DeleteFlight(List<Flight> flights,List<Passenger> passengers)
        {
            Console.Clear();
            var input = 0;
            char? choice = null;
            input = Helper.ReadInt("Unesite ID leta kojeg želite izbrisati: ");
            var chosenFlight = flights.FirstOrDefault(f => f.DisplayId == input);
            if (chosenFlight != null)
            {
                Console.WriteLine("Ne postoji let s tim ID-om, pritisnite bilo koju tipku za vraćanje u prethodni izbornik");
                Console.ReadKey();
                return;
            }
            TimeSpan timeUntilFlight = DateTime.Now - chosenFlight.Departure;
            var availableSeats = chosenFlight.getNumberofAllSeats();
            var totalSeats = 0;
            foreach (var seatCategory in chosenFlight.Airplane.Seat) 
            {
                totalSeats += seatCategory.Value;
            }
            if (availableSeats/totalSeats*100>=50)
            {
                Console.WriteLine("Let nije moguce otkazati jer je popunjenost veća od 50%, pritisnite bilo koju tipku za vraćanje u prethodni izbornik ");
                Console.ReadKey();
                return;
            }
            if(timeUntilFlight < TimeSpan.FromHours(24))
            {
                Console.WriteLine("Let nije moguce otkazati jer je polazak za manje od 24 sata, pritisnite bilo koju tipku za vraćanje u prethodni izbornik ");
                Console.ReadKey();
                return;
            }
            Console.WriteLine($"Jeste li sigurni da želite izbrisati let {chosenFlight.Name}? (y/n)");
            choice = Helper.    ReadChar("");
            if (choice != 'y') ;
                Console.WriteLine("Brisanje leta otkazano, povratak na prethodni izbornik");
            foreach(var passenger in passengers)
            {
                var removeFlight = passenger.Flights.FirstOrDefault(f => f.FlightId == chosenFlight.Id);
                if (removeFlight!=null)
                {
                    passenger.Flights.Remove(removeFlight);
                }
            }
            flights.Remove(chosenFlight);
            Console.WriteLine("Brisanje leta uspješno, pritisnite bilo koju tipku za povratak");
            Console.ReadKey();
        }
        static void AirplaneMenu(List<Airplane> airplanes,List<Flight> flights)
        {
            int input = 0;
            while (input != 6)
            {
                Console.Clear();
                Console.WriteLine("Letovi:\r\n1. Prikaz svih aviona\r\n2.Dodavanje novog aviona\r\n3.Pretraživanje aviona\r\n4.Brisanje aviona\r\n5.Povratak na glavni izbornik ");
                input = Helper.ReadInt("");
                switch (input)
                {
                    case 1:
                        AirplaneList(airplanes);
                        break;
                    case 2:
                        AddAirplane(airplanes);
                        break;
                    case 3:
                        SearchAirplane(airplanes);
                        break;
                    case 4:
                        DeleteAirplane(airplanes,flights);
                        break;
                    case 5:
                        Console.WriteLine("Povratak u glavni izbornik, pritisnite bilo koju tipku");
                        Console.ReadKey();
                        return;
                    default:
                        Console.WriteLine("Pogrešan unos, unesite neki od ponuđenih brojeva");
                        Console.ReadKey();
                        break;
                }
            }
        }
        static void AirplaneList(List<Airplane> airplanes)
        {
            Console.Clear();
            Console.WriteLine("ID - Naziv - Godina Proizvodnje - Broj letova");
            foreach (var airplane in airplanes)
            {
                airplane.Print();
            }
            Console.WriteLine("Za povratak u prethodni izbornik, pritisnite bilo koju tipku");
            Console.ReadKey();
        }
        static void AddAirplane(List<Airplane> airplanes)
        {
            Console.Clear();
            string name = null;
            int year = 0;
            var input = -1;
            int numberofFlights = 0;
            Dictionary<SeatCategory, int> seat = null;
            int numberOfCategories = 0;
            Airplane newAirplane=null;
            while (input != 0)
            {
                name = Helper.ReadNonEmpty("Unesite naziv aviona: ");
                foreach (var airplane in airplanes)
                {
                    if (string.Equals(airplane.Name, name, StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine("Uneseni naziv aviona već postoji, ako želite odustati unesite 0, za ponovan pokušaj unesite bilo koji drugi broj ");
                        input = Helper.ReadInt("");
                    }
                    else
                        input = 0;
                }
            }
            while (true)
            {
                year = Helper.ReadInt($"Unesite godinu proizvodnje aviona {name}: ");
                if (year > 2025 || year < 1910)
                {
                    Console.WriteLine("Unesena godina ne može biti manja od 1910 ni veća od 2025");
                    continue;
                }
                break;
            }
            numberofFlights = Helper.ReadInt("Unesite broj letova: ");
            while (true)
            {
                numberOfCategories = Helper.ReadInt("Koliko kategorija sjedala let posjeduje?: ");
                if (numberOfCategories > Enum.GetValues(typeof(SeatCategory)).Length)
                {
                    Console.WriteLine($"Ne postoji toliko kategorija, broj mora biti manji od {Enum.GetValues(typeof(SeatCategory)).Length}");
                    continue;
                }
                break;
            }
            char choice = ' ';
            Console.WriteLine($"Želite li dodati avion {name}? (y/n)");
            choice = Helper.ReadChar("");
            if (choice != 'y')
            {
                Console.WriteLine("Dodavanje otkazano, pritisnite bilo koju tipku za povratak u prethodni izbornik");
                Console.ReadKey();
                return;
            }
            seat = ChooseFlightSeats(numberOfCategories);
            newAirplane=new Airplane(name,year,numberofFlights,seat); 
            airplanes.Add(newAirplane);
            Console.WriteLine("Podaci uspješno spremljeni, povratak u prethodni izbornik,pritisnite bilo koju tipku");
            Console.ReadKey();

        }
        static Dictionary<SeatCategory, int> ChooseFlightSeats(int numberOfCategories)
        {
            int input = 1;
            var chosenCategories = new Dictionary<SeatCategory, int>();
            var listCategories = Enum.GetValues(typeof(SeatCategory)).Cast<SeatCategory>().ToList();
            for (int i = 0; i < numberOfCategories; i++)
            {
                Console.Clear();
                SeatCategory selectedCategory;
                while (true)
                {
                    Console.WriteLine("Dostupne kategorije:");
                    var availableCategories = listCategories.Except(chosenCategories.Keys).ToList();
                    for (int j = 0; j < availableCategories.Count; j++)
                    {
                        Console.WriteLine($"{j + 1}. {availableCategories[j]}");
                    }
                    input = Helper.ReadInt("Odaberite kategoriju, unesite ID: ");
                    if (input >= 1 && input <= availableCategories.Count)
                    {
                        selectedCategory = availableCategories[input - 1];
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Neispravan unos, pokušajte ponovo.");
                    }
                }
                input = Helper.ReadInt($"Unesite broj sjedala kategorije {selectedCategory}: ");
                chosenCategories.Add(selectedCategory, input);
            }
            return chosenCategories;
        }
        static void SearchAirplane(List<Airplane> airplanes)
        {
            Console.Clear();
            char input = ' ';
            Console.WriteLine("Pretraživanje aviona:\r\na)po ID-u\r\nb)po nazivu\r\n0)Povratak u prethodni izbornik");
            while (input != '0')
            {

                input = Helper.ReadChar("");
                switch (input)
                {
                    case 'a':
                        SearchAirplaneId(airplanes);
                        return;
                    case 'b':
                        SearchAirplaneName(airplanes);
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
        static void SearchAirplaneId(List<Airplane> airplanes)
        {
            Console.Clear();
            int airplaneID = Helper.ReadInt("Pretraživanje aviona po ID-u\n\nUnesite ID aviona koji želite pretražiti:\n");

            Airplane chosenAirplane = airplanes.FirstOrDefault(f => f.DisplayId == airplaneID);

            if (chosenAirplane != null)
            {
                Console.WriteLine("ID - Naziv - Godina Proizvodnje - Broj letova");
                chosenAirplane.Print();
                Console.WriteLine("\nKategorije sjedala:\r\n");
                foreach (var c in chosenAirplane.Seat)
                {
                    Console.WriteLine($"{(int)c.Key+1}.{c.Key} - broj sjedala: {c.Value}");
                }
            }
            else
            {
                Console.WriteLine("Ne postoji avion s tim ID-om");
            }
            Console.WriteLine("\nPritisnite bilo koju tipku za povratak u izbornik letova");
            Console.ReadKey();
        }
        static void SearchAirplaneName(List<Airplane> airplanes)
        {
            Console.Clear();
            string airplaneName = Helper.ReadNonEmpty("Pretraživanje aviona po nazivu\n\nUnesite naziv aviona koji želite pretražiti:\n");
            Airplane chosenAirplane = airplanes.FirstOrDefault(a=> string.Equals(airplaneName.Trim(), a.Name.Trim(), StringComparison.OrdinalIgnoreCase));
            if (chosenAirplane != null)
            {
                Console.WriteLine("ID - Naziv - Godina Proizvodnje - Broj letova");
                chosenAirplane.Print();
                Console.WriteLine("\nKategorije sjedala:\r\n");
                foreach (var c in chosenAirplane.Seat)
                {
                    Console.WriteLine($"{(int)c.Key+1}.{c.Key} - broj sjedala: {c.Value}");
                }
            }
            else
            {
                Console.WriteLine("Ne postoji avion s tim ID-om");
            }
            Console.WriteLine("\nPritisnite bilo koju tipku za povratak u prethodni izbornik");
            Console.ReadKey();

        }
        static void DeleteAirplane(List<Airplane> airplanes,List<Flight> flights)
        {
            Console.Clear();
            char input = ' ';
            Console.WriteLine("Brisanje aviona:\r\na)po ID-u\r\nb)po nazivu\r\n0)Povratak u prethodni izbornik");
            while (input != '0')
            {

                input = Helper.ReadChar("");
                switch (input)
                {
                    case 'a':
                        DeleteAirplaneId(airplanes,flights);
                        return;
                    case 'b':
                        DeleteAirplaneName(airplanes,flights);
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
        static void DeleteAirplaneId(List<Airplane> airplanes,List<Flight> flights)
        {
            Console.Clear();
            int airplaneID = Helper.ReadInt("Brisanje aviona po ID-u\n\nUnesite ID aviona koji želite izbrisati:\n");

            Airplane chosenAirplane = airplanes.FirstOrDefault(f => f.DisplayId == airplaneID);

            if (chosenAirplane != null)
            {
                Console.WriteLine($"Jeste li sigurni da želite izbrisati avion {chosenAirplane.Name}?(y/n)");
                char choice = ' ';
                choice = Helper.ReadChar("");
                if (choice != 'y')
                {
                    Console.WriteLine("Brisanje otkazano, pritisnite bilo koju tipku za povratak u prethodni izbornik");
                    Console.ReadKey();
                    return;
                }
                var availableAirplanes = airplanes.Where(a => a != chosenAirplane).ToList();
                foreach (var flight in flights.Where(f => f.Airplane == chosenAirplane))
                {
                    Console.Clear();
                    Console.WriteLine($"Odaberite avion koji će zamijeniti obrisani avion za let {flight.Name}\n");
                    flight.Airplane = ChooseAirplane(availableAirplanes);
                }
                airplanes.Remove(chosenAirplane);
                Console.WriteLine("Avion uspješno izbrisan");
            }
            else
            {
                Console.WriteLine("Ne postoji avion s tim ID-om");
            }
            Console.WriteLine("\nPritisnite bilo koju tipku za povratak u izbornik letova");
            Console.ReadKey();
        }
        static void DeleteAirplaneName(List<Airplane> airplanes, List<Flight> flights)
        {
            Console.Clear();
            string airplaneName = Helper.ReadNonEmpty("Brisanje aviona po nazivu\n\nUnesite naziv aviona koji želite izbrisati:\n");
            Airplane chosenAirplane = airplanes.FirstOrDefault(a => string.Equals(airplaneName.Trim(), a.Name.Trim(), StringComparison.OrdinalIgnoreCase));
            if (chosenAirplane != null)
            {
                Console.WriteLine($"Jeste li sigurni da želite izbrisati avion {chosenAirplane.Name}?(y/n)");
                char choice = ' ';
                choice = Helper.ReadChar("");
                if (choice != 'y')
                {
                    Console.WriteLine("Brisanje otkazano, pritisnite bilo koju tipku za povratak u prethodni izbornik");
                    Console.ReadKey();
                    return;
                }
                var availableAirplanes = airplanes.Where(a => a != chosenAirplane).ToList();
                foreach (var flight in flights.Where(f => f.Airplane == chosenAirplane))
                {
                    Console.Clear();
                    Console.WriteLine($"\nOdaberite avion koji će zamijeniti obrisani avion za let {flight.Name}\n\n");
                    flight.Airplane = ChooseAirplane(availableAirplanes);
                }
                airplanes.Remove(chosenAirplane);
                Console.Clear();
                Console.WriteLine("Avion uspješno izbrisan");
            }
            else
            {
                Console.WriteLine("Ne postoji avion s tim ID-om");
            }
            Console.WriteLine("\nPritisnite bilo koju tipku za povratak u prethodni izbornik");
            Console.ReadKey();

        }
        static void CrewMenu(List<Crew> crews,List<CrewMember> crewMembers)
        {
            int input = 0;
            while (input != 6)
            {
                Console.Clear();
                Console.WriteLine("Posada:\r\n1. Prikaz svih posada\r\n2.Kreiranje nove posade\r\n3.Dodavanje osobe\r\n5.Povratak na glavni izbornik ");
                input = Helper.ReadInt("");
                switch (input)
                {
                    case 1:
                        CrewList(crews);
                        break;
                    case 2:
                        NewCrew(crewMembers,crews);
                        break;
                    case 3:
                        NewCrewMember(crewMembers);
                        break;
                    case 4:
                        Console.WriteLine("Povratak u glavni izbornik, pritisnite bilo koju tipku");
                        Console.ReadKey();
                        return;
                    default:
                        Console.WriteLine("Pogrešan unos, unesite neki od ponuđenih brojeva");
                        Console.ReadKey();
                        break;
                }
            }
        }
        static void CrewList(List<Crew> crews)
        {
            Console.Clear();
            foreach(Crew c in crews)
            {
                Console.WriteLine("\n\n");
                c.print();
                Console.WriteLine("\nPodaci o članovima:\nIme - Prezime - Pozicija - Spol - Datum rođenja\n");
                c.Pilot.PrintInfo();
                c.Copilot.PrintInfo();
                c.Attendants[0].PrintInfo();
                c.Attendants[1].PrintInfo();
            }
            Console.WriteLine("Za povratak u prethodni izbornik pritisnite bilo koju tipku");
            Console.ReadKey();
        }
        static void NewCrew(List<CrewMember> crewMembers, List<Crew> crews)
        {
            string name = Helper.ReadNonEmpty("Unesite naziv nove posade: ");
            int input = 1;

            var availableMembers = crewMembers.Where(cm => !crews.Any(c => c.AllAttendants().Contains(cm))).ToList();

            var pilots = availableMembers.Where(p => p.Position == CrewPosition.Pilot).ToList();
            var copilots = availableMembers.Where(p => p.Position == CrewPosition.Copilot).ToList();
            var attendants = availableMembers.Where(p => p.Position == CrewPosition.Steward || p.Position == CrewPosition.Stewardess).ToList();
            if(pilots.Count==0|copilots.Count==0||attendants.Count<2)
            {
                Console.WriteLine("Nema dovoljno dostupnih članova za kreiranje posade, pritisnite bilo koju tipku za povratak u početni izbornik");
                Console.ReadKey();
                return;
            }
            foreach (var pilot in pilots)
            {
                Console.Write($"{pilot.DisplayId}.");
                pilot.PrintInfo();
            }

            CrewMember chosenPilot = null;
            while (chosenPilot == null)
            {
                input = Helper.ReadInt("Odaberi pilota:\n");
                chosenPilot = pilots.FirstOrDefault(p => p.DisplayId == input);
                if (chosenPilot == null)
                    Console.WriteLine("Pogrešan unos, unesite jedan od ID-a s ekrana");
            }

            foreach (var copilot in copilots)
            {
                Console.Write($"{copilot.DisplayId}.");
                copilot.PrintInfo();
            }

            CrewMember chosenCopilot = null;
            while (chosenCopilot == null)
            {
                input = Helper.ReadInt("Odaberi kopilota:\n");
                chosenCopilot = copilots.FirstOrDefault(p => p.DisplayId == input);
                if (chosenCopilot == null)
                    Console.WriteLine("Pogrešan unos, unesite jedan od ID-a s ekrana");
            }

            foreach (var attendant in attendants)
            {
                Console.Write($"{attendant.DisplayId}.");
                attendant.PrintInfo();
            }

            List<CrewMember> chosenAttendants = new List<CrewMember>();
            CrewMember firstAttendant = null;
            while (firstAttendant == null)
            {
                input = Helper.ReadInt("Odaberi stjuarda/stjuardesu:\n");
                firstAttendant = attendants.FirstOrDefault(p => p.DisplayId == input);
                if (firstAttendant == null)
                    Console.WriteLine("Pogrešan unos, unesite jedan od ID-a s ekrana");
            }
            chosenAttendants.Add(firstAttendant);

            CrewMember secondAttendant = null;
            while (secondAttendant == null)
            {
                input = Helper.ReadInt("Odaberi drugog stjuarda/stjuardesu:\n");
                secondAttendant = attendants.FirstOrDefault(p => p.DisplayId == input);
                if (secondAttendant == null)
                {
                    Console.WriteLine("Pogrešan unos, unesite jedan od ID-a s ekrana");
                    continue;
                }
                if (secondAttendant.DisplayId == firstAttendant.DisplayId)
                {
                    Console.WriteLine($"Člana {firstAttendant.DisplayId} ste već odabrali, unesite drugi ID");
                    secondAttendant = null;
                }
            }
            chosenAttendants.Add(secondAttendant);

            char choice = Helper.ReadChar($"Želite li nastaviti kreiranje posade {name}? (y/n): ");
            if (choice != 'y')
            {
                Console.WriteLine("Kreiranje posade otkazano, pritisnite bilo koju tipku za povratak u prethodni izbornik");
                Console.ReadKey();
                return;
            }

            var newCrew = new Crew(name, chosenPilot, chosenCopilot, chosenAttendants);
            crews.Add(newCrew);
            Console.WriteLine("Posada uspješno kreirana!");
            Console.ReadKey();
        }

        static void NewCrewMember(List<CrewMember> crewMembers)
        {
        }
    }
}
