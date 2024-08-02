using System;

namespace College.Models
{
    internal class CourseCycle
    {
        private int _id;
        private Course _course;
        private DateTime _startDate;
        private string _dayOfWeek;
        private TimeSpan _startTime;
        private TimeSpan _endTime;
        private decimal _price;

        public CourseCycle(int id, Course course, DateTime startDate, string dayOfWeek, TimeSpan startTime, TimeSpan endTime, decimal price)
        {
            _id = id;
            _course = course;
            _startDate = startDate;
            _dayOfWeek = dayOfWeek;
            _startTime = startTime;
            _endTime = endTime;
            _price = price;
        }

        public int Id { get { return _id; } set { _id = value; } }
        public Course Course { get { return _course; } set { _course = value; } }
        public DateTime StartDate { get { return _startDate; } set { _startDate = value; } }
        public string DayOfWeek { get { return _dayOfWeek; } set { _dayOfWeek = value; } }
        public TimeSpan StartTime { get { return _startTime; } set { _startTime = value; } }
        public TimeSpan EndTime { get { return _endTime; } set { _endTime = value; } }
        public decimal Price { get { return _price; } set { _price = value; } }
    }
}