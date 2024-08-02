using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace College.Models
{
    internal class Student
    {
        private int _id;
        private string _name;
        private string _email;
        private decimal _balance;

        public Student(string name, string email) 
        {
            _name = name;
            _email = email;
        }

        public Student(int id,string name, string email)
        {
            _id = id;
            _name = name;
            _email = email;
        }
        public int Id 
            { get { return _id; } set { _id = value; } }
        public string Name
            {get { return _name; } set { _name = value; } }
        public string Email
            { get { return _email; } set { _email = value; } }
        public decimal Balance { get { return _balance; } set {_balance = value; } }
    }
}
