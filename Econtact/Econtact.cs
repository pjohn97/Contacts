using Econtact.econtactClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Econtact
{
    public partial class Econtact : Form
    {
        public Econtact()
        {
            InitializeComponent();
        }
        econtactClass c = new econtactClass();
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //Get the value from input fields
            c.FirstName = txtBoxFirstName.Text;
            c.LastName = txtBoxLastName.Text;
            c.ContactNo = txtBoxContactNo.Text;
            c.Address = txtBoxAddress.Text;
            c.Gender = cmbGender.Text;

            //Inserting data into database
            bool success = c.Insert(c);
            if(success==true)
            {
                //Successfully inserted
                MessageBox.Show("New Contact Successfully inserted");
                //call the clear method
                Clear();
            }
            else
            {
                MessageBox.Show("Failed to create new contact. Try again");
            }

            //Load data on DataGridView
            DataTable dt = c.Select();
            dgvContactList.DataSource = dt;
        }

        private void Econtact_Load(object sender, EventArgs e)
        {
            //Load data on DataGridView
            DataTable dt = c.Select();
            dgvContactList.DataSource = dt;
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
        
        //Method to clear fields
        public void Clear()
        {
            txtBoxContactID.Text = "";
            txtBoxFirstName.Text = "";
            txtBoxLastName.Text = "";
            txtBoxContactNo.Text = "";
            txtBoxAddress.Text = "";
            cmbGender.Text = "";

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //get data from text boxes
            c.ContactID = int.Parse(txtBoxContactID.Text);
            c.FirstName = txtBoxFirstName.Text;
            c.LastName = txtBoxLastName.Text;
            c.ContactNo = txtBoxContactNo.Text;
            c.Address = txtBoxAddress.Text;
            c.Gender = cmbGender.Text;
            //Update data in database
            bool success = c.update(c);
            if(success==true)
            {
                MessageBox.Show("Contact has been updated successfully");
                //Load data on DataGridView
                DataTable dt = c.Select();
                dgvContactList.DataSource = dt;
                //Call clear method
                Clear();
            }
            else
            {
                MessageBox.Show("Failed to update contact. Please try again");
            }
           
        }

        private void dgvContactList_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //get the data from data grid and load it to the text boxes
            //identify row on which mouse is clicked
            int rowIndex = e.RowIndex;
            txtBoxContactID.Text = dgvContactList.Rows[rowIndex].Cells[0].Value.ToString();
            txtBoxFirstName.Text = dgvContactList.Rows[rowIndex].Cells[1].Value.ToString();
            txtBoxLastName.Text = dgvContactList.Rows[rowIndex].Cells[2].Value.ToString();
            txtBoxContactNo.Text = dgvContactList.Rows[rowIndex].Cells[3].Value.ToString();
            txtBoxAddress.Text = dgvContactList.Rows[rowIndex].Cells[4].Value.ToString();
            cmbGender.Text = dgvContactList.Rows[rowIndex].Cells[5].Value.ToString();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //Get data from TextBox
            c.ContactID = int.Parse(txtBoxContactID.Text);
            bool success = c.Delete(c);
            if(success==true)
            {
                MessageBox.Show("Contact successfully deleted");
                //Load data on DataGridView
                DataTable dt = c.Select();
                dgvContactList.DataSource = dt;
                Clear();
            }
            else
            {
                MessageBox.Show("Failed to delete contact");
            }
        }
        static string myconnstr = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;
        public void txtBoxSearch_TextChanged(object sender, EventArgs e)
        {
            
            string keyword = txtBoxSearch.Text;

            SqlConnection conn = new SqlConnection(myconnstr);
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM tbl_contact WHERE FirstName LIKE '%" + keyword + "%' OR LastName LIKE '%" + keyword + "%' OR Address LIKE '%" + keyword + "%'", conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dgvContactList.DataSource = dt;
        }
    }
}
