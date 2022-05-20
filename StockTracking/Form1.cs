using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace StockTracking
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //Data Source=DESKTOP-MMA8671;Initial Catalog=Stock_Tracking;Integrated Security=True

        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-MMA8671;Initial Catalog=Stock_Tracking;Integrated Security=True");

        void List()
        {

            conn.Open();
            SqlCommand cmd = new SqlCommand("Select * From Tbl_Oil_Info where OilType='Kurşunsuz Benzin 95'", conn);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Gassoline.Text = dr[3].ToString();
                progressBar1.Value = int.Parse(dr[4].ToString());
                G.Text = dr[4].ToString();
            }
            conn.Close();

            conn.Open();
            SqlCommand cmd1 = new SqlCommand("Select * From Tbl_Oil_Info where OilType='Euro Diesel'", conn);
            SqlDataReader dr1 = cmd1.ExecuteReader();
            while (dr1.Read())
            {
                euroDiesel.Text = dr1[3].ToString();
                progressBar2.Value = int.Parse(dr1[4].ToString());
                ED.Text = dr1[4].ToString();
            }
            conn.Close();

            conn.Open();
            SqlCommand cmd2 = new SqlCommand("Select * From Tbl_Oil_Info where OilType='Eco Force Diesel'", conn);
            SqlDataReader dr2 = cmd2.ExecuteReader();
            while (dr2.Read())
            {
                ecoDiesel.Text = dr2[3].ToString();
                progressBar3.Value = int.Parse(dr2[4].ToString());
                ECD.Text = dr2[4].ToString();
            }
            conn.Close();

            conn.Open();
            SqlCommand cmd3 = new SqlCommand("Select * From Tbl_Oil_Info where OilType='PoGaz'", conn);
            SqlDataReader dr3 = cmd3.ExecuteReader();
            while (dr3.Read())
            {
                Gas.Text = dr3[3].ToString();
                progressBar4.Value = int.Parse(dr3[4].ToString());
                GA.Text = dr3[4].ToString();
            }
            conn.Close();

            conn.Open();
            SqlCommand cmd4 = new SqlCommand("Select * From Tbl_Cash", conn);
            SqlDataReader dr4 = cmd4.ExecuteReader();
            while (dr4.Read())
            {
                LblCash.Text = dr4[0].ToString();

            }
            conn.Close();


        }
        private void Form1_Load(object sender, EventArgs e)
        {

            List();


        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            double gassoline, liter, amount;
            gassoline = Convert.ToDouble(Gassoline.Text);
            liter = Convert.ToDouble(numericUpDown1.Value);
            amount = gassoline * liter;
            txtGassoline.Text = amount.ToString();


        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            double EuroDiesel, liter, amount;
            EuroDiesel = Convert.ToDouble(euroDiesel.Text);
            liter = Convert.ToDouble(numericUpDown2.Value);
            amount = EuroDiesel * liter;
            txtEuDiesel.Text = amount.ToString();
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            double EcoDiesel, liter, amount;
            EcoDiesel = Convert.ToDouble(ecoDiesel.Text);
            liter = Convert.ToDouble(numericUpDown3.Value);
            amount = EcoDiesel * liter;
            txtEcDiesel.Text = amount.ToString();
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            double lpg, liter, amount;
            lpg = Convert.ToDouble(Gas.Text);
            liter = Convert.ToDouble(numericUpDown4.Value);
            amount = lpg * liter;
            txtGas.Text = amount.ToString();

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (numericUpDown1.Value != 0)
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("insert into Tbl_Sale_Moves (Plate,ProductType,Liter,Price) values(@p1,@p2,@p3,@p4)", conn);
                cmd.Parameters.AddWithValue("@p1", txtPlate.Text);
                cmd.Parameters.AddWithValue("@p2", "Kursunsun Benzin");
                cmd.Parameters.AddWithValue("@p3", numericUpDown1.Value);
                cmd.Parameters.AddWithValue("@p4", float.Parse(txtGassoline.Text));
                cmd.ExecuteNonQuery();
                conn.Close();

                conn.Open();
                SqlCommand command1 = new SqlCommand("update Tbl_Cash set Amount=Amount+@p1", conn);
                float x = float.Parse(txtGassoline.Text);
                command1.Parameters.AddWithValue("@p1", x);
                command1.ExecuteNonQuery();
                conn.Close();


                conn.Open();
                SqlCommand cmd2 = new SqlCommand("update Tbl_Oil_Info set Stock=Stock-@p1 where OilType='Kurşunsuz Benzin 95' ", conn);
                cmd2.Parameters.AddWithValue("@p1", numericUpDown1.Value);
                cmd2.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Satış Yapıldı");
                List();


            }

            if (numericUpDown2.Value != 0)
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("insert into Tbl_Sale_Moves (Plate,ProductType,Liter,Price) " +
                    "values(@p1,@p2,@p3,@p4)", conn);
                cmd.Parameters.AddWithValue("@p1", txtPlate.Text);
                cmd.Parameters.AddWithValue("@p2", "Euro Dizel");
                cmd.Parameters.AddWithValue("@p3", numericUpDown2.Value);
                cmd.Parameters.AddWithValue("@p4", float.Parse(txtEuDiesel.Text));
                cmd.ExecuteNonQuery();
                conn.Close();
                conn.Open();

                SqlCommand cmd1 = new SqlCommand("update Tbl_Cash set Amount=Amount+@p1", conn);
                cmd1.Parameters.AddWithValue("@p1", float.Parse(txtEuDiesel.Text));
                cmd1.ExecuteNonQuery();
                conn.Close();


                conn.Open();
                SqlCommand cmd2 = new SqlCommand("update Tbl_Oil_Info set Stock=Stock-@p1 where OilType='Euro Diesel'", conn);
                cmd2.Parameters.AddWithValue("@p1", numericUpDown2.Value);
                cmd2.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Satış Yapıldı");
                List();


            }
            if (numericUpDown3.Value != 0)
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("insert into Tbl_Sale_Moves (Plate,ProductType,Liter,Price) " +
                    "values(@p1,@p2,@p3,@p4)", conn);
                cmd.Parameters.AddWithValue("@p1", txtPlate.Text);
                cmd.Parameters.AddWithValue("@p2", "Eco Dizel");
                cmd.Parameters.AddWithValue("@p3", numericUpDown3.Value);
                cmd.Parameters.AddWithValue("@p4", float.Parse(txtEcDiesel.Text));
                cmd.ExecuteNonQuery();
                conn.Close();
                conn.Open();

                SqlCommand cmd1 = new SqlCommand("update Tbl_Cash set Amount=Amount+@p1", conn);
                cmd1.Parameters.AddWithValue("@p1", float.Parse(txtEcDiesel.Text));
                cmd1.ExecuteNonQuery();
                conn.Close();


                conn.Open();
                SqlCommand cmd2 = new SqlCommand("update Tbl_Oil_Info set Stock=Stock-@p1 where OilType='Euro Diesel'", conn);
                cmd2.Parameters.AddWithValue("@p1", numericUpDown3.Value);
                cmd2.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Satış Yapıldı");
                List();


            }
            if (numericUpDown4.Value != 0)
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("insert into Tbl_Sale_Moves (Plate,ProductType,Liter,Price) " +
                    "values(@p1,@p2,@p3,@p4)", conn);
                cmd.Parameters.AddWithValue("@p1", txtPlate.Text);
                cmd.Parameters.AddWithValue("@p2", "LPG");
                cmd.Parameters.AddWithValue("@p3", numericUpDown4.Value);
                cmd.Parameters.AddWithValue("@p4", float.Parse(txtGas.Text));
                cmd.ExecuteNonQuery();
                conn.Close();
                conn.Open();

                SqlCommand cmd1 = new SqlCommand("update Tbl_Cash set Amount=Amount+@p1", conn);
                cmd1.Parameters.AddWithValue("@p1", float.Parse(txtGas.Text));
                cmd1.ExecuteNonQuery();
                conn.Close();


                conn.Open();
                SqlCommand cmd2 = new SqlCommand("update Tbl_Oil_Info set Stock=Stock-@p1 where OilType='Euro Diesel'", conn);
                cmd2.Parameters.AddWithValue("@p1", numericUpDown4.Value);
                cmd2.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Satış Yapıldı");
                List();


            }
        }
    }
}
