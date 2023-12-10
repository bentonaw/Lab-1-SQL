using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Labb_1_SQL
{
    internal class Classes
    {
        internal static void GetClasses()
        {
            string connectionString = @"Data Source=(localdb)\.;Initial Catalog=LAB1SQL;Integrated Security=True;Pooling=False";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Classes", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        Console.Clear();
                        while (reader.Read())
                        {
                            string className = reader.GetString(reader.GetOrdinal("ClassName"));
                            //string classTeacher = reader.GetString(reader.GetOrdinal("PersonelId_FK"));
                            string classCode = reader.GetString(reader.GetOrdinal("ClassCode"));
                            int classYear =reader.GetInt32(reader.GetOrdinal("ClassYear"));

                            Console.WriteLine($"Class: {classCode},  \tClass name: {className},  \tStart year: {classYear},");
                            
                        }
                        Console.ReadLine();
                        //create menu for selecting class (method below) or go back
                    }
                }
            }
        }
        internal static void GetStudentsInClass()
        {

        }
    }
}
