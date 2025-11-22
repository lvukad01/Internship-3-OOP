

namespace Internship_3_OOP
{
     internal class AirplaneMenu
    {
        public static void View(List<Airplane> airplanes, List<Flight> flights)
        {
            int input = 0;
            while (input != 5)
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
                        DeleteAirplane(airplanes, flights);
                        break;
                    case 5:
                        Console.WriteLine("Povratak u glavni izbornik, pritisnite bilo koju tipku");
                        Console.ReadKey();
                        break;
                    default:
                        Console.WriteLine("Pogrešan unos, unesite neki od ponuđenih brojeva");
                        Console.ReadKey();
                        break;
                }
            }
        }
        public static void AirplaneList(List<Airplane> airplanes)
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
        public static void AddAirplane(List<Airplane> airplanes)
        {
            Console.Clear();
            string name = null;
            int year = 0;
            var input = -1;
            int numberofFlights = 0;
            Dictionary<SeatCategory, int> seat = null;
            int numberOfCategories = 0;
            Airplane newAirplane = null;
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
            newAirplane = new Airplane(name, year, numberofFlights, seat);
            airplanes.Add(newAirplane);
            Console.WriteLine("Podaci uspješno spremljeni, povratak u prethodni izbornik,pritisnite bilo koju tipku");
            Console.ReadKey();

        }
        public static Dictionary<SeatCategory, int> ChooseFlightSeats(int numberOfCategories)
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
        public static void SearchAirplane(List<Airplane> airplanes)
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
        public static void SearchAirplaneId(List<Airplane> airplanes)
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
                    Console.WriteLine($"{(int)c.Key + 1}.{c.Key} - broj sjedala: {c.Value}");
                }
                Console.WriteLine($"\n\nAvion dodan: {chosenAirplane.Created}\nZadnje ažuriranje: {chosenAirplane.Updated}");
            }
            else
            {
                Console.WriteLine("Ne postoji avion s tim ID-om");
            }
            Console.WriteLine("\nPritisnite bilo koju tipku za povratak u izbornik letova");
            Console.ReadKey();
        }
        public static void SearchAirplaneName(List<Airplane> airplanes)
        {
            Console.Clear();
            string airplaneName = Helper.ReadNonEmpty("Pretraživanje aviona po nazivu\n\nUnesite naziv aviona koji želite pretražiti:\n");
            Airplane chosenAirplane = airplanes.FirstOrDefault(a => string.Equals(airplaneName.Trim(), a.Name.Trim(), StringComparison.OrdinalIgnoreCase));
            if (chosenAirplane != null)
            {
                Console.WriteLine("ID - Naziv - Godina Proizvodnje - Broj letova");
                chosenAirplane.Print();
                Console.WriteLine("\nKategorije sjedala:\r\n");
                foreach (var c in chosenAirplane.Seat)
                {
                    Console.WriteLine($"{(int)c.Key + 1}.{c.Key} - broj sjedala: {c.Value}");
                }
                Console.WriteLine($"\n\nAvion dodan: {chosenAirplane.Created}\nZadnje ažuriranje: {chosenAirplane.Updated}");
            }
            else
            {
                Console.WriteLine("Ne postoji avion s tim ID-om");
            }
            Console.WriteLine("\nPritisnite bilo koju tipku za povratak u prethodni izbornik");
            Console.ReadKey();

        }
        public static void DeleteAirplane(List<Airplane> airplanes, List<Flight> flights)
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
                        DeleteAirplaneId(airplanes, flights);
                        return;
                    case 'b':
                        DeleteAirplaneName(airplanes, flights);
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
        public static void DeleteAirplaneId(List<Airplane> airplanes, List<Flight> flights)
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
                    flight.Airplane = ChooseHelper.ChooseAirplane(availableAirplanes);
                    flight.Update();
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
        public static void DeleteAirplaneName(List<Airplane> airplanes, List<Flight> flights)
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
                    flight.Airplane = ChooseHelper.ChooseAirplane(availableAirplanes);
                    flight.Update();
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
    }
}
