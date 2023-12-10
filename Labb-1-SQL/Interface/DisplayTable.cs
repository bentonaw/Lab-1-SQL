using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using static Labb_1_SQL.NewFolder.Menus;

namespace Labb_1_SQL.Interface
{
    internal class DisplayTable
    {
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
            decimal grade = reader.GetDecimal(reader.GetOrdinal("Grade"));
            DateTime gradeDate = reader.GetDateTime(reader.GetOrdinal("GradeDate"));
            string teacherFirstName = reader.GetString(reader.GetOrdinal("TeacherFirstName"));
            string teacherLastName = reader.GetString(reader.GetOrdinal("TeacherLastName"));
            string course = reader.GetString(reader.GetOrdinal("CourseName"));
            string studenFirstName = reader.GetString(reader.GetOrdinal("StudentFirstName"));
            String studentLastName = reader.GetString(reader.GetOrdinal("StudentLastName"));

            Console.WriteLine($"Student: {studenFirstName} {studentLastName} \tGrade: {grade} in {course} course by {teacherFirstName} {teacherLastName}, {gradeDate}");
        }
        internal static void CourseDisplay(SqlDataReader reader)
        {
            string courseName = reader.GetString(reader.GetOrdinal("CourseName"));
            decimal averageGrade = reader.IsDBNull(reader.GetOrdinal("AverageGrade")) ? 0.000M : Convert.ToDecimal(reader["AverageGrade"]);
            decimal minGrade = reader.IsDBNull(reader.GetOrdinal("MinGrade")) ? 0 : Convert.ToDecimal(reader["MinGrade"]);
            decimal maxGrade = reader.IsDBNull(reader.GetOrdinal("MaxGrade")) ? 0 : Convert.ToDecimal(reader["MaxGrade"]);

            Console.WriteLine($"Course: {courseName}\tAverage Grade: {averageGrade:F2}\tMin Grade: {minGrade}\tMax Grade: {maxGrade}");

            Console.WriteLine();
        }
    }
}
