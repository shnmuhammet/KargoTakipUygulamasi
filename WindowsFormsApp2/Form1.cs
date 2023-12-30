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

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-LOTOEV7\SQLEXPRESS;Initial Catalog=kargoTakipp;Persist Security Info=True;User ID=shn;Password=1");

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("select*from sehir order by sehir_key asc", baglanti);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cmbSehir.Items.Add(dr[1]);
            }
            baglanti.Close();


        }

        private void cmbSehir_SelectedIndexChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("select*from ilce where ilce_sehirkey=@p1",baglanti);
            cmd.Parameters.AddWithValue("@p1", cmbSehir.SelectedIndex + 1);
            SqlDataReader dr = cmd.ExecuteReader();
            cmbİlce.Items.Clear();
            cmbİlce.Text = " ";
            while (dr.Read())
            {
                cmbİlce.Items.Add(dr[1]);
            }

            baglanti.Close();
        }

        private void cmbİlce_SelectedIndexChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("select*from mahalle where mahalle_ilcekey in (select ilce_key from ilce where ilce_title like '" + cmbİlce.Text + "') ", baglanti);
            SqlDataReader rdr = cmd.ExecuteReader();
            cmbMahalle.Items.Clear();
            cmbMahalle.Text = " ";
            while (rdr.Read())
            {
                cmbMahalle.Items.Add(rdr[1]);
            }


            baglanti.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                baglanti.Open();




                baglanti.Close();
            }
           

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                grpBireysel.Enabled = true;
                grpKurumsal.Enabled=false;
               
            }

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                grpKurumsal.Enabled = true;
                grpBireysel.Enabled = false;
            }

        }
    }
}
