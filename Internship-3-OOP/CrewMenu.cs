

namespace Internship_3_OOP
{
    internal class CrewMenu
    {
        public static void View(List<Crew> crews, List<CrewMember> crewMembers)
        {
            int input = 0;
            while (input != 4)
            {
                Console.Clear();
                Console.WriteLine("Posada:\r\n1. Prikaz svih posada\r\n2.Kreiranje nove posade\r\n3.Dodavanje osobe\r\n4.Povratak na glavni izbornik ");
                input = Helper.ReadInt("");
                switch (input)
                {
                    case 1:
                        CrewList(crews);
                        break;
                    case 2:
                        NewCrew(crewMembers, crews);
                        break;
                    case 3:
                        NewCrewMember(crewMembers);
                        break;
                    case 4:
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
        public static void CrewList(List<Crew> crews)
        {
            Console.Clear();
            foreach (Crew c in crews)
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
        public static void NewCrew(List<CrewMember> crewMembers, List<Crew> crews)
        {
            string name = Helper.ReadNonEmpty("Unesite naziv nove posade: ");
            int input = 1;

            var availableMembers = crewMembers.Where(cm => !crews.Any(c => c.AllAttendants().Contains(cm))).ToList();

            var pilots = availableMembers.Where(p => p.Position == CrewPosition.Pilot).ToList();
            var copilots = availableMembers.Where(p => p.Position == CrewPosition.Copilot).ToList();
            var attendants = availableMembers.Where(p => p.Position == CrewPosition.Steward || p.Position == CrewPosition.Stewardess).ToList();
            if (pilots.Count == 0 | copilots.Count == 0 || attendants.Count < 2)
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

       public static void NewCrewMember(List<CrewMember> crewMembers)
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

                foreach (var crewMember in crewMembers)
                {

                    if (string.Equals(crewMember.Email, email, StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine("Član s tim mailom već postoji.\nAko želite odustati unesite 0, za ponovni pokušaj registracije unesite bilo koji drugi broj");
                        {
                            input = Helper.ReadInt("");
                            if (input == 0)
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
            CrewPosition position = CrewPosition.Steward;
            Console.WriteLine("Odaberite poziciju člana:\r\n1.Pilot\r\n2.Copilot\r\n3.Stjuard/Stjuardesa");
            while (input == 0)
            {
                input = Helper.ReadInt("");
                switch (input)
                {
                    case 1:
                        position = CrewPosition.Pilot;
                        break;
                    case 2:
                        position = CrewPosition.Copilot;
                        break;
                    case 3:
                        if (gender == Gender.Female)
                            position = CrewPosition.Stewardess;
                        break;
                    default:
                        Console.WriteLine("Pogrešan unos, unesite 1,2 ili 3");
                        input = 0;
                        break;
                }
            }
            Console.Write($"Želite li nastaviti s dodavanjem člana {name} {surname}?(y/n)");
            var confirm = Helper.ReadChar("");
            confirm = char.ToLower(confirm);
            if (confirm != 'y')
            {
                Console.WriteLine("Dodavaje člana otkazano, pritisnite bilo koju tipku za povratak u prethodni izbornik.");
                Console.ReadKey();
                return;

            }
            CrewMember newCrewMember = new CrewMember(name, surname, email, password, birthday, gender, position);
            crewMembers.Add(newCrewMember);
            Console.WriteLine("Registracija dovršena, pritisnite bilo koju tipku za povratak u prethodni izbornik.");
            Console.ReadKey();
            return;
        }
    }
}
