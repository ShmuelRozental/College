using College.Models;
using College.services; 
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace College.View
{
    public partial class StudentCardForm : Form
    {
        private Student student;
         internal StudentCardForm(Student student)
        {
            InitializeComponent();
            this.student = student;
        }

        private void StudentCardForm_Load(object sender, EventArgs e)
        {
            lblStudentName.Text = student.Name;
            lblStudentEmail.Text = student.Email;

            LoadStudentCoursesAndBalance();
        }

        private void LoadStudentCoursesAndBalance()
        {
            List<Course> enrolledCourses = EnrollmentService.GetEnrolledCoursesByStudentId(student.Id);
            decimal totalBalance = EnrollmentService.GetTotalBalanceByStudentId(student.Id);

            Console.WriteLine($"Total Balance: {totalBalance}");

            // Populate ListBox with enrolled courses
            listBoxCourses.Items.Clear();
            foreach (Course course in enrolledCourses)
            {
                listBoxCourses.Items.Add($"{course.Name} - {course.Price:C}");
            }

            // Display total balance
            lblTotalBalance.Text = $"Total Balance: {totalBalance:C}";
        }




     

        private void lblTotalBalance_Click(object sender, EventArgs e)
        {

        }

        private void listBoxCourses_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btmPayment_Click(object sender, EventArgs e)
        {
            PaymentForm paymentForm = new PaymentForm(student);
            paymentForm.ShowDialog();
        }
    }
}
