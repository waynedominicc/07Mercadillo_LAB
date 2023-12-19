using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _07Mercadillo_LAB
{
    public partial class FrmUpdateMember : Form
    {

        private ClubRegistrationQuery clubRegistrationQuery;
        public FrmUpdateMember()
        {
            InitializeComponent();
            clubRegistrationQuery = new ClubRegistrationQuery();
            LoadStudentIDs();
            cbStudentid.SelectedIndexChanged += cbStudentid_SelectedIndexChanged;
            cbStudentid.Click += cbStudentid_SelectedIndexChanged;
        }
        private void LoadStudentIDs()
        {
            cbStudentid.DataSource = clubRegistrationQuery.GetStudentIDs();
        }
        private void btnConfirm_Click(object sender, EventArgs e)
        {

            string selectedStudentID = cbStudentid.SelectedItem.ToString();
            string updatedFirstName = txtFname.Text;
            string updatedLastName = txtLname.Text;
            string updatedMiddleName = txtMname.Text;
            string updatedGender = cbGender.SelectedItem.ToString();
            int updatedAge = int.Parse(txtAge.Text);
            string updatedProgram = cbProgram.SelectedItem.ToString();

            clubRegistrationQuery.UpdateStudentInfo(selectedStudentID, updatedFirstName, updatedLastName, updatedMiddleName, updatedAge, updatedGender, updatedProgram);

        }

        private void cbStudentid_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedStudentID = cbStudentid.SelectedItem.ToString();
            Student student = clubRegistrationQuery.GetStudentInfo(selectedStudentID);

            txtFname.Text = student.FirstName;
            txtLname.Text = student.LastName;
            txtMname.Text = student.MiddleName;
            cbGender.Text = student.Gender;
            txtAge.Text = student.Age.ToString();
            cbProgram.Text = student.Program;
        }
    }
}
