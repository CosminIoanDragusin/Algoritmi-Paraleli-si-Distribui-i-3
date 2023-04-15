using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MySqlCrud
{
    class DbStudent
    {
        public static MySqlConnection GetConnection() 
        {
            string sql ="datasource=localhost;port=3306;username=root;password=P4r0l4m34mysql;database=StudentC# ";
            MySqlConnection con = new MySqlConnection(sql);
            try
            {
                con.Open();
            }catch(MySqlException ex) 
            {
                MessageBox.Show("MySql Connection! \n" + ex.Message ,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            return con;
        }

        public static void AddStudent(Student std) 
        {
            string sql = "INSERT into student values(NULL,@Nume,@Media)";
            MySqlConnection con = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@Nume", MySqlDbType.VarChar).Value = std.Nume;
            cmd.Parameters.Add("@Media", MySqlDbType.Int32).Value = std.Media;

            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Added Succesfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(MySqlException ex) 
            {
                MessageBox.Show("Student didn't insert! \n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            con.Close();
        }

        public static void UpdateStudent(Student std,string id)
        {
            string sql = "UPDATE student SET nume = @Nume, media = @Media where id = @Id";
            MySqlConnection con = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("nume", MySqlDbType.VarChar).Value = std.Nume;
            cmd.Parameters.Add("media", MySqlDbType.Int32).Value = std.Media;

            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Updated Succesfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Student didn't update! \n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            con.Close();
        }

        public static void DeleteStudent(string id)
        {
            string sql = "delete from student  where id = @Id";
            MySqlConnection con = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("id", MySqlDbType.Int32).Value = id;

            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Deleted Succesfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Student didn't deleted! \n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            con.Close();
        }

        public static void DisplayStudent(string query, DataGridView dgv) 
        {
            string sql = query;
            MySqlConnection con = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, con);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataTable tbl = new DataTable();
            adp.Fill(tbl);
            dgv.DataSource = tbl;
            con.Close();
        }

    }
}
