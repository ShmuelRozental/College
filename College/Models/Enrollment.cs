using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace College.Models
{
    public class Enrollment
    {
        public int EnrollmentId { get; set; }
        public int StudentId { get; set; }
        public int CycleId { get; set; }
        public decimal Balance { get; set; }
        public string CourseName { get; set; } // Assuming you want to display course name in UI

        public Enrollment(int enrollmentId, int studentId, int cycleId, decimal balance)
        {
            EnrollmentId = enrollmentId;
            StudentId = studentId;
            CycleId = cycleId;
            Balance = balance;
        }

        // Optional constructor for convenience if you have the course name
        public Enrollment(int enrollmentId, int studentId, int cycleId, decimal balance, string courseName)
            : this(enrollmentId, studentId, cycleId, balance)
        {
            CourseName = courseName;
        }
    }
}
