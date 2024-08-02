using College.Models;
using College.services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace College.Services
{
    internal class EnrollmentService
    {
        // Retrieve enrollment object by enrollment ID
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
                    Student student = StudentService.GetStudentById(studentId);

                    int cycleId = reader.GetInt32(2);
                    CourseCycle courseCycle = CourseCycleService.GetCourseCycleById(cycleId);
                    decimal balance = reader.GetDecimal(3);

                    return new Enrollment(enrollmentId, student, courseCycle, balance);
                }
            }

            return null;
        }

        // Retrieve all enrollments for a student
        public static List<Enrollment> GetAllStudentEnrollments(int studentId)
        {
            List<Enrollment> studentEnrollments = new List<Enrollment>();

            DatabaseHelper dbHelper = DatabaseHelper.Instance();
            string query = @"
        SELECT EnrollmentId, StudentId, CycleId, Balance
        FROM Enrollments
        WHERE StudentId = @StudentId";

            // Define the parameters array
            SqlParameter[] parameters = {
        new SqlParameter("@StudentId", SqlDbType.Int) { Value = studentId }
    };

            // Execute the query
            using (SqlDataReader reader = dbHelper.ExecuteReader(query, parameters))
            {
                while (reader.Read())
                {
                    int enrollmentId = reader.GetInt32(reader.GetOrdinal("EnrollmentId"));
                    int cycleId = reader.GetInt32(reader.GetOrdinal("CycleId"));
                    decimal balance = reader.GetDecimal(reader.GetOrdinal("Balance"));

                    // Assuming you have methods to get the student and course cycle objects
                    Student student = StudentService.GetStudentById(studentId);
                    CourseCycle courseCycle = CourseCycleService.GetCourseCycleById(cycleId);

                    Enrollment enrollment = new Enrollment(enrollmentId, student, courseCycle, balance);
                    studentEnrollments.Add(enrollment);
                }
            }

            return studentEnrollments;
        }




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
                        
                    });
                }
            }

            return courses;
        }


        private static decimal GetCourseCyclePrice(int cycleId)
        {
            DatabaseHelper dbHelper = DatabaseHelper.Instance();
            string query = @"
                SELECT Price
                FROM CourseCycles
                WHERE CycleId = @CycleId";

            SqlParameter[] parameters = {
                new SqlParameter("@CycleId", cycleId)
            };

            object result = dbHelper.ExecuteScalar(query, parameters);
            return result != DBNull.Value ? (decimal)result : 0;
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


        public static void EnrollStudent(int studentId, int cycleId)
        {
            DatabaseHelper dbHelper = DatabaseHelper.Instance();

            decimal courseCyclePrice = GetCourseCyclePrice(cycleId);

            string query = @"
                INSERT INTO Enrollments (StudentId, CycleId, Balance)
                VALUES (@StudentId, @CycleId, @Balance)";

             SqlParameter[] parameters = {
                new SqlParameter("@StudentId", studentId),
                new SqlParameter("@CycleId", cycleId),
                new SqlParameter("@Balance", courseCyclePrice)
            };

            dbHelper.ExecuteNonQuery(query, parameters);
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

        public static int GetEnrollmentIdByCourseAndStudent(int courseId, int studentId)
        {
            int enrollmentId = 0;

            DatabaseHelper dbHelper = DatabaseHelper.Instance();
            string query = @"
                SELECT e.EnrollmentId 
                FROM Enrollments e
                JOIN CourseCycles cc ON e.CycleId = cc.CycleId
                WHERE cc.CourseId = @CourseId AND e.StudentId = @StudentId";

            SqlParameter[] parameters = {
                new SqlParameter("@CourseId", courseId),
                new SqlParameter("@StudentId", studentId)
            };

            object result = dbHelper.ExecuteScalar(query, parameters);

            if (result != null && result != DBNull.Value)
            {
                enrollmentId = Convert.ToInt32(result);
            }

            return enrollmentId;
        }
    }
}
