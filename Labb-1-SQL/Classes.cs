using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Labb_1_SQL
{
    internal class Classes
    {
        internal static void GetClasses(SqlConnection connection)
        {
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

                        Console.WriteLine($"Class code: {classCode},  \tClass name: {className},  \tStart year: {classYear},");
                            
                    }
                    Console.WriteLine("---");
                    Console.Write("Enter the class code to view students or press 'e' to go back:");
                    string input = Console.ReadLine().ToUpper();

                    if (input == "e")
                    {
                        // User wants to go back
                        return;
                    }
                    //GetStudentsInClass(connection, input);
                }
            }
        }
        internal static void StudentSearchByClass(SqlConnection connection)
        {
            Console.Write("Enter the class code to view students or press 'e' to go back:");
            string input = Console.ReadLine().ToUpper();

            using (SqlCommand command = new SqlCommand("SELECT * FROM Students WHERE ClassId_FK = (SELECT ClassId FROM Classes WHERE ClassCode = @ClassCode", connection))
            {
                command.Parameters.AddWithValue("@ClassCode", input);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Students.StudentDisplay(reader);
                    }
                }
                Console.ReadLine();
            }
        }
        internal static void GetStudentsInClass(SqlConnection connection, string input, string classSearch)
        {
            using (SqlCommand command = new SqlCommand("SELECT * FROM Students WHERE ClassId_FK = (SELECT ClassId FROM Classes WHERE ClassCode = @ClassCode", connection))
            {
                command.Parameters.AddWithValue("@ClassCode", input);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Students.StudentDisplay(reader);
                    }
                }
                Console.ReadLine();
            }
            
        }
    }
}
