using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B1Task1
{
    internal class FileGenerator
    {
        private static Random random = new Random();
        private const int filesCount = 100;
        private const int rowsCount = 100_000;
        private const int generatedCharatersCount = 10;
        private const int maxIntBound = 99_999_999;
        private const int minIntBound = 1;
        private const int minFloatBound = 1;
        private const int maxFloatBound = 20;


        public static void GenerateFiles(string folderPath)
        {

            Directory.CreateDirectory(folderPath);

            for (int i = 0; i < filesCount; i++)
            {
                Console.Write("\rFiles created: " + (i + 1));
                string filePath = Path.Combine(folderPath, $"file_{i}.txt");
                GenerateFile(filePath);
            }
            Console.WriteLine();
        }

        private static void GenerateFile(string fileName)
        {
            StreamWriter writer = new StreamWriter(fileName);
            for (int i = 0; i < rowsCount; i++)
            {
                writer.WriteLine(GenerateRandomLine());
            }
        }

        public static string GenerateRandomLine()
        {
            var datePart = GenerateRandomDate();
            var latinPart = GenerateRandomString("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz", generatedCharatersCount);
            var cyrillicPart = GenerateRandomString("абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ", generatedCharatersCount);
            var evenNumberPart = GenerateRandomEvenNumber(minIntBound, maxIntBound);
            var floatNumberPart = GenerateRandomFloat(minFloatBound, maxFloatBound);

            return $"{datePart}||{latinPart}||{cyrillicPart}||{evenNumberPart}||{floatNumberPart}||";
        }

        static string GenerateRandomDate()
        {
            int year = random.Next(DateTime.Today.Year - 5, DateTime.Today.Year + 1);
            int month = random.Next(1, 13); 
            int day;

            switch (month)
            {
                case 2: 
                    day = random.Next(1, DateTime.IsLeapYear(year) ? 30 : 29);
                    break;
                case 4:
                case 6:
                case 9:
                case 11:
                    day = random.Next(1, 31); 
                    break;
                default:
                    day = random.Next(1, 32); 
                    break;
            }

            return new DateTime(year, month, day).ToString("dd.MM.yyyy");
        }

        static string GenerateRandomString(string characters, int length)
        {
            var result = "";
            for (int i = 0; i < length; i++)
            {
                result += characters[random.Next(characters.Length)];
            }
            return result.ToString();
        }

        static int GenerateRandomEvenNumber(int min, int max)
        {
            return random.Next(min / 2, max / 2) * 2;
        }

        static string GenerateRandomFloat(int min, int max)
        {
            double number = random.NextDouble() * (max - min) + min;
            return number < 10 ? number.ToString("F8") : number.ToString("F7");
        }
    }
}
