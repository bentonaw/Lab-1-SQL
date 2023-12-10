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
            string connectionString = @"Data Source=(localdb)\.;Initial Catalog=LAB1SQL;Integrated Security=True;Pooling=False";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                Menu(connection);
            }
        }
        static void WelcomeScreen()
        {
            Console.WriteLine("Welcome to the School database");
            Console.WriteLine("1. Get all students"); // sort by first or last name*, asc or desc** for each option
            Console.WriteLine("2. Add new student"); // add student
            Console.WriteLine("3. Get all classes"); // asc or desc all classes based on term*, asc or desc** students name by first and last*
            Console.WriteLine("4. Get all personnel"); // get all personnel *, base on role, add personnel
            Console.WriteLine("5. Add new personnel"); // get all personnel *, base on role, add personnel
            Console.WriteLine("6. Get gradings last 30 days"); // gradings last month, gradings based on criterias
            Console.WriteLine("7. Get all courses"); // list all courses with average grade, lowest and highest grade
            Console.WriteLine("e. exit program");
            Console.Write("Please select option by inputting the corresponding number: ");
        }

        static void Menu(SqlConnection connection)
        {
            while (true)
            {
                Console.Clear();
                WelcomeScreen();
                string select = Console.ReadLine();
                DisplayMethod studentDisplayMethod = StudentDisplay;
                DisplayMethod classDisplayMethod = ClassDisplay;
                DisplayMethod personnelDisplayMethod = ClassDisplay;
                DisplayMethod gradeDisplayMethod = GradeDisplay;

                switch (select)
                {
                    case "1":
                        GetTable(connection, "SELECT * FROM Students " +
                            "INNER JOIN Classes ON Students.ClassId_FK = Classes.ClassId", studentDisplayMethod);
                        // menu for selecting sorting order
                        StudentSortMenu(connection, studentDisplayMethod);
                        break;
                    case "2":
                        break;
                    case "3":
                        GetTable(connection, "SELECT * FROM Classes", classDisplayMethod);
                        FilterOption(connection, "Enter the class code to view students or press 'e' to go back:", 
                            "SELECT * FROM Students " +
                            " INNER JOIN Classes ON Students.ClassId_FK = Classes.ClassId" +
                            " WHERE ClassCode = @Column", studentDisplayMethod);
                        break;
                    case "4":
                        GetTable(connection, "SELECT * FROM Personnel", personnelDisplayMethod);
                        FilterOption(connection, "Enter the role to filter personnel or press 'e' to go back:", 
                            "SELECT * FROM Personnel " +
                            " WHERE Role = @Column", personnelDisplayMethod);
                        break;
                    case "5":
                        
                        break;
                    case "6":
                        GetTable(connection,"SELECT * FROM Grades" +
                            "INNER JOIN Students ON Grades.StudentId_FK = Students.StudentId" +
                            " WHERE Date >= DATEADD(day, -30, GETDATE())", gradeDisplayMethod); 
                        break;
                    case "7":
                        GetTable(connection, "SELECT * FROM Courses" +
                            " INNER JOIN Grades ON Courses.CourseId ON Grades.CourseId_FK", gradeDisplayMethod);
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
        internal delegate void DisplayMethod(SqlDataReader reader);

        internal static void GetTable(SqlConnection connection, string selection, DisplayMethod displayMethod)
        {
            Console.Clear();
            using (SqlCommand command = new SqlCommand(selection, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    Console.Clear();
                    while (reader.Read())
                    {
                        displayMethod(reader);
                    }
                }
            }
        }

        internal static void StudentDisplay(SqlDataReader reader)
        {
            string firstName = reader.GetString(reader.GetOrdinal("FirstName"));
            string lastName = reader.GetString(reader.GetOrdinal("LastName"));
            string classCode = reader.GetString(reader.GetOrdinal("ClassCode"));

            Console.WriteLine($"Name: {firstName} {lastName},\t Class: {classCode}");
        }

        internal static void ClassDisplay(SqlDataReader reader)
        {
            string className = reader.GetString(reader.GetOrdinal("ClassName"));
            //string classTeacher = reader.GetString(reader.GetOrdinal("PersonelId_FK"));
            string classCode = reader.GetString(reader.GetOrdinal("ClassCode"));
            int classYear = reader.GetInt32(reader.GetOrdinal("ClassYear"));

            Console.WriteLine($"Class code: {classCode},  \tClass name: {className},  \tStart year: {classYear},");
        }
        internal static void PersonnelDisplay(SqlDataReader reader)
        {
            string firstName = reader.GetString(reader.GetOrdinal("FirstName"));
            string lastName = reader.GetString(reader.GetOrdinal("LastName"));
            string role = reader.GetString(reader.GetOrdinal("Role"));

            Console.WriteLine($"Name: {firstName} {lastName}, {role}");
        }
        internal static void GradeDisplay(SqlDataReader reader)
        {
            int grade = reader.GetInt32(reader.GetOrdinal("Grade"));
            DateTime gradeDate = reader.GetDateTime(reader.GetOrdinal("GradeDate"));
            string teacherFirstName = reader.GetString(reader.GetOrdinal("Personnel.FirstName"));
            string teacherLastName = reader.GetString(reader.GetOrdinal("Personnel.LastName"));
            string course = reader.GetString(reader.GetOrdinal("Courses.CourseName"));
            string studenFirstName = reader.GetString(reader.GetOrdinal("Students.FirstName"));
            String studentLastName = reader.GetString(reader.GetOrdinal("Students.LastName"));

            Console.WriteLine($"Student: {studenFirstName} {studentLastName} \tGrade: {grade} in {course} by {teacherFirstName} {teacherLastName}, {gradeDate}");
        }

        internal static void CourseDisplay(SqlDataReader reader)
        {
            string courseName = reader.GetString(reader.GetOrdinal("CourseName")); 
            int grade = reader.GetInt32(reader.GetOrdinal("GRades.Grade"));

            Console.WriteLine();
        }

        static void StudentSortMenu(SqlConnection connection, DisplayMethod studentDisplayMethod)
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
                        GetTable(connection, "SELECT * FROM Students INNER JOIN Classes ON Students.ClassId_FK = Classes.ClassId ORDER BY FirstName ASC", studentDisplayMethod);
                        break;
                    case "2": //First name desc
                        GetTable(connection, "SELECT * FROM Students INNER JOIN Classes ON Students.ClassId_FK = Classes.ClassId ORDER BY FirstName DESC", studentDisplayMethod);
                        break;
                    case "3": //last name asc
                        GetTable(connection, "SELECT * FROM Students INNER JOIN Classes ON Students.ClassId_FK = Classes.ClassId ORDER BY LastName ASC", studentDisplayMethod);
                        break;
                    case "4": //last name desc
                        GetTable(connection, "SELECT * FROM Students INNER JOIN Classes ON Students.ClassId_FK = Classes.ClassId ORDER BY LastName DESC", studentDisplayMethod);
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
    }
}