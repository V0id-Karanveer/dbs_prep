using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;
namespace prac3
{
    public partial class Form1 : Form
    {
        OracleConnection conn;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        public void ConnectDB()
        {
            conn = new OracleConnection("DATA SOURCE=karan-helios300;USER ID=system;PASSWORD=karan");
            try
            {
                conn.Open();
                MessageBox.Show("Connected");
            }
            catch (Exception e1)
            {
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ConnectDB();
            OracleCommand comm1 = conn.CreateCommand();
            comm1.CommandType = CommandType.Text;
            comm1.CommandText = "select * from accident";
            DataSet ds = new DataSet();
            OracleDataAdapter da = new OracleDataAdapter(comm1.CommandText, conn);
            da.Fill(ds, "accident1");
            dataGridView1.DataSource = ds.Tables[0];
            comm1.Dispose();
            conn.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ConnectDB();
            OracleCommand comm2 = conn.CreateCommand();
            comm2.CommandType = CommandType.Text;
            comm2.CommandText = "insert into accident values(" + textBox1.Text + ",'" + textBox2.Text + "','" + textBox3.Text + "')";
            comm2.ExecuteNonQuery();
            comm2.Dispose();
            conn.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ConnectDB();
            OracleCommand comm3 = conn.CreateCommand();
            comm3.CommandType = CommandType.Text;
            comm3.CommandText = "delete from accident where report_number=" + textBox1.Text;
            comm3.ExecuteNonQuery();
            comm3.Dispose();
            conn.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ConnectDB();
            OracleCommand comm4 = conn.CreateCommand();
            comm4.CommandType = CommandType.Text;
            string inp = "update accident set ";
            if (!(string.IsNullOrEmpty(textBox2.Text)))
            {
                inp += "accd_date= '" + textBox2.Text +"'";
            }
            if (!(string.IsNullOrEmpty(textBox3.Text)))
            {
                inp += "location= '" + textBox3.Text +"'";
            }
            inp += " where report_number=" + textBox1.Text;
            comm4.CommandText = inp;
            comm4.ExecuteNonQuery();
            comm4.Dispose();
            conn.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ConnectDB();
            OracleCommand comm1 = conn.CreateCommand();
            comm1.CommandType = CommandType.StoredProcedure;
            comm1.CommandText = "totdmg";
            comm1.Parameters.Add("driver", OracleDbType.Varchar2, 20).Value = textBox1.Text;
            comm1.Parameters.Add("year", OracleDbType.Int16, 20).Value = textBox2.Text;
            comm1.Parameters.Add("totdmg", OracleDbType.Int16, 20).Direction = ParameterDirection.Output;
            try
            {
                comm1.ExecuteNonQuery();
                string res = comm1.Parameters["totdmg"].Value.ToString();
                textBox3.Text = res;
            }
            catch (Exception e1)
            {

            }
            finally
            {
                conn.Close();
            }

        }
        
    }
}
