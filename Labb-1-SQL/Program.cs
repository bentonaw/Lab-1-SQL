using Labb_1_SQL.NewFolder;
using System.Data.SqlClient;

namespace Labb_1_SQL
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Data Source=(localdb)\.;Initial Catalog=LAB1SQL;Integrated Security=True;Pooling=False";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                Menus.Menu(connection);
            }
        }
    }
}