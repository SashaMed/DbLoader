using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B1Task1
{
    internal class DatabaseProcedureCaller
    {
        private string connectionString;

        public DatabaseProcedureCaller(string connectionString)
        {
            this.connectionString = connectionString;
        }

        
        public void GetTotalSum()
        {
            long totalSum = 0;
            string query = "SELECT SUM(IntNumber) AS TotalSum FROM B1Database.dbo.DataTable";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        totalSum = Convert.ToInt64(result);
                    }
                }
            }
            Console.WriteLine("Total sum: " + totalSum);
        }

        public void CalculateMedian()
        {
            decimal median = 0;
            string query = @"
            WITH NumberedRows AS
            (
                SELECT FloatNumber, 
                       ROW_NUMBER() OVER (ORDER BY FloatNumber) AS RowNum,
                       COUNT(*) OVER () AS TotalRows
                FROM B1Database.dbo.DataTable
            )
            SELECT AVG(FloatNumber) as Median
            FROM NumberedRows
            WHERE RowNum IN ((TotalRows + 1) / 2, (TotalRows + 2) / 2)";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            object result = command.ExecuteScalar();
            if (result != DBNull.Value)
            {
                median = Convert.ToDecimal(result);
            }
            Console.WriteLine("Median: " + median);
        }
    }
}
