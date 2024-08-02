using College.Models;
using College.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace College.services
{
  
        internal class CourseCycleService
        {
            public static CourseCycle GetCourseCycleById(int courseCycleId)
            {
                DatabaseHelper dbHelper = DatabaseHelper.Instance();
                string query = "SELECT * FROM CourseCycles WHERE CycleId = @CycleId";
                SqlParameter[] parameters = {
                new SqlParameter("@CycleId", courseCycleId)
            };

                CourseCycle courseCycle = null;

                try
                {
                    using (SqlDataReader reader = dbHelper.ExecuteReader(query, parameters))
                    {
                        if (reader.Read())
                        {
                            int courseId = reader.GetInt32(reader.GetOrdinal("CourseId"));
                            Course course = CourseService.GetCourseById(courseId);

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
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }

                return courseCycle;
            }
        }
}
