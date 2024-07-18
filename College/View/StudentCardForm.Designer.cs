namespace College.View
{
    partial class StudentCardForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblTotalBalance = new System.Windows.Forms.Label();
            this.lblStudentEmail = new System.Windows.Forms.Label();
            this.lblStudentName = new System.Windows.Forms.Label();
            this.listBoxCourses = new System.Windows.Forms.ListBox();
            this.btmPayment = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTotalBalance
            // 
            this.lblTotalBalance.AutoSize = true;
            this.lblTotalBalance.Location = new System.Drawing.Point(393, 336);
            this.lblTotalBalance.Name = "lblTotalBalance";
            this.lblTotalBalance.Size = new System.Drawing.Size(0, 20);
            this.lblTotalBalance.TabIndex = 1;
            this.lblTotalBalance.Click += new System.EventHandler(this.lblTotalBalance_Click);
            // 
            // lblStudentEmail
            // 
            this.lblStudentEmail.AutoSize = true;
            this.lblStudentEmail.Location = new System.Drawing.Point(106, 30);
            this.lblStudentEmail.Name = "lblStudentEmail";
            this.lblStudentEmail.Size = new System.Drawing.Size(0, 20);
            this.lblStudentEmail.TabIndex = 2;
            // 
            // lblStudentName
            // 
            this.lblStudentName.AutoSize = true;
            this.lblStudentName.Location = new System.Drawing.Point(32, 30);
            this.lblStudentName.Name = "lblStudentName";
            this.lblStudentName.Size = new System.Drawing.Size(0, 20);
            this.lblStudentName.TabIndex = 3;
            // 
            // listBoxCourses
            // 
            this.listBoxCourses.FormattingEnabled = true;
            this.listBoxCourses.ItemHeight = 20;
            this.listBoxCourses.Location = new System.Drawing.Point(157, 70);
            this.listBoxCourses.Name = "listBoxCourses";
            this.listBoxCourses.Size = new System.Drawing.Size(501, 244);
            this.listBoxCourses.TabIndex = 4;
            this.listBoxCourses.SelectedIndexChanged += new System.EventHandler(this.listBoxCourses_SelectedIndexChanged);
            // 
            // btmPayment
            // 
            this.btmPayment.Location = new System.Drawing.Point(30, 380);
            this.btmPayment.Name = "btmPayment";
            this.btmPayment.Size = new System.Drawing.Size(135, 43);
            this.btmPayment.TabIndex = 5;
            this.btmPayment.Text = "click to pay";
            this.btmPayment.UseVisualStyleBackColor = true;
            this.btmPayment.Click += new System.EventHandler(this.btmPayment_Click);
            // 
            // StudentCardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btmPayment);
            this.Controls.Add(this.listBoxCourses);
            this.Controls.Add(this.lblStudentName);
            this.Controls.Add(this.lblStudentEmail);
            this.Controls.Add(this.lblTotalBalance);
            this.Name = "StudentCardForm";
            this.Text = "StudentCardForm";
            this.Load += new System.EventHandler(this.StudentCardForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblTotalBalance;
        private System.Windows.Forms.Label lblStudentEmail;
        private System.Windows.Forms.Label lblStudentName;
        private System.Windows.Forms.ListBox listBoxCourses;
        private System.Windows.Forms.Button btmPayment;
    }
}