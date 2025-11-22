

namespace Internship_3_OOP
{
    internal static class FlightMenu
    {
        public static void View(List<Flight> flights, List<Airplane> airplanes, List<Crew> crews, List<Passenger> passengers)
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
                        FlightList(flights);
                        break;
                    case 2:
                        AddFlight(flights, airplanes, crews);
                        break;
                    case 3:
                        SearchFlight(flights);
                        break;
                    case 4:
                        EditFlight(flights, crews);
                        break;
                    case 5:
                        DeleteFlight(flights, passengers);
                        break;
                    case 6:
                        Console.WriteLine("Povratak u glavni izbornik, pritisnite bilo koju tipku");
                        Console.ReadKey();
                        return;
                }
            }
        }
        public static void FlightList(List<Flight> flights)
        {
            Console.Clear();
            Console.WriteLine("\nID - Naziv - Datum i vrijeme polaska - Datum i vrijeme dolaska - Udaljenost - Vrijeme putovanja\n");
            foreach (var flight in flights)
            {
                flight.Print();
            }
            Console.WriteLine("\nZa povratak u prethodni izbornik pritisnite bilo koju tipku");
            Console.ReadKey();
        }
        public static void AddFlight(List<Flight> flights, List<Airplane> airplanes, List<Crew> crews)
        {
            Console.Clear();
            string departureTown = null;
            string arrivalTown = null;
            string name = null;
            DateTime departure = DateTime.MinValue;
            DateTime arrival = DateTime.MinValue;
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
            chosenAirplane = ChooseHelper.ChooseAirplane(airplanes);
            chosenCrew = ChooseCrew(crews);
            addFlight = new Flight(name, departure, arrival, distance, chosenAirplane, chosenCrew);
            flights.Add(addFlight);
            Console.WriteLine("Let uspješno dodan, za povratak na prethodni izbornik pritisnite bilo koju tipku");
            Console.ReadKey();

        }

        public static Crew ChooseCrew(List<Crew> crews)
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
                chosenCrew = crews.FirstOrDefault(c => c.DisplayId == input);
                if (chosenCrew == null)
                {
                    Console.WriteLine("Ne postoji posada s unesenim ID-om, pokušajte ponovo");
                    input = 0;
                }
            }
            return chosenCrew;
        }
        public static void SearchFlight(List<Flight> flights)
        {
            Console.Clear();
            char input = ' ';
            Console.WriteLine("Pretraživanje leta:\r\na)po ID-u\r\nb)po nazivu\r\n0)Povratak u prethodni izbornik");
            while (input != '0')
            {

                input = Helper.ReadChar("");
                switch (input)
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
        public static void SearchFlightId(List<Flight> flights)
        {
            Console.Clear();
            int flightID = Helper.ReadInt("Pretraživanje leta po ID-u\n\nUnesite ID leta koji želite pretražiti:\n");

            Flight chosenFlight = flights.FirstOrDefault(f => f.DisplayId == flightID);

            if (chosenFlight != null)
            {
                Console.Clear();
                Console.WriteLine("Ispis svih podataka odabranog leta:\nID - Naziv - Datum i vrijeme polaska - Datum i vrijeme dolaska - Udaljenost - Vrijeme putovanja");
                chosenFlight.Print();
                Console.WriteLine($"\nAvion: {chosenFlight.Airplane.Name}");
                Console.WriteLine($"Posada: {chosenFlight.Crew.Name}\nVrijeme dodavanja leta: {chosenFlight.Created}\nZadnji put uređivan: {chosenFlight.Updated}");

            }
            else
            {
                Console.WriteLine("Ne postoji let s tim ID-om");
            }
            Console.WriteLine("Pritisnite bilo koju tipku za povratak u izbornik letova");
            Console.ReadKey();
        }
       public static void SearchFlightName(List<Flight> flights)
        {
            Console.Clear();
            string flightName = Helper.ReadNonEmpty("Pretraživanje leta po nazivu\n\nUnesite naziv leta koji želite pretražiti:\n");
            Flight chosenFlight = flights.FirstOrDefault(f => string.Equals(flightName.Trim(), f.Name.Trim(), StringComparison.OrdinalIgnoreCase));
            if (chosenFlight != null)
            {
                Console.WriteLine("\nID - Naziv - Datum i vrijeme polaska - Datum i vrijeme dolaska - Udaljenost - Vrijeme putovanja");
                chosenFlight.Print();
                Console.WriteLine($"\nAvion: {chosenFlight.Airplane.Name}");
                Console.WriteLine($"Posada: {chosenFlight.Crew.Name}\nVrijeme dodavanja leta: {chosenFlight.Created}\nZadnji put uređivan: {chosenFlight.Updated}");
            }
            else
            {
                Console.WriteLine("Ne postoji let s tim ID-om");
            }
            Console.WriteLine("Pritisnite bilo koju tipku za povratak u prethodni izbornik");
            Console.ReadKey();

        }
        public static void EditFlight(List<Flight> flights, List<Crew> crews)
        {
            Console.Clear();
            var input = 0;
            char? choice = null;
            input = Helper.ReadInt("Unesite ID leta kojeg želite urediti: ");
            var chosenFlight = flights.FirstOrDefault(f => f.DisplayId == input);
            if (chosenFlight == null)
            {
                Console.WriteLine("Ne postoji let s tim ID-om, pritisnite bilo koju tipku za vraćanje u prethodni izbornik");
                Console.ReadKey();
                return;
            }
            Console.WriteLine($"Uređivanje leta {chosenFlight.Name}\r\na)Uredi vrijeme polaska\r\nb)Uredi vrijeme dolaska\r\nc)Promijeni posadu\r\nd)Sve od navedeno\r\n0)Odustani");
            choice = Helper.ReadChar("");
            DateTime departure = chosenFlight.Departure;
            DateTime arrival = chosenFlight.Arrival;
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
                        crew = EditFlightCrew(chosenFlight, crews);
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
            chosenFlight.Departure = departure;
            chosenFlight.Arrival = arrival;
            chosenFlight.Crew = crew;
            chosenFlight.Update();
            Console.WriteLine("Promjene uspješno spremljene, pritisnite bilo koju tipku za povratak u prethodni izbornik");
            Console.ReadKey();
        }
        public static Crew EditFlightCrew(Flight flight, List<Crew> crews)
        {
            Crew crew = null;
            var input = 0;
            Console.WriteLine($"\nStara posada: {flight.Crew.Name}");
            Console.WriteLine("\nPopis ostalih posada: ");

            Console.WriteLine("ID - Naziv posade");
            foreach (var c in crews)
            {
                if (flight.Crew == c)
                    continue;
                Console.WriteLine($"{c.DisplayId} - {c.Name}");
            }
            Console.WriteLine("Unesite Id posade koju želite odabrati: ");
            while (true)
            {
                input = Helper.ReadInt("");
                crew = crews.FirstOrDefault(c => c.DisplayId == input);
                if (crew == null)
                {
                    Console.WriteLine("Ne postoji posada s tim ID-om, pokušajte ponovo");
                }
                else
                    break;
            }
            return crew;
        }
        public static void DeleteFlight(List<Flight> flights, List<Passenger> passengers)
        {
            Console.Clear();
            var input = 0;
            char? choice = null;
            input = Helper.ReadInt("Unesite ID leta kojeg želite izbrisati: ");
            var chosenFlight = flights.FirstOrDefault(f => f.DisplayId == input);
            if (chosenFlight == null)
            {
                Console.WriteLine("Ne postoji let s tim ID-om, pritisnite bilo koju tipku za vraćanje u prethodni izbornik");
                Console.ReadKey();
                return;
            }
            TimeSpan timeUntilFlight = chosenFlight.Departure - DateTime.Now;
            var availableSeats = chosenFlight.getNumberofAllSeats();
            var totalSeats = 0;
            foreach (var seatCategory in chosenFlight.Airplane.Seat)
            {
                totalSeats += seatCategory.Value;
            }
            if ((double)(availableSeats / totalSeats) * 100 >= 50)
            {
                Console.WriteLine("Let nije moguce otkazati jer je popunjenost veća od 50%, pritisnite bilo koju tipku za vraćanje u prethodni izbornik ");
                Console.ReadKey();
                return;
            }
            if (timeUntilFlight < TimeSpan.FromHours(24))
            {
                Console.WriteLine("Let nije moguce otkazati jer je polazak za manje od 24 sata, pritisnite bilo koju tipku za vraćanje u prethodni izbornik ");
                Console.ReadKey();
                return;
            }
            Console.WriteLine($"Jeste li sigurni da želite izbrisati let {chosenFlight.Name}? (y/n)");
            choice = Helper.ReadChar("");
            if (choice != 'y');
            Console.WriteLine("Brisanje leta otkazano, povratak na prethodni izbornik");
            foreach (var passenger in passengers)
            {
                var removeFlight = passenger.Flights.FirstOrDefault(f => f.FlightId == chosenFlight.Id);
                if (removeFlight != null)
                {
                    passenger.Flights.Remove(removeFlight);
                }
            }
            flights.Remove(chosenFlight);
            Console.WriteLine("Brisanje leta uspješno, pritisnite bilo koju tipku za povratak");
            Console.ReadKey();
        }
    }
}
