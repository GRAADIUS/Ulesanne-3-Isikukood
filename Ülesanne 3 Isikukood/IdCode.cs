using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ülesanne_3_Isikukood;
using static System.Net.Mime.MediaTypeNames;

namespace Ülesanne_3_Isikukood
{
    public class IdCode
    {
        private readonly string _idCode;

        public IdCode(string idCode)
        {
            _idCode = idCode;
        }
        private bool IsValidLength()
        {
            return _idCode.Length == 11;
        }
        private bool ContainsOnlyNumbers()
        {
            // return _idCode.All(Char.IsDigit);

            for (int i = 0; i < _idCode.Length; i++)
            {
                if (!Char.IsDigit(_idCode[i]))
                {
                    return false;
                }
            }
            return true;
        }
        private int GetGenderNumber()
        {
            return Convert.ToInt32(_idCode.Substring(0, 1));
        }
        public string GetGender()
        {
            int genderNumber = GetGenderNumber();
            if (genderNumber == 1 || genderNumber == 3 || genderNumber == 5 || genderNumber == 7)
            {
                return "Male";
            }
            else if (genderNumber == 2 || genderNumber == 4 || genderNumber == 6 || genderNumber == 8)
            {
                return "Female";
            }
            else
            {
                return "";
            }
        }
        private bool IsValidGenderNumber()
        {
            int genderNumber = GetGenderNumber();
            return genderNumber > 0 && genderNumber < 7;
        }
        private int Get2DigitYear()
        {
            return Convert.ToInt32(_idCode.Substring(1, 2));
        }
        public int GetFullYear()
        {
            int genderNumber = GetGenderNumber();
            // 1, 2 => 18xx
            // 3, 4 => 19xx
            // 5, 6 => 20xx
            return 1800 + (genderNumber - 1) / 2 * 100 + Get2DigitYear();
        }
        private int GetMonth()
        {
            return Convert.ToInt32(_idCode.Substring(3, 2));
        }
        private bool IsValidMonth()
        {
            int month = GetMonth();
            return month > 0 && month < 13;
        }
        private static bool IsLeapYear(int year)
        {
            return year % 4 == 0 && year % 100 != 0 || year % 400 == 0;
        }
        private int GetDay()
        {
            return Convert.ToInt32(_idCode.Substring(5, 2));
        }
        private bool IsValidDay()
        {
            int day = GetDay();
            int month = GetMonth();
            int maxDays = 31;
            if (new List<int> { 4, 6, 9, 11 }.Contains(month))
            {
                maxDays = 30;
            }
            if (month == 2)
            {
                if (IsLeapYear(GetFullYear()))
                {
                    maxDays = 29;
                }
                else
                {
                    maxDays = 28;
                }
            }
            return 0 < day && day <= maxDays;
        }
        private int CalculateControlNumberWithWeights(int[] weights)
        {
            int total = 0;
            for (int i = 0; i < weights.Length; i++)
            {
                total += Convert.ToInt32(_idCode.Substring(i, 1)) * weights[i];
            }
            return total;
        }
        private bool IsValidControlNumber()
        {
            int controlNumber = Convert.ToInt32(_idCode[^1..]);
            int[] weights = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 1 };
            int total = CalculateControlNumberWithWeights(weights);
            if (total % 11 < 10)
            {
                return total % 11 == controlNumber;
            }
            // second round
            int[] weights2 = { 3, 4, 5, 6, 7, 8, 9, 1, 2, 3 };
            total = CalculateControlNumberWithWeights(weights2);
            if (total % 11 < 10)
            {
                return total % 11 == controlNumber;
            }
            // third round, control number has to be 0
            return controlNumber == 0;
        }

        public bool IsValid()
        {
            return IsValidLength() && ContainsOnlyNumbers()
                && IsValidGenderNumber() && IsValidMonth()
                && IsValidDay()
                && IsValidControlNumber();
        }
        public DateOnly GetBirthDate()
        {
            int day = GetDay();
            int month = GetMonth();
            int year = GetFullYear();
            return new DateOnly(year, month, day);
        }
        public int GetAge()
        {
            int year_of_burn = GetFullYear();
            var currentYear = DateTime.Now.Year;
            int Age = currentYear - year_of_burn - 1;
            return Age;
        }

        public string PlaceOfBirth(int number)
        {
            string place = "";
            if (number > 1 && number <= 10) 
            {
                place = "Kuressaare haigla";
            }
            else if (number > 11 && number <= 19)
            {
                place = "Tartu Ülikooli Naistekliinik";
            }
            else if (number > 21 && number <= 150)
            {
                place = "Ida - Tallinna keskhaigla, Pelgulinna sünnitusmaja(Tallinn)";
            }
            else if (number > 151 && number <= 160)
            {
                place = "Keila haigla";
            }
            else if (number > 161 && number <= 220)
            {
                place = "Rapla haigla, Loksa haigla, Hiiumaa haigla(Kärdla)";
            }
            else if (number > 221 && number <= 270)
            {
                place = "Ida - Viru keskhaigla(Kohtla - Järve, endine Jõhvi)";
            }
            else if (number > 271 && number <= 370)
            {
                place = "Maarjamõisa kliinikum(Tartu), Jõgeva haigla";
            }
            else if (number > 371 && number <= 420)
            {
                place = "Narva haigla";
            }
            else if (number > 421 && number <= 470)
            {
                place = "Pärnu haigla";
            }
            else if (number > 471 && number <= 490)
            {
                place = "Haapsalu haigla";
            }
            else if (number > 491 && number <= 520)
            {
                place = "Järvamaa haigla(Paide)";
            }
            else if (number > 521 && number <= 570)
            {
                place = "Rakvere haigla, Tapa haigla";
            }
            else if (number > 571 && number <= 600)
            {
                place = "Valga haigla";
            }
            else if (number > 601 && number <= 650)
            {
                place = "Viljandi haigla";
            }
            else if (number > 651 && number <= 150)
            {
                place = "Lõuna - Eesti haigla(Võru), Põlva haigla";
            }
            else
            {
                place = "Selle sünnitusmaja kohta andmed puuduvad";
            }
            return place;
        }
    }
}
