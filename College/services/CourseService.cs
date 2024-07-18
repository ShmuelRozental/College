using College.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace College.Services
{
    internal static class CourseService
    {
        public static List<Course> GetAllCourses()
        {
            DatabaseHelper dbHelper = DatabaseHelper.Instance();
            string query = "SELECT c.CourseId, c.CourseName, cy.CycleId, cy.StartDate, cy.Price " +
                           "FROM Courses c " +
                           "JOIN CourseCycles cy ON c.CourseId = cy.CourseId";

            DataTable dt = dbHelper.ExecuteQuery(query);
            List<Course> courses = new List<Course>();

            foreach (DataRow row in dt.Rows)
            {
                Course course = new Course
                {
                    CourseId = Convert.ToInt32(row["CourseId"]),
                    Name = row["CourseName"].ToString(),
                    CycleId = Convert.ToInt32(row["CycleId"]),
                    Price = Convert.ToDecimal(row["Price"])
                };
                courses.Add(course);
            }

            return courses;
        }


        public static int GetCycleIdForCourse(int courseId)
        {
            DatabaseHelper dbHelper = DatabaseHelper.Instance();
            string query = "SELECT CycleId FROM CourseCycles WHERE CourseId = @CourseId";
            SqlParameter[] parameters = {
                new SqlParameter("@CourseId", courseId)
            };
            object result = dbHelper.ExecuteScalar(query, parameters);
            return result != DBNull.Value ? Convert.ToInt32(result) : -1;
        }

    }
}
