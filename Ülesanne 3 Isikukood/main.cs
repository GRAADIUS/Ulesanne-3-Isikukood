using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ülesanne_3_Isikukood
{
    public class Program
    {
        public static void Main()
        {
            while (true)
            {
                Console.WriteLine("Enter IDcode");
                string IDcode = Console.ReadLine();
                if (new IdCode(IDcode).IsValid())
                {
                    Console.WriteLine("Sugu: " + new IdCode(IDcode).GetGender()); // male
                    Console.WriteLine("Sünniaasta: " + new IdCode(IDcode).GetFullYear()); // 2000
                    Console.WriteLine("Sünnipäev: " + new IdCode(IDcode).GetBirthDate());  // 20.05.2000
                    Console.WriteLine(new IdCode(IDcode).GetAge() + " years");  // 23 years
                    string lastThreeDigits = new string(IDcode.Reverse().Take(3).Reverse().ToArray());
                    Console.WriteLine(new IdCode(IDcode).PlaceOfBirth(int.Parse(lastThreeDigits)));  // Maarjamoisa kliinikum(Tartu), Jogeva haigla
                }
                else
                {
                    Console.WriteLine("Vale kood!");
                }
            }
        }
    }
}
