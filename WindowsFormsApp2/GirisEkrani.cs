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
    public partial class GirisEkrani : Form
    {
        public GirisEkrani()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-LOTOEV7\SQLEXPRESS;Initial Catalog=kargoTakipp;Persist Security Info=True;User ID=shn;Password=1");
        

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string kullanici = txtKullaniciAdi.Text;
            ŞifremiUnuttum şifremiUnuttum = new ŞifremiUnuttum();
            şifremiUnuttum.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string kullanici, sifre;
            kullanici = txtKullaniciAdi.Text;
            sifre = txtSifre.Text;

            baglanti.Open();
            SqlCommand cmd = new SqlCommand("select*from personel where kullaniciAdi='"+kullanici+"' and sifre='"+sifre+"'",baglanti);
            SqlDataReader rdr = cmd.ExecuteReader();
            
            if (rdr.Read())
            {
                
                MessageBox.Show("Hoş Geldiniz" + kullanici + "");
                Form1 form1 = new Form1(kullanici);
                form1.kullanici(kullanici);
                form1.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Kullanıcı adı veya şifre yanlıştır tekrar deneyiniz");
            }

            baglanti.Close();          
        }

        private void GirisEkrani_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }


    }
}
