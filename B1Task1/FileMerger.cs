using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B1Task1
{
    internal class FileMerger
    {
        public static void MergeFiles(string directoryPath, string outputFile, string stringToRemove)
        {
            var skipedStringsCount = 0;
            string[] fileNames = Directory.GetFiles(directoryPath);

            StreamWriter writer = new StreamWriter(outputFile);
            for (int i = 0; i < fileNames.Length; i++)
            {
                StreamReader reader = new StreamReader(fileNames[i]);
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Contains(stringToRemove) && !string.IsNullOrEmpty(stringToRemove))
                    {
                        skipedStringsCount++;
                        continue;
                    }
                    writer.WriteLine(line);
                }
                Console.Write("\rFiles merged: " + (i + 1));
            }
            writer.Close();
            Console.WriteLine("\nSkiped strings count: " + skipedStringsCount);
        }
    }
}
