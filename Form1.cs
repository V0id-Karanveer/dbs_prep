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

namespace Connector
{
    public partial class Form1 : Form
    {
        OracleConnection conn;
        public Form1()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ConnectDB();
            OracleCommand comm1 = conn.CreateCommand();
            string commstr = "update STUDENT ";

            int flag = 0;
            if (!string.IsNullOrEmpty(textBox2.Text))
            {
               commstr = commstr + " set name=" + "'" + textBox2.Text + "'";
               
            }
            else if (!string.IsNullOrEmpty(textBox3.Text))
            {
                
                    commstr = commstr + " set major=" + "'" + textBox3.Text + "'";
                
                


            }
            else if (!string.IsNullOrEmpty(textBox4.Text))
            {
                
                    commstr = commstr + " set bdate=" + "'" + textBox4.Text + "'";
                
               



            }
            commstr = commstr + " where regno=" + "'" + textBox1.Text + "'";

            comm1.Connection = conn;
            comm1.CommandText = commstr;
            comm1.CommandType = CommandType.Text;

            comm1.ExecuteNonQuery();
            MessageBox.Show("Updated");
            comm1.Dispose();
            conn.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        public void ConnectDB()
        {
            conn = new OracleConnection("DATA SOURCE=172.16.54.24:1521/ictorcl;USER ID=it088;PASSWORD=student");
            try{
                conn.Open();
                MessageBox.Show("Connected");
            }
            catch (Exception e1){

            }
        }

        private void ins_Click(object sender, EventArgs e)
        {
            ConnectDB();

            OracleCommand command1 = conn.CreateCommand();
            command1.CommandText = "insert into student values(" + "'" + textBox1.Text + "'" + "," + "'" + textBox2.Text + "'" + "," + "'" + textBox3.Text + "'" + "," +"'" +textBox4.Text+"'" + ")";
            command1.CommandType = CommandType.Text;
            command1.ExecuteNonQuery();
            MessageBox.Show("Inserted");
            command1.Dispose();
            conn.Close();

        }

        private void fetch_Click(object sender, EventArgs e)
        {
            ConnectDB();
            OracleCommand comm1 = conn.CreateCommand();
            comm1.Connection = conn;
            comm1.CommandText = "select * from STUDENT";
            comm1.CommandType = CommandType.Text;

            OracleDataAdapter da = new OracleDataAdapter(comm1.CommandText, conn);

            

            DataSet ds = new DataSet();  
            da.Fill(ds, "student");   

            dataGridView1.DataSource = ds.Tables[0];
            comm1.Dispose();
            conn.Close();
        }

        private void sea_Click(object sender, EventArgs e)
        {
            ConnectDB();
            OracleCommand comm1 = conn.CreateCommand();
            string commstr = "select * from STUDENT where";
           
            int flag = 0;
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                commstr = commstr + " regno=" + textBox1.Text;
                flag = 1;
            }
            if (!string.IsNullOrEmpty(textBox2.Text))
            {
                if (flag == 1)
                {
                    commstr = commstr + " and name=" + "'"+textBox2.Text+"'";
                }
                else
                {
                    commstr = commstr + " name=" + "'"+textBox2.Text+"'";
                }
                flag = 1;

                
            }
            if (!string.IsNullOrEmpty(textBox3.Text))
            {
                if (flag == 1)
                {
                    commstr = commstr + " and major=" + "'" + textBox3.Text + "'";
                }
                else
                {
                    commstr = commstr + " major=" + "'" + textBox3.Text + "'";
                }
                flag = 1;


            }
            if (!string.IsNullOrEmpty(textBox4.Text))
            {   
                if (flag == 1)
                {
                    commstr = commstr + " and bdate=" + "'" + textBox4.Text + "'";
                }
                else
                {
                    commstr = commstr + " bdate=" + "'" + textBox4.Text + "'";
                }
            


            }
         
           comm1.Connection = conn;
           comm1.CommandText = commstr;
           comm1.CommandType = CommandType.Text;

            OracleDataAdapter da = new OracleDataAdapter(comm1.CommandText, conn);



            DataSet ds = new DataSet();
            da.Fill(ds, "student");

            dataGridView1.DataSource = ds.Tables[0];
            comm1.Dispose();
            conn.Close();
        }

        private void del_Click(object sender, EventArgs e)
        {
            ConnectDB();

            OracleCommand command1 = conn.CreateCommand();
            command1.CommandText = "delete from student where regno="+ "'" + textBox1.Text + "'";
            command1.CommandType = CommandType.Text;
            command1.ExecuteNonQuery();
            MessageBox.Show("Deleted");
            command1.Dispose();
            conn.Close();
        }
    }
}
