using DatabaseHelper.SQL;
using Microsoft.Data.SqlClient;
using System.Data;

namespace G19_20240424
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var database = new Database("Server=Jokha-PC;Database=NothWind;Integrated Security=True; TrustServerCertificate = true;");
            //database.ExecuteNonQuery("INSERT INTO Categories (Name) VALUES ('Nika')");
            //test reader
            using SqlDataReader reader = database.ExecuteReader("SELECT * FROM Categories");
            while (reader.Read())
            {
                Console.WriteLine(reader["Name"]);
            }
        }
    }
}
