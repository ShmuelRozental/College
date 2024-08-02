using System;

namespace College.Models
{
    internal class Enrollment
    {
        // Private fields 
        private int enrollmentId;
        private Student student;
        private CourseCycle courseCycle;
        private decimal balance;

        // Public properties
        public int EnrollmentId
        {
            get { return enrollmentId; }
            set { enrollmentId = value; }
        }

        public Student Student
        {
            get { return student; }
            set { student = value; }
        }

        public CourseCycle CourseCycle
        {
            get { return courseCycle; }
            set { courseCycle = value; }
        }

        public decimal Balance
        {
            get { return balance; }
            set { balance = value; }
        }

        // Constructor
        public Enrollment(int enrollmentId, Student student, CourseCycle courseCycle, decimal balance)
        {
            EnrollmentId = enrollmentId;
            Student = student;
            CourseCycle = courseCycle;
            Balance = balance;
        }
    }
}