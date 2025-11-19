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
               new Passenger("Andrea","Vukadin","andrea.vukadin@gmail.com","andrealozinka",new DateOnly(2001,10,07),Gender.Female)
            };

            List<Airplane> airplanes = new List<Airplane>()
            {
                new Airplane("Airbus A320", 2015, 842, new Dictionary<SeatCategory,int>{{SeatCategory.Economy,150},{SeatCategory.Business,18},{SeatCategory.FirstClass,0}}),
                new Airplane("Boeing 737-800", 2012, 1094, new Dictionary<SeatCategory,int>{{SeatCategory.Economy,162},{SeatCategory.Business,12},{SeatCategory.FirstClass,0}}),
                new Airplane("Airbus A330-300", 2018, 540, new Dictionary<SeatCategory,int>{{SeatCategory.Economy,250},{SeatCategory.Business,36},{SeatCategory.FirstClass,8}}),
                new Airplane("Boeing 777-300ER", 2020, 320, new Dictionary<SeatCategory,int>{{SeatCategory.Economy,300},{SeatCategory.Business,40},{SeatCategory.FirstClass,8}}),
                new Airplane("Airbus A350-900", 2019, 410, new Dictionary<SeatCategory,int>{{SeatCategory.Economy,260},{SeatCategory.Business,48},{SeatCategory.FirstClass,12}})
            };
            List<Flight> flights = new List<Flight>()
            {
                new Flight("St-Zg",new DateTime(2025,12,23,15,30,00),new DateTime(2025,12,23,16,30,00),260,new TimeOnly(1,0),airplanes[0]),
                new Flight("Split - Dubrovnik",new DateTime(2026, 8, 2, 6, 20, 0),new DateTime(2025, 8, 2, 7, 05, 0),165,new TimeOnly(0, 45),airplanes[2]),
                new Flight("Split - Frankfurt",new DateTime(2025, 11, 5, 13, 10, 0),new DateTime(2025, 11, 5, 15, 15, 0),950,new TimeOnly(2, 5),airplanes[1]),
                new Flight("Split - London",new DateTime(2025, 4, 12, 17, 55, 0),new DateTime(2025, 4, 12, 20, 35, 0),1550,new TimeOnly(2, 40),airplanes[3])
            };
            passengers[0].AddFlights(flights[0].Id);
            passengers[1].AddFlights(flights[3].Id);
            passengers[2].AddFlights(flights[1].Id);
            passengers[2].AddFlights(flights[2].Id);

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
                        FlightList(passenger, flights);
                        break;
                    case 2:
                        ReserveFlightList(passenger, flights);
                        break;
                    case 5:
                        Console.WriteLine("Povratak u glavni izbornik, pritisnite bilo koju tipku");
                        Console.ReadKey();
                        return;
                }
            }
        }
        static void FlightList(Passenger passenger, List<Flight> flights)
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
        static void ReserveFlightList(Passenger passenger, List<Flight> flights)
        {
            Console.Clear();
            var input = -1;
            int counter = 0;
            Flight chosenFlight=null;
            List<Flight> availableFlights=null;
            foreach(var flight in flights)
            {
                int availableSeats = flight.getNumberofAllSeats();
                if (availableSeats > 0 && flight.DepartureTime > DateTime.Now)
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
            Console.WriteLine("ID - Naziv - Datum polaska - Datum dolaska - Udaljenost - Vrijeme putovanja\n");

            foreach (var flight in availableFlights)
                    flight.Print();

            Console.WriteLine("Unesite ID leta koji želite rezervirati,ako želite odustati unesite 0: ");
            while (input != 0)
            {
                input = ReadInt("");
                chosenFlight = flights.FirstOrDefault(f => f.DisplayId == input);
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
            chooseCategory(passenger,chosenFlight);
        }
        static void chooseCategory(Passenger passenger,Flight flight)
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
                Console.WriteLine("Rezervacija je uspješna");
                Thread.Sleep(2000);
            }
            else
            {
                Console.WriteLine("Odustajanje od rezerviranja");
                return;
            }
        }
        static double ReadDouble(string message)
        {
            Console.Write(message);
            double number;

            while (!double.TryParse(Console.ReadLine(), out number))
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
