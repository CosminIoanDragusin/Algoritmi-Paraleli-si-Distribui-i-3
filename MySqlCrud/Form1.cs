using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MySqlCrud
{
    public partial class Form1 : Form
    {
        public string id;
        public string nume, media;

        public Form1()
        {
            InitializeComponent();
        }

        public void UpdateInfo() 
        {
        
        }

        public void Display() 
        {
            DbStudent.DisplayStudent("SELECT id, nume, media from student", dataGridView1);
        }

        public void Clear() 
        {
            txtBoxNume.Text = txtBoxMedie.Text = string.Empty;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
           if(txtBoxNume.Text.Trim().Length < 2)
            {
                MessageBox.Show("Student name is Empty ( > 3).");
                return;
            }
            if (txtBoxMedie.Text.Trim().Length == 0)
            {
                MessageBox.Show("Student media is Empty ( > 1).");
                return;
            }
            if (btnAdd.Text == "Add") 
            {
                Student std = new Student(txtBoxNume.Text.Trim(), txtBoxMedie.Text.Trim());
                DbStudent.AddStudent(std);
                Clear();
            }
            Display();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            Display();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            MySqlConnection con = new MySqlConnection("datasource=localhost;port=3306;username=root;password=P4r0l4m34mysql;database=StudentC# ");
            con.Open();

            MySqlCommand cmd = new MySqlCommand("UPDATE student SET nume = @Nume, media = @Media where id = @Id", con);
            cmd.Parameters.AddWithValue("@Nume", txtBoxNume.Text);
            cmd.Parameters.AddWithValue("@Media", int.Parse(txtBoxMedie.Text));

            cmd.ExecuteNonQuery();
            MessageBox.Show("Updated Succesfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);


            con.Close();

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0) 
            {

                if (MessageBox.Show("Do you want to update ?", "Information", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    MySqlConnection con = new MySqlConnection("datasource=localhost;port=3306;username=root;password=P4r0l4m34mysql;database=StudentC# ");
                    con.Open();
                                      
                    MySqlCommand cmd = new MySqlCommand("UPDATE student SET nume = @Nume, media = @Media where id = @Id",con);
                    cmd.Parameters.AddWithValue("@Nume", txtBoxNume.Text);
                    cmd.Parameters.AddWithValue("@Media", int.Parse(txtBoxMedie.Text));
                    
                         cmd.ExecuteNonQuery();
                        MessageBox.Show("Updated Succesfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    
                    con.Close();
                }

                return;
            }

            if (e.ColumnIndex == 1)
            {
                if (MessageBox.Show("Do you want to delete ?", "Information", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information) == DialogResult.Yes) 
                {
                    DbStudent.DeleteStudent(dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString());
                    Display();
                }
                return;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();  
        }
    }
}
