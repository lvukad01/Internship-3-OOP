using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internship_3_OOP
{
    internal static class PassengerMenu
    {
       public static void View(List<Passenger> passengers, List<Flight> flights)
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
                        RegisterPassenger(passengers, flights);
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
        public static void RegisterPassenger(List<Passenger> passengers, List<Flight> flights)
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
                {
                    Console.WriteLine("Neispravan format emaila,pokušajte ponovo.");
                    email = null;
                    continue;
                }
                foreach (var passenger in passengers)
                {

                    if (string.Equals(passenger.Email, email, StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine("Korisnik s tim mailom već postoji.\nAko želite ići na prijavu unesite 1,za odustajanje unesite 0, a za ponovni pokušaj registracije unesite bilo koji drugi broj");
                        {
                            input = Helper.ReadInt("");
                            if (input == 1)
                            {
                                PassengerLogIn(passengers, flights);
                                return;
                            }
                            else if (input == 0)
                            {
                                Console.WriteLine("Povratak u prethodni izbornik, pritisnite bilo koju tipku");
                                Console.ReadKey();
                                return;
                            }
                            else
                            {
                                email = null;
                                break;
                            }
                        }

                    }
                }
            }
            while (password == null)
            {
                password = Helper.ReadNonEmpty("Unesite lozinku: ");
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
            Gender gender = ChooseHelper.ChooseGender();
            Console.Write($"Želite li nastaviti s registracijom korisnika {name} {surname}?(y/n)");
            var confirm = Helper.ReadChar("");
            confirm = char.ToLower(confirm);
            if (confirm != 'y')
            {
                Console.WriteLine("Registracija otkazana, pritisnite bilo koju tipku za povratak u prethodni izbornik.");
                Console.ReadKey();
                return;

            }
            Passenger newPassenger = new Passenger(name, surname, email, password, birthday, gender);
            passengers.Add(newPassenger);
            Console.WriteLine("Registracija dovršena, pritisnite bilo koju tipku za povratak u prethodni izbornik.");
            Console.ReadKey();
            return;
        }
        
        public static void PassengerLogIn(List<Passenger> passengers, List<Flight> flights)
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
        public static void PassengerFlightList(Passenger passenger, List<Flight> flights)
        {
            Console.Clear();
            if (passenger.Flights.Count == 0)
            {
                Console.WriteLine("Korisnik nema rezerviranih letova, za povratak u prethodni izbornik pritisnite bilo koju tipku");
                Console.ReadKey();
                return;
            }
            Console.WriteLine("ID - Naziv - Datum polaska - Datum dolaska - Udaljenost - Vrijeme putovanja\n");
            foreach (var Id in passenger.Flights)
            {
                var flight = flights.FirstOrDefault(f => f.Id == Id.FlightId);
                flight.Print();
            }
            Console.WriteLine("\n\nZa povratak pritisnite bilo koju tipku");
            Console.ReadKey();
        }
        public static void ReserveFlight(Passenger passenger, List<Flight> flights)
        {
            Console.Clear();
            var input = -1;
            int counter = 0;
            Flight chosenFlight = null;
            List<Flight> availableFlights = new List<Flight>();
            foreach (var flight in flights)
            {
                int availableSeats = flight.getNumberofAllSeats();
                if (availableSeats > 0 && flight.Departure > DateTime.Now)
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
                else if (passenger.Flights.Any(f => f.FlightId == chosenFlight.Id))
                {
                    Console.WriteLine("Taj let ste već rezervirali");
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
            ChooseCategory(passenger, chosenFlight);
        }
        public static void ChooseCategory(Passenger passenger, Flight flight)
        {
            int input = -1;
            char choice = '0';
            int freeSeats = 0;
            Console.WriteLine("Odaberite dostupnu kategoriju sjedala koje želite rezervirati");

            foreach (var category in flight.Airplane.Seat)
            {
                freeSeats = flight.getNumberofSeats(category.Key);
                if (freeSeats > 0)
                {
                    Console.WriteLine($"{(int)category.Key + 1}.{category.Key}- broj dostupnih sjedala: {freeSeats}");
                }
            }
            while (!int.TryParse(Console.ReadLine(), out input) || !Enum.IsDefined(typeof(SeatCategory), input - 1))
            {
                Console.WriteLine("Neispravan unos, unesite ID kategorije:");
            }
            SeatCategory chosenSeat = (SeatCategory)(input - 1);
            Console.WriteLine($"Jeste li sigurni da želite rezervirati sjedalo kategorije {chosenSeat}?(y/n)");
            choice = Helper.ReadChar("");
            Reservation reserve = new Reservation(flight.Id, chosenSeat);
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
        public static void PassengerSearchFlight(Passenger passenger, List<Flight> flights)
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
                        PassengerSearchFlightId(passenger, flights);
                        break;
                    case 'b':
                        PassengerSearchFlightName(passenger, flights);
                        break;
                    default:
                        Console.WriteLine("Pogrešan unos, unesite a ili b,pritisnite bilo koju tipku pa pokušajte ponovo");
                        choice = '0';
                        Console.ReadKey();
                        break;
                }
            }
        }
       public static void PassengerSearchFlightId(Passenger passenger, List<Flight> flights)
        {
            int flightID = Helper.ReadInt("Unesite ID leta koji želite pretražiti:\n");
            var passengerFlightIds = passenger.Flights.Select(r => r.FlightId).ToList();
            Flight chosenFlight = flights.FirstOrDefault(f => f.DisplayId == flightID && passengerFlightIds.Contains(f.Id));

            if (chosenFlight != null)
            {
                Console.WriteLine("ID - Naziv - Datum i vrijeme polaska - Datum i vrijeme dolaska - Udaljenost - Vrijeme putovanja\n");
                chosenFlight.Print();
            }
            else
            {
                Console.WriteLine("Korisnik nema rezervaciju za let s tim ID-om");
            }
            Console.WriteLine("Pritisnite bilo koju tipku za povratak u prethodni izbornik");
            Console.ReadKey();

        }
        public static void PassengerSearchFlightName(Passenger passenger, List<Flight> flights)
        {
            string flightName = Helper.ReadNonEmpty("Unesite naziv leta koji želite pretražiti:\n");
            var passengerFlightIds = passenger.Flights.Select(r => r.FlightId).ToList();
            Flight chosenFlight = flights.FirstOrDefault(f => string.Equals(flightName.Trim(), f.Name.Trim(), StringComparison.OrdinalIgnoreCase) && passengerFlightIds.Contains(f.Id));
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
        public static void PassengerCancelFlight(Passenger passenger, List<Flight> flights)
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
                chosenFlight = flights.FirstOrDefault(f => f.DisplayId == input);

                if (input == 0)
                {
                    Console.WriteLine("Povratak na prethodni izbornik, pritisnite bilo koju tipku");
                    Console.ReadKey();
                    return;
                }
                else if (chosenFlight == null || !passenger.Flights.Any(f => f.FlightId == chosenFlight.Id))
                {
                    Console.WriteLine("Let s tim ID-om nije na popisu, ako želite odustati od odabira unesite 0,za nastavak unesite bilo koji drugi broj");
                    input = Helper.ReadInt("");
                    if (input == 0)
                    {
                        Console.WriteLine("Vraćanje u prethodni izbornik, pritisnite bilo koju tipku");
                        Console.ReadKey();
                        return;
                    }
                    else
                        input = 0;
                }
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
    }
}
