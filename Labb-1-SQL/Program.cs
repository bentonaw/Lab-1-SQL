using System.Data.SqlClient;
//using System.Reflection.PortableExecutable;
//using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Labb_1_SQL
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /* 
            * - sort by first or last name
            ** - asc or desc
            ***
            */

            //string connectionString = @"Data Source=(localdb)\.;Initial Catalog=LAB1SQL;Integrated Security=True;Encrypt=True";
            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    connection.Open();
            //}
            
            Menu();



        }
        static void WelcomeScreen()
        {
            Console.WriteLine("Welcome to the School database");
            Console.WriteLine("1. Get all students"); // sort by first or last name*, asc or desc** for each option
                                                      // add student
            Console.WriteLine("2. Get all classes"); // asc or desc all classes based on term*, asc or desc** students name by first and last*
            Console.WriteLine("3. Get all courses"); // list all courses with average grade, lowest and highest grade
            Console.WriteLine("4. Get gradings"); // gradings last month, gradings based on criterias
            Console.WriteLine("5. Get all personnel"); // get all personnel *, base on role, add personnel
            Console.WriteLine("e. exit program");
            Console.Write("Please select option by inputting the corresponding number: ");
        }

        static void Menu()
        {
            while (true)
            {
                Console.Clear();
                WelcomeScreen();
                string select = Console.ReadLine();
                switch (select)
                {
                    case "1":
                        Students.GetStudents();
                        break;
                    case "2":
                        break;
                    case "3":
                        break;
                    case "4":
                        break;
                    case "5":
                        break;
                    case "e":
                        Console.WriteLine("bye");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine();
                        Console.WriteLine("Invalid input.");
                        Thread.Sleep(800);
                        break;
                }
            }
        }

        
    }
}