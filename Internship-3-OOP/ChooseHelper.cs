

namespace Internship_3_OOP
{
    internal class ChooseHelper
    {
        public static Airplane ChooseAirplane(List<Airplane> airplanes)
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
        public static Gender ChooseGender()
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
