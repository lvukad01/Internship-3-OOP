using System;
using System.Collections.Generic;

namespace Internship_3_OOP
{
    public static class Helper
    {
        public static double ReadDouble(string message)
        {
            Console.Write(message);
            double number;

            while (!double.TryParse(Console.ReadLine(), out number) || number < 0)
                Console.Write("Neispravan unos, pokušajte ponovno: ");

            return number;
        }

        public static DateOnly ReadDate(string message)
        {
            Console.Write(message);
            DateOnly date;

            while (!DateOnly.TryParse(Console.ReadLine(), out date))
                Console.Write("Neispravan datum, unesite u formatu yyyy-mm-dd: ");

            return date;
        }

        public static DateTime ReadDateTime(string message)
        {
            Console.Write(message);
            DateTime date;

            while (!DateTime.TryParse(Console.ReadLine(), out date))
                Console.Write("Neispravan datum, unesite u formatu yyyy-mm-dd hh:mm: ");

            return date;
        }

        public static TimeOnly ReadTime(string message)
        {
            Console.Write(message);
            TimeOnly time;

            while (!TimeOnly.TryParse(Console.ReadLine(), out time))
                Console.Write("Neispravan unos sati, unesite u formatu (hh:mm): ");

            return time;
        }

        public static string ReadNonEmpty(string message)
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

        public static int ReadInt(string message)
        {
            Console.Write(message);
            int value;

            while (!int.TryParse(Console.ReadLine(), out value)||value<0)
            {
                Console.Write("Neispravan unos, unesite pozitivan cijeli broj: ");
            }

            return value;
        }

        public static char ReadChar(string message)
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
