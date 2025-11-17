using System.Globalization;

namespace Internship_3_OOP
{
    internal class Program
    {
        static void Main()
        {
            List<Passenger> passengers = new List<Passenger>()
            {
               new Passenger("Lana","Vukadin","lana.vukadin@gmail.com","lanalozinka",new DateOnly(2005,10,03),Gender.Female),
               new Passenger("Arijana","Radeljak","arijana.radeljak@gmail.com","arijanalozinka",new DateOnly(2005,10,02),Gender.Female),
               new Passenger("Andrea","Vukadin","andrea.vukadin@gmail.com","andrealozinka",new DateOnly(2001,10,07),Gender.Female)
            };
            List<Flight> flights = new List<Flight>()
            {
                new Flight("St-Zg",new DateTime(2025,12,23,15,30,00),new DateTime(2025,12,23,16,30,00),260,new TimeOnly(1,0)),
                new Flight("Split - Dubrovnik",new DateTime(2025, 8, 2, 6, 20, 0),new DateTime(2025, 8, 2, 7, 05, 0),165,new TimeOnly(0, 45)),
                new Flight("Split - Frankfurt",new DateTime(2025, 11, 5, 13, 10, 0),new DateTime(2025, 11, 5, 15, 15, 0),950,new TimeOnly(2, 5)),
                new Flight("Split - London",new DateTime(2025, 4, 12, 17, 55, 0),new DateTime(2025, 4, 12, 20, 35, 0),1550,new TimeOnly(2, 40))
            };
            passengers[0].AddFlights(flights[0].Id);
            passengers[1].AddFlights(flights[3].Id);
            passengers[2].AddFlights(flights[1].Id);
            passengers[2].AddFlights(flights[2].Id);

            int input = 0;
            Console.WriteLine("KONZOLNA APLIKACIJA ZA UPRAVLJANJE AERODROMOM\r\n");
            Console.WriteLine("1 - Putnici\r\n2 - Letovi\r\n3 – Avioni\r\n4 – Posada\r\n5 – Izlaz iz programa\r\n");
            while(input!=5)
            {
                while (!int.TryParse(Console.ReadLine(), out input))
                {
                    Console.WriteLine("Neispravan unos, unesite broj:");
                }
                switch (input)
                {
                    case 1:
                        PassengerMenu(passengers,flights);
                        break;
                    default:
                        break;
                        
                }
            }
           
        }
        static void PassengerMenu(List<Passenger> passengers,List<Flight> flights)
        {
            Console.WriteLine("PUTNICI\r\n1-Registracija\r\n2-Prijava\r\n0-Povratak u glavni izbornik");
            int input = -1;
            while(input!=0)
            {
                while (!int.TryParse(Console.ReadLine(), out input))
                {
                    Console.WriteLine("Neispravan unos, unesite broj:");
                }
                switch (input)
                {
                    case 1:
                        RegisterPassenger(passengers);
                        break;
                    default:
                        Console.WriteLine("pogresan unos");
                        break;
                }
            }

        }
        static void RegisterPassenger(List<Passenger> passengers)
        {
            string name = null, surname = null, email = null, password = null;
            int input = 0;
            DateOnly birthday = DateOnly.MinValue;
            try
            {
                Console.Write("Unesi ime: ");
                name = Console.ReadLine();

                Console.Write("Unesi prezime: ");
                surname = Console.ReadLine();
                while (email == null)
                {
                    Console.Write("Unesite email: ");
                    email = Console.ReadLine();
                    foreach (var passenger in passengers)
                    {
                        if (passenger.Email == email)
                        {
                            Console.WriteLine("Korisnik s tim mailom već postoji.\nAko želite odustati od radnje unesite 0,za promjenu unesenog maila unesite bilo koji drugi broj");
                            {
                                while (!int.TryParse(Console.ReadLine(), out input))
                                {
                                    Console.WriteLine("Neispravan unos, unesite broj:");
                                }
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
                    Console.Write("Unesite lozinku: ");
                    password = Console.ReadLine();
                    if (password.Length < 8)
                    {
                        Console.WriteLine("Lozinka mora imati min 8 znakova\n");
                        password = null;
                    }
                }
                Console.Write("Unesi datum rođenja (yyyy-mm-dd): ");
                while (!DateOnly.TryParse(Console.ReadLine(), out birthday))
                {
                    Console.WriteLine("Neispravan datum, unesite u formatu yyyy-MM-dd:");
                }
                if (birthday.Year < 1925 || birthday.Year > 2025)
                {
                    Console.WriteLine("Unesite godinu veću od 1925 i manju od 2025");
                    birthday = DateOnly.MinValue;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Greška: " + ex.Message);
                return;
            }
            Gender gender = ChooseGender();
            Console.Write("Želite li nastaviti s registracijom?(y/n)");
            char confirm = char.Parse(Console.ReadLine());
            confirm = char.ToLower(confirm);
            if (confirm != 'y')
            {
                Console.WriteLine("Registracija otkazana.");
                Thread.Sleep(2000);
                return;

            }
            Passenger p=new Passenger(name,surname,email,password,birthday,gender);
            passengers.Add(p);
            Console.WriteLine("Registracija dovršena");
            Thread.Sleep(2000);
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
    }
}
