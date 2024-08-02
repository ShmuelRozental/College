using College.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace College.services
{
    internal static class StudentService
    {
        public static void AddStudent(Student student)
        {
            DatabaseHelper dbHelper = DatabaseHelper.Instance();
            string query = "INSERT INTO Students (StudentName, Email) VALUES (@Name, @Email)";
            SqlParameter[] parameters = {
                new SqlParameter("@Name", student.Name),
                new SqlParameter("@Email", student.Email)
            };
            dbHelper.ExecuteNonQuery(query, parameters);
        }

        public static Student GetStudentById(int StudentId)
        {
            DatabaseHelper dbHelper = DatabaseHelper.Instance(); 
            string query = "SELECT StudentId, StudentName, Email FROM Students WHERE StudentId = @StudentId";
            SqlParameter parameter = new SqlParameter("@StudentId", SqlDbType.Int) { Value = StudentId };

            using (SqlDataReader reader = dbHelper.ExecuteReader(query, new[] { parameter }))
            {
                if (reader.Read())
                {
                    int studentId = Convert.ToInt32(reader["StudentId"]);
                    string studentName = Convert.ToString(reader["StudentName"]);
                    string studentEmail = Convert.ToString(reader["Email"]);

                    return new Student(studentId, studentName, studentEmail);
                }
                // Handle case where no student with the given email exists
                return null;
            }
        }

        public static Student GetStudentByEmail(string email)
        {
            DatabaseHelper dbHelper = DatabaseHelper.Instance(); // Obtain the DatabaseHelper instance
            string query = "SELECT StudentId, StudentName, Email FROM Students WHERE Email = @Email";
            SqlParameter parameter = new SqlParameter("@Email", SqlDbType.NVarChar) { Value = email };

            using (SqlDataReader reader = dbHelper.ExecuteReader(query, new[] { parameter }))
            {
                if (reader.Read())
                {
                    int studentId = Convert.ToInt32(reader["StudentId"]);
                    string studentName = Convert.ToString(reader["StudentName"]);
                    string studentEmail = Convert.ToString(reader["Email"]);

                    return new Student(studentId, studentName, studentEmail);
                }
                // Handle case where no student with the given email exists
                return null;
            }
        }

    }
}
