using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Labb_1_SQL.Interface;

namespace Labb_1_SQL.NewFolder
{
    internal class Menus
    {
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
        internal delegate void DisplayMethod(SqlDataReader reader);

        internal static void Menu(SqlConnection connection)
        {
            while (true)
            {
                Console.Clear();
                WelcomeScreen();
                string select = Console.ReadLine();
                DisplayMethod studentDisplayMethod = DisplayTable.StudentDisplay;
                DisplayMethod classDisplayMethod = DisplayTable.ClassDisplay;
                DisplayMethod personnelDisplayMethod = DisplayTable.PersonnelDisplay;
                DisplayMethod gradeDisplayMethod = DisplayTable.GradeDisplay;
                DisplayMethod courseDisplayMethod = DisplayTable.CourseDisplay;

                switch (select)
                {
                    case "1": // Get all students
                        DisplayTable.GetTable(connection, @"
                        SELECT * FROM Students 
                        INNER JOIN Classes ON Students.ClassId_FK = Classes.ClassId", 
                        studentDisplayMethod);
                        // menu for selecting sorting order
                        SubMenu.StudentSortMenu(connection, studentDisplayMethod);
                        break;
                    case "2": // add new student
                        SubMenu.AddData(connection,"student");
                        break;
                    case "3": // get all clasees
                        DisplayTable.GetTable(connection, @"
                        SELECT * FROM Classes", classDisplayMethod);
                        SubMenu.FilterOption(connection, "Enter the class code to view students or press 'e' to go back:", @"
                        SELECT * FROM Students 
                        INNER JOIN Classes ON Students.ClassId_FK = Classes.ClassId
                        WHERE ClassCode = @Column", studentDisplayMethod);
                        break;
                    case "4": // get all personnel
                        DisplayTable.GetTable(connection, @"
                        SELECT * FROM Personnel", 
                        personnelDisplayMethod);
                        SubMenu.FilterOption(connection, "Enter the role to filter personnel or press 'e' to go back:", @"
                        SELECT * FROM Personnel 
                        WHERE Role = @Column", 
                        personnelDisplayMethod);
                        break;
                    case "5": // add new personnel
                        SubMenu.AddData(connection,"personnel")
                        break;
                    case "6": // get gradings, last 30 days
                        DisplayTable.GetTable(connection, @"
                        SELECT 
                            Students.FirstName AS StudentFirstName,
                            Students.LastName AS StudentLastName,
                            Grade,
                            Courses.CourseName AS CourseName,
                            Personnel.FirstName AS TeacherFirstName,
                            Personnel.LastName AS TeacherLastName,
                            GradeDate
                        FROM Grades
                        INNER JOIN Students ON Grades.StudentId_FK = Students.StudentId
                        INNER JOIN Courses ON Grades.CourseId_FK = Courses.CourseId
                        INNER JOIN Personnel ON Grades.PersonnelId_FK = Personnel.PersonnelId
                        WHERE GradeDate >= DATEADD(day, -30, GETDATE())", 
                        gradeDisplayMethod);
                        SubMenu.FilterOption(connection, "Press enter to go back", "", gradeDisplayMethod);
                        break;
                    case "7": // Get all courses and average grades
                        DisplayTable.GetTable(connection, @"
                        SELECT
                            CourseName,
                            AVG(Grades.Grade) AS AverageGrade,
                            MIN(Grades.Grade) AS MinGrade,
                            MAX(Grades.Grade) AS MaxGrade
                        FROM Courses
                        LEFT JOIN Grades ON Courses.CourseId = Grades.CourseId_FK 
                        GROUP BY Courses.CourseName", 
                        courseDisplayMethod);
                        SubMenu.FilterOption(connection, "Press enter to go back", "", gradeDisplayMethod);
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
