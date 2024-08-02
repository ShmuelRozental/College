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
                int selectedCourseId = Convert.ToInt32(dataGridViewCourses.SelectedRows[0].Cells["Id"].Value);
                int cycleId = CourseService.GetCycleByCourseId(selectedCourseId).Id;
                try
                {
                    if (!EnrollmentService.IsStudentEnrolled(student.Id, cycleId))
                    {
                        EnrollmentService.EnrollStudent(student.Id, cycleId);
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
            // Retrieve the list of courses
            List<Course> courses = CourseService.GetAllCourses();

            // Add a new column to the DataGridView for the additional data
            DataGridViewTextBoxColumn newColumn = new DataGridViewTextBoxColumn
            {
                Name = "coursePrice", // Set the name of the new column
                HeaderText = "Price of Current Cycle" // Set the header text of the new column
            };
            dataGridViewCourses.Columns.Add(newColumn);

            // Set the DataSource for the DataGridView
            dataGridViewCourses.DataSource = courses;

            // Populate the new column with data from another source
            foreach (DataGridViewRow row in dataGridViewCourses.Rows)
            {
                if (row.DataBoundItem is Course course)
                {
                    var courseCycle = CourseCycleService.GetCourseCycleById(course.Id);
                    if (courseCycle != null)
                    {
                        row.Cells["coursePrice"].Value = courseCycle.Price;
                    }

                }
            }
        }
    }
}
    


