

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
                        PassengerMenu.View(passengers, flights);
                        break;
                    case 2:
                        FlightMenu.View(flights,airplanes,crews,passengers);
                        break;
                    case 3:
                        AirplaneMenu.View(airplanes,flights);
                        break;
                    case 4:
                        CrewMenu.View(crews, crewMembers);
                        break;
                    default:
                        break;

                }
            }

        }
       
    }
}
