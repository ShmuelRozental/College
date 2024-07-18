using College.Models;
using College.services;
using College.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace College
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btnStudentCard.Visible = false;
            btnViewCourses.Visible = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            {
                string name = txtName.Text;
                string email = txtEmail.Text;

                Student existingStudent = StudentService.GetStudentByEmail(email);
                if (existingStudent == null)
                {
                    Student newStudent = new Student(name, email);
                    StudentService.AddStudent(newStudent);
                    MessageBox.Show("Student added successfully!");

                    // Show the choice buttons and hide the save button
                    btnStudentCard.Visible = true;
                    btnViewCourses.Visible = true;
                    btnSave.Visible = false;

                    // Store the new student for later use
                    btnStudentCard.Tag = newStudent;
                    btnViewCourses.Tag = newStudent;
                }
                else
                {
                    MessageBox.Show("Student already exists. Redirecting to choice.");

                    // Show the choice buttons and hide the save button
                    btnStudentCard.Visible = true;
                    btnViewCourses.Visible = true;
                    btnSave.Visible = false;

                    // Store the existing student for later use
                    btnStudentCard.Tag = existingStudent;
                    btnViewCourses.Tag = existingStudent;
                }
            }
        }

        private void btnStudentCard_Click(object sender, EventArgs e)
        {
            // Retrieve the student object from the button's Tag property
            Student student = btnStudentCard.Tag as Student;
            if (student != null)
            {
                StudentCardForm studentCardForm = new StudentCardForm(student);
                studentCardForm.Show();
            }
        }

        private void btnViewCourses_Click(object sender, EventArgs e)
        {
            // Retrieve the student object from the button's Tag property
            Student student = btnViewCourses.Tag as Student;
            if (student != null)
            {
                ViewCoursesForm viewCoursesForm = new ViewCoursesForm(student);
                viewCoursesForm.Show();
            }
        }
    }
}
