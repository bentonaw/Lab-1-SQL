using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Labb_1_SQL.NewFolder.Menus;
using System.Data.SqlClient;
using System.Reflection;

namespace Labb_1_SQL.Interface
{
    internal class SubMenu
    {
        internal static void StudentSortMenu(SqlConnection connection, DisplayMethod studentDisplayMethod)
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
                        DisplayTable.GetTable(connection, "SELECT * FROM Students INNER JOIN Classes ON Students.ClassId_FK = Classes.ClassId ORDER BY FirstName ASC", studentDisplayMethod);
                        break;
                    case "2": //First name desc
                        DisplayTable.GetTable(connection, "SELECT * FROM Students INNER JOIN Classes ON Students.ClassId_FK = Classes.ClassId ORDER BY FirstName DESC", studentDisplayMethod);
                        break;
                    case "3": //last name asc
                        DisplayTable.GetTable(connection, "SELECT * FROM Students INNER JOIN Classes ON Students.ClassId_FK = Classes.ClassId ORDER BY LastName ASC", studentDisplayMethod);
                        break;
                    case "4": //last name desc
                        DisplayTable.GetTable(connection, "SELECT * FROM Students INNER JOIN Classes ON Students.ClassId_FK = Classes.ClassId ORDER BY LastName DESC", studentDisplayMethod);
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

        internal static void FilterOption(SqlConnection connection, string message, string selection, DisplayMethod displaymethod)
        {
            Console.WriteLine("---");
            Console.Write(message);
            string input = Console.ReadLine().ToUpper();

            if (input == "E")
            {
                return;
            }
            using (SqlCommand command = new SqlCommand(selection, connection))
            {
                command.Parameters.AddWithValue("@Column", input);
                try
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            displaymethod(reader);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Input Error");
                }
                Console.WriteLine();
                Console.Write("Press enter to continue");
                Console.ReadLine();
            }
        }

        internal static void AddData(SqlConnection connection, string type)
        {
            Console.Write("Enter Firstname: ");
            string firstName = Console.ReadLine();
            Console.Write("Enter Lastname: ");
            string lastName = Console.ReadLine();
            if (type == "student")
            {
                DateTime dateOfBirth;
                while (true)
                {
                    Console.Write("DateOfBirth (yyyy-mm-dd): ");
                    string dateOfBirthString = Console.ReadLine();

                    if (DateTime.TryParse(dateOfBirthString, out dateOfBirth))
                    {
                        // The input was successfully parsed into a DateTime
                        Console.WriteLine($"Date of Birth: {dateOfBirth}");
                        break;
                    }
                    else
                    {
                        // The input was not in the correct format
                        Console.WriteLine("Invalid date format. Please enter the date in yyyy-mm-dd format.");
                    }
                }

                Console.Write("Gender (Male/Female/Non-Binary/Other: ");
                string gender = Console.ReadLine();

                Console.Write("Which Class: ");
                int classId_FK = int.Parse(Console.ReadLine());

                // inserts into database
                string insertCommand = "INSERT INTO Students (FirstName, LastName, DateOfBirth, Gender, ClassId_FK) VALUES (@FirstName, @LastName, @DateOfBirth, @Gender, @ClassId_FK)";
                using (SqlCommand command = new SqlCommand(insertCommand, connection))
                {
                    // Add parameters
                    command.Parameters.AddWithValue("@FirstName", firstName);
                    command.Parameters.AddWithValue("@LastName", lastName);
                    command.Parameters.AddWithValue("@DateOfBirth", dateOfBirth);
                    command.Parameters.AddWithValue("@Gender", gender);
                    command.Parameters.AddWithValue("@ClassId_FK", classId_FK);

                    try
                    {
                        int rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine($"{rowsAffected} row(s) inserted successfully.");
                        Console.WriteLine();
                        Console.WriteLine("Press enter to continue");
                        Console.ReadLine();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                        Console.WriteLine();
                        Console.WriteLine("Press enter to continue");
                        Console.ReadLine();
                    }
                }
            }
            else if (type == "personnel")
            {
                Console.Write("What role will the person have?:");
                string role = Console.ReadLine();

                string insertCommand = "INSERT INTO Personnel (FirstName, LastName, Role) VALUES (@FirstName, @LastName, @Role)";
                using (SqlCommand command = new SqlCommand(insertCommand, connection))
                {
                    // Add parameters
                    command.Parameters.AddWithValue("@FirstName", firstName);
                    command.Parameters.AddWithValue("@LastName", lastName);
                    command.Parameters.AddWithValue("@Role", role);

                    try
                    {
                        int rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine($"{rowsAffected} row(s) inserted successfully.");
                        Console.WriteLine();
                        Console.WriteLine("Press enter to continue");
                        Console.ReadLine();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                        Console.WriteLine();
                        Console.WriteLine("Press enter to continue");
                        Console.ReadLine();
                    }
                }
            }
            
        }
    }
}
