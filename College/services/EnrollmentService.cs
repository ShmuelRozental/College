using College.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace College.services
{
    internal class EnrollmentService
    {
        public static List<Course> GetEnrolledCoursesByStudentId(int studentId)
        {
            List<Course> courses = new List<Course>();

            DatabaseHelper dbHelper = DatabaseHelper.Instance();
            string query = @"
                SELECT c.CourseId, c.CourseName, cc.Price
                FROM Enrollments e
                JOIN CourseCycles cc ON e.CycleId = cc.CycleId
                JOIN Courses c ON cc.CourseId = c.CourseId
                WHERE e.StudentId = @StudentId";

            SqlParameter[] parameters = {
                new SqlParameter("@StudentId", studentId)
            };

            using (SqlDataReader reader = dbHelper.ExecuteReader(query, parameters))
            {
                while (reader.Read())
                {
                    courses.Add(new Course
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Price = reader.GetDecimal(2)
                    });
                }
            }

            return courses;
        }

       public static void EnrollStudent(int studentId, int cycleId)
        {
            DatabaseHelper dbHelper = DatabaseHelper.Instance();
            // Retrieve cycle price to initialize balance
            string query = @"
        INSERT INTO Enrollments (StudentId, CycleId, Balance)
        SELECT @StudentId, @CycleId, cc.Price
        FROM CourseCycles cc
        WHERE cc.CycleId = @CycleId";

            SqlParameter[] parameters = {
        new SqlParameter("@StudentId", studentId),
        new SqlParameter("@CycleId", cycleId)
    };

            dbHelper.ExecuteNonQuery(query, parameters);
        }

        public static Enrollment GetEnrollmentById(int enrollmentId)
        {
            DatabaseHelper dbHelper = DatabaseHelper.Instance();
            string query = @"
                SELECT EnrollmentId, StudentId, CycleId, Balance
                FROM Enrollments
                WHERE EnrollmentId = @EnrollmentId";

            SqlParameter[] parameters = {
                new SqlParameter("@EnrollmentId", enrollmentId)
            };

            using (SqlDataReader reader = dbHelper.ExecuteReader(query, parameters))
            {
                if (reader.Read())
                {
                    int studentId = reader.GetInt32(1);
                    int cycleId = reader.GetInt32(2);
                    decimal balance = reader.GetDecimal(3);

                    return new Enrollment(enrollmentId, studentId, cycleId, balance);
                }
            }

            return null; // Return null if no enrollment found with given ID
        }

        public static decimal GetTotalBalanceByStudentId(int studentId)
        {
            decimal totalBalance = 0;

            DatabaseHelper dbHelper = DatabaseHelper.Instance();
            string query = "SELECT SUM(Balance) FROM Enrollments WHERE StudentId = @StudentId";

            SqlParameter[] parameters = {
                new SqlParameter("@StudentId", studentId)
            };

            object result = dbHelper.ExecuteScalar(query, parameters);
            if (result != DBNull.Value)
            {
                totalBalance = (decimal)result;
            }

            return totalBalance;
        }

        public static bool IsStudentEnrolled(int studentId, int cycleId)
        {
            DatabaseHelper dbHelper = DatabaseHelper.Instance();
            string query = "SELECT COUNT(*) FROM Enrollments WHERE StudentId = @StudentId AND CycleId = @CycleId";
            SqlParameter[] parameters = {
            new SqlParameter("@StudentId", studentId),
            new SqlParameter("@CycleId", cycleId)
        };
            int count = Convert.ToInt32(dbHelper.ExecuteScalar(query, parameters));
            return count > 0;
        }

        public static void MakePayment(int enrollmentId, decimal amount)
        {
            DatabaseHelper dbHelper = DatabaseHelper.Instance();
            string query = "INSERT INTO Payments (EnrollmentId, Amount) VALUES (@EnrollmentId, @Amount)";
            SqlParameter[] parameters = {
                new SqlParameter("@EnrollmentId", enrollmentId),
                new SqlParameter("@Amount", amount)
            };
            dbHelper.ExecuteNonQuery(query, parameters);
        }

        public static decimal GetTotalPaidByEnrollmentId(int enrollmentId)
        {
            decimal totalPaid = 0;

            DatabaseHelper dbHelper = DatabaseHelper.Instance();
            string query = "SELECT SUM(Amount) FROM Payments WHERE EnrollmentId = @EnrollmentId";

            SqlParameter[] parameters = {
                new SqlParameter("@EnrollmentId", enrollmentId)
            };

            object result = dbHelper.ExecuteScalar(query, parameters);
            if (result != DBNull.Value)
            {
                totalPaid = (decimal)result;
            }

            return totalPaid;
        }
    }
}
