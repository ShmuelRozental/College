using College.Models;
using College.services;
using College.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace College.View
{
    public partial class ViewCoursesForm : Form
    {
            private Student student;

            internal ViewCoursesForm(Student student)
            {
                InitializeComponent();
                this.student = student;
            }

            private void btnEnroll_Click(object sender, EventArgs e)
            {
            if (dataGridViewCourses.SelectedRows.Count > 0)
            {
                int selectedCycleId = Convert.ToInt32(dataGridViewCourses.SelectedRows[0].Cells["CycleId"].Value);

                try
                {
                    if (!EnrollmentService.IsStudentEnrolled(student.Id, selectedCycleId))
                    {
                        EnrollmentService.EnrollStudent(student.Id, selectedCycleId);
                        MessageBox.Show("Enrolled successfully!");
                    }
                    else
                    {
                        MessageBox.Show("You are already enrolled in this course cycle.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please select a course to enroll.");
            }
        }

        private void dataGridViewCourses_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ViewCoursesForm_Load(object sender, EventArgs e)
        {
            List<Course> courses = CourseService.GetAllCourses();
            dataGridViewCourses.DataSource = courses;

            Console.WriteLine($"Number of courses: {courses.Count}");
        }
    }
    
}

