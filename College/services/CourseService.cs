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
            string query = "SELECT * FROM Courses ";

            DataTable dt = dbHelper.ExecuteQuery(query);
            List<Course> courses = new List<Course>();

            foreach (DataRow row in dt.Rows)
            {
                Course course = new Course
                {
                    Id = Convert.ToInt32(row["CourseId"]),
                    Name = row["CourseName"].ToString(),
                    
                };
                courses.Add(course);
            }

            return courses;
        }

        public static CourseCycle GetCycleByCourseId(int courseId)
        {
            DatabaseHelper dbHelper = DatabaseHelper.Instance();
            string query = "SELECT * FROM CourseCycles WHERE CourseId = @CourseId";
            SqlParameter[] parameters = {
        new SqlParameter("@CourseId", courseId)
    };

            CourseCycle courseCycle = null;

            try
            {
                using (SqlDataReader reader = dbHelper.ExecuteReader(query, parameters))
                {
                    if (reader.Read())
                    {
                        Course course = GetCourseById(reader.GetInt32(reader.GetOrdinal("CourseId")));

                        courseCycle = new CourseCycle(
                            reader.GetInt32(reader.GetOrdinal("CycleId")),
                            course,
                            reader.GetDateTime(reader.GetOrdinal("StartDate")),
                            reader.GetString(reader.GetOrdinal("DaysOfWeek")),
                            reader.GetTimeSpan(reader.GetOrdinal("StartTime")),
                            reader.GetTimeSpan(reader.GetOrdinal("EndTime")),
                            reader.GetDecimal(reader.GetOrdinal("Price"))
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return courseCycle;
        }

        public static Course GetCourseById(int courseId)
        {
            DatabaseHelper dbHelper = DatabaseHelper.Instance();
            string query = "SELECT * FROM Courses WHERE CourseId = @CourseId";
            SqlParameter[] parameters = {
            new SqlParameter("@CourseId", courseId)
            };

            Course course = null;

            try
            {
                using (SqlDataReader reader = dbHelper.ExecuteReader(query, parameters))
                {
                    if (reader.Read())
                    {
                        course = new Course
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("CourseId")),
                            Name = reader.GetString(reader.GetOrdinal("CourseName"))
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return course;
        }
    }

}
