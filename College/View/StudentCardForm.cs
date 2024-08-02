using College.Models;
using College.Services; 
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

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
            decimal totalBalance = 0;
            DatabaseHelper dbHelper = DatabaseHelper.Instance();

    
            //get a list of all enrollment for student
            List<Enrollment> enrolledCourses = EnrollmentService.GetAllStudentEnrollments(student.Id);
            

            // Populate ListBox with enrolled courses
            listBoxCourses.Items.Clear();
            foreach (Enrollment enrollment in enrolledCourses)
            {
                // method to get course details based on CycleId
                string courseName = enrollment.CourseCycle.Course.Name;
                decimal coursePrice = enrollment.CourseCycle.Price;
                totalBalance += enrollment.Balance;

                ListViewItem item = new ListViewItem(enrollment.EnrollmentId.ToString());
                item.SubItems.Add(courseName);
                item.SubItems.Add(coursePrice.ToString());
                item.SubItems.Add(enrollment.Balance.ToString());

                listViewCourses.Items.Add(item);
            }

            // Display total balance
            lblTotalBalance.Text = $"Total Balance: {totalBalance}";
        }




     

        private void lblTotalBalance_Click(object sender, EventArgs e)
        {

        }

        private void listBoxCourses_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btmPayment_Click(object sender, EventArgs e)
        {
            if (listViewCourses.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = listViewCourses.SelectedItems[0];
                int enrollmentId = int.Parse(selectedItem.SubItems[0].Text);

                PaymentForm paymentForm = new PaymentForm(student, enrollmentId);
                paymentForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("אנא בחר קורס מרשימת הקורסים לתשלום.");
            }
        }

        private void listViewCourses_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
