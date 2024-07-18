using College.Models;
using College.services;
using System;
using System.Windows.Forms;

namespace College.View
{
    public partial class PaymentForm : Form
    {
        private Student student;

        internal PaymentForm(Student student)
        {
            InitializeComponent();
            this.student = student;
        }

        private void PaymentForm_Load(object sender, EventArgs e)
        {
            // Initialize PaymentForm, load payment details or controls as needed
            lblStudentName.Text = student.Name;
            lblStudentEmail.Text = student.Email;
        }

        private void btnSavePayment_Click(object sender, EventArgs e)
        {
            
            decimal amount = decimal.Parse(txtAmount.Text); 
            DateTime paymentDate = DateTime.Now; 
            int enrollmentId = // Retrieve enrollment ID based on logic or user selection

            PaymentService.AddPayment(enrollmentId, amount, paymentDate);

            MessageBox.Show("Payment saved successfully!");
            this.Close(); // Close the PaymentForm after saving payment
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close(); // Close the PaymentForm if canceled
        }

        private void txtAmount_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
