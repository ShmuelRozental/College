using College.Models;
using College.services;
using College.Services;
using System;
using System.Windows.Forms;

namespace College.View
{
    public partial class PaymentForm : Form
    {
        private Student student;

        internal PaymentForm(Student student, int enrollmentId)
        {
            InitializeComponent();
            this.student = student;
        }

        private void PaymentForm_Load(object sender, EventArgs e)
        {
            
            lblStudentName.Text = student.Name;
            lblStudentEmail.Text = student.Email;
        }

        

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close(); // Close the PaymentForm if canceled
        }

        private void txtAmount_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void txtSelectCourseId_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void btnSavePayment_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSelectCourseId.Text))
            {
                MessageBox.Show("Please enter a course ID.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtAmount.Text))
            {
                MessageBox.Show("Please enter an amount.");
                return;
            }

            // Parse course ID
            if (!int.TryParse(txtSelectCourseId.Text, out int courseId))
            {
                MessageBox.Show("Invalid course ID. Please enter a valid number.");
                return;
            }

            // Retrieve the EnrollmentId based on courseId and studentId
            int enrollmentId = EnrollmentService.GetEnrollmentIdByCourseAndStudent(courseId, student.Id);

            if (enrollmentId == 0)
            {
                MessageBox.Show("No enrollment found for the selected course.");
                return;
            }

            // Parse amount
            if (!decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Invalid amount. Please enter a positive number.");
                return;
            }

            // Continue with payment processing
            DateTime paymentDate = DateTime.Now;

            PaymentService.AddPayment(enrollmentId, amount, paymentDate);

            MessageBox.Show("Payment saved successfully!");
            this.Close(); // Close the PaymentForm after saving payment
        }
    }
}
