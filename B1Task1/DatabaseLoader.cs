using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B1Task1
{
    internal class DatabaseLoader
    {
        private string connectionString;
        private const int BatchSize = 100000;
        private int batchCount;

        public DatabaseLoader(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void LoadDataToDbFromFile(string filePath)
        {
            var totalRows = CountLinesInFile(filePath);
            var connection = new SqlConnection(connectionString);
            List<string> linesBatch = new List<string>();
            StreamReader reader = new StreamReader(filePath);
            string line;
            connection.Open();
            while ((line = reader.ReadLine()) != null)
            {
                linesBatch.Add(line);
                if (linesBatch.Count >= BatchSize)
                {
                    BulkInsertDataTable(linesBatch);
                    linesBatch.Clear();
                    batchCount++;
                    var inserted = (batchCount * BatchSize);
                    Console.Write("\rRows inserted: " + inserted + ", Rows left: " + (totalRows - inserted));
                }
            }
            if (linesBatch.Count > 0)
            {
                BulkInsertDataTable(linesBatch);
                Console.WriteLine("\n\rAll rows inserted: " + (batchCount * BatchSize + linesBatch.Count));
            }
        }


        private int CountLinesInFile(string filePath)
        {
            return File.ReadLines(filePath).Count();
        }

        private (DateTime, string, string, long, double) ParseLine(string line)
        {
            var parts = line.Split(new string[] { "||" }, StringSplitOptions.None);
            DateTime date = DateTime.Now;
            string latin = "as", cyrillic = "as";
            long intNumber = 0;
            double floatNumber = 0;
            if (parts.Length < 5)
            {
                return (date, latin, cyrillic, intNumber, floatNumber);
            }
            try
            {
                date = DateTime.Parse(parts[0]);
                latin = parts[1];
                cyrillic = parts[2];
                intNumber = long.Parse(parts[3]);
                floatNumber = double.Parse(parts[4]);
            }
            catch { }

            return (date, latin, cyrillic, intNumber, floatNumber);
        }

        public void BulkInsertDataTable(List<string> dataList)
        {
            Random random = new Random();
            DataTable table = new DataTable();
            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("DateField", typeof(DateTime));
            table.Columns.Add("LatinCharacters", typeof(string));
            table.Columns.Add("CyrillicCharacters", typeof(string));
            table.Columns.Add("IntNumber", typeof(int));
            table.Columns.Add("FloatNumber", typeof(float));
            foreach (var item in dataList)
            {
                var data = ParseLine(item);
                table.Rows.Add(random.Next(), data.Item1, data.Item2, data.Item3, data.Item4, data.Item5);
            }
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlBulkCopy bulkCopy = new SqlBulkCopy(connection);
            bulkCopy.DestinationTableName = "B1Database.dbo.DataTable";
            bulkCopy.WriteToServer(table);
        }
    }
}
