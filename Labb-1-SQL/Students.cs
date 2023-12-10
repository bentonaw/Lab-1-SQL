using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Labb_1_SQL
{
    internal class Students
    {
        internal static void GetStudents()
        {
            string connectionString = @"Data Source=(localdb)\.;Initial Catalog=LAB1SQL;Integrated Security=True;Pooling=False";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Students INNER JOIN Classes ON Students.ClassId_FK = Classes.ClassId", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        Console.Clear();
                        while (reader.Read())
                        {
                            string firstName = reader.GetString(reader.GetOrdinal("FirstName"));
                            string lastName = reader.GetString(reader.GetOrdinal("LastName"));
                            string classCode = reader.GetString(reader.GetOrdinal("ClassCode"));

                            Console.WriteLine($"Name: {firstName} {lastName},\t Class: {classCode}");
                        }
                    }
                }
                // menu for selecting sorting order
                SortMenu(connection);
            }
        }
        static void StudentDisplay(SqlDataReader reader)
        {
            string firstName = reader.GetString(reader.GetOrdinal("FirstName"));
            string lastName = reader.GetString(reader.GetOrdinal("LastName"));
            string classCode = reader.GetString(reader.GetOrdinal("ClassCode"));

            Console.WriteLine($"Name: {firstName} {lastName},\t Class: {classCode}");
        }
        static void StudentList(string sort, SqlConnection connection)
        {
            Console.Clear();
            using (SqlCommand command = new SqlCommand(sort, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    Console.Clear();
                    while (reader.Read())
                    {
                        StudentDisplay(reader);
                    }
                }
            }
        }

        static void SortMenu(SqlConnection connection)
        {
            while (true)
            {
                Console.WriteLine("---");
                Console.WriteLine("Would you like to sort differently or go back?");
                Console.WriteLine("1. By first name, ascending.");
                Console.WriteLine("2. By first name, descending.");
                Console.WriteLine("3. By last name, ascending.");
                Console.WriteLine("4. By last name, descending.");
                Console.WriteLine("e. Go back");
                Console.Write(": ");

                string input = Console.ReadLine();
                switch (input)
                {
                    case "1": //First name asc
                        StudentList("SELECT * FROM Students INNER JOIN Classes ON Students.ClassId_FK = Classes.ClassId ORDER BY FirstName ASC", connection);
                        break;
                    case "2": //First name desc
                        StudentList("SELECT * FROM Students INNER JOIN Classes ON Students.ClassId_FK = Classes.ClassId ORDER BY FirstName DESC", connection);
                        break;
                    case "3": //last name asc
                        StudentList("SELECT * FROM Students INNER JOIN Classes ON Students.ClassId_FK = Classes.ClassId ORDER BY LastName ASC", connection);
                        break;
                    case "4": //last name desc
                        StudentList("SELECT * FROM Students INNER JOIN Classes ON Students.ClassId_FK = Classes.ClassId ORDER BY LastName DESC", connection);
                        break;
                    case "e":
                        return;
                    default:
                        Console.Clear();
                        Console.WriteLine("Invalid input. Please select from option 1-4 or \"e\"");
                        break;
                }
            }
        }
    }
}
