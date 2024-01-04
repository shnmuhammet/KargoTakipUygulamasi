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
using WindowsFormsApp2.DataModels;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1(string kullanici)
        {
            InitializeComponent();
            
        }

        public Form1()
        {
        }

        public void kullanici(string kullanici)
        {
            label28.Text = kullanici;
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-LOTOEV7\SQLEXPRESS;Initial Catalog=kargoTakipp;Persist Security Info=True;User ID=shn;Password=1");

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
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

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            int gondericiId = 0;

            if (rdbBireysel.Checked)
            {
                baglanti.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO BireyselGonderici (ad, soyad, kimlikNo, mail, telefon) VALUES (@ad, @soyad, @kimlikNo, @mail, @telefon); SELECT SCOPE_IDENTITY();", baglanti);
                cmd.Parameters.AddWithValue("@ad", txtAd.Text);
                cmd.Parameters.AddWithValue("@soyad", txtSoyad.Text);
                cmd.Parameters.AddWithValue("@kimlikNo", txtKimlik.Text);
                cmd.Parameters.AddWithValue("@mail", txtMail.Text);
                cmd.Parameters.AddWithValue("@telefon", txtTelefon.Text);

                 gondericiId = Convert.ToInt32(cmd.ExecuteScalar());

                // Yeni eklenen BireyselGonderici kaydının gondericiID değerini Gonderici tablosuna ekleyin
                SqlCommand cmdUpdateGonderici = new SqlCommand("INSERT INTO Gonderici (gondericiID,tur) VALUES (@gondericiID,@tur)", baglanti);
                cmdUpdateGonderici.Parameters.AddWithValue("@gondericiID", gondericiId);
                cmdUpdateGonderici.Parameters.AddWithValue("@tur", rdbBireysel.Text);
                
                cmdUpdateGonderici.ExecuteNonQuery();

                baglanti.Close();
                MessageBox.Show("Girilen bilgiler kaydedildi");
            }
            
            if (rdbKurumsal.Checked)
            {
                baglanti.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO KurumsalGonderici(kurumAdi, mersisNo) VALUES (@KurumAdi, @mersisNo); SELECT SCOPE_IDENTITY()", baglanti);
                cmd.Parameters.AddWithValue("@kurumAdi", txtKurumAdi.Text);
                cmd.Parameters.AddWithValue("@mersisNo", txtMersisNo.Text);

                gondericiId = Convert.ToInt32(cmd.ExecuteScalar());

                // Yeni eklenen KurumsalGonderici kaydının kurumsalGondericiId değerini Gonderici tablosuna ekleyin
                SqlCommand cmdUpdateGonderici = new SqlCommand("INSERT INTO Gonderici (gondericiID,tur) VALUES (@gondericiID,@tur)", baglanti);
                cmdUpdateGonderici.Parameters.AddWithValue("@gondericiID",gondericiId);
                cmdUpdateGonderici.Parameters.AddWithValue("@tur", rdbKurumsal.Text);
                cmdUpdateGonderici.ExecuteNonQuery();

                baglanti.Close();
                MessageBox.Show("Girilen bilgiler kaydedildi");
            }
            baglanti.Open();
            SqlCommand cmd1 = new SqlCommand("INSERT INTO Alici(ad,soyad,adres,telefon,mahalle) VALUES (@ad,@soyad,@adres,@telefon,@mahalle) SELECT SCOPE_IDENTITY()", baglanti);
            cmd1.Parameters.AddWithValue("@ad", txtAliciAd.Text);
            cmd1.Parameters.AddWithValue("@soyad", txtAliciSoyad.Text);
            cmd1.Parameters.AddWithValue("@telefon", txtAliciTelefon.Text);
            cmd1.Parameters.AddWithValue("@adres", txtAliciAdres.Text);
            cmd1.Parameters.AddWithValue("@mahalle", cmbMahalle.Text);
            int aliciId = Convert.ToInt32(cmd1.ExecuteScalar());
           // SqlDataReader rdr1 = cmd1.ExecuteReader();
            MessageBox.Show("Girilen bilgiler kaydedildi");
            baglanti.Close();

            baglanti.Open();
            SqlCommand cmd2 = new SqlCommand("INSERT INTO Kargo(turu,desi,agirlik,adet,icerik,gonderimSekli,odemeTuru,sigorta,aliciId,gondericiId) VALUES (@turu,@desi,@agirlik,@adet,@icerik,@gonderimSekli,@odemeTuru,@sigorta,@aliciId,@gondericiId) SELECT SCOPE_IDENTITY()", baglanti);
            cmd2.Parameters.AddWithValue("@turu", DBNull.Value);
            cmd2.Parameters.AddWithValue("@desi", DBNull.Value);
            cmd2.Parameters.AddWithValue("@agirlik", DBNull.Value);
            cmd2.Parameters.AddWithValue("@odemeTuru", DBNull.Value);
            cmd2.Parameters.AddWithValue("@adet", numericUpDown1.Value);
            cmd2.Parameters.AddWithValue("@icerik", cmbİcerik.Text);
            cmd2.Parameters.AddWithValue("@gonderimSekli", DBNull.Value);
            cmd2.Parameters.AddWithValue("@sigorta", DBNull.Value);
            cmd2.Parameters.AddWithValue("@aliciId", aliciId);
            cmd2.Parameters.AddWithValue("@gondericiId", gondericiId);

            if (rdbZarf.Checked)
            {
                cmd2.Parameters["@turu"].Value= rdbZarf.Text;
            }
            else
            {
                cmd2.Parameters["@turu"].Value=rdbKoli.Text;
            }

            if (rdbGonderici.Checked)
            {
                cmd2.Parameters["@odemeTuru"].Value=rdbGonderici.Text;
            }
            else
            {
                cmd2.Parameters["@odemeTuru"].Value= rdbAlici.Text;
            }




            if (rdbHizli.Checked)
            {
                cmd2.Parameters["@gonderimSekli"].Value= rdbHizli.Text;
            }
            else
            {
                cmd2.Parameters["@gonderimSekli"].Value= rdbNormal.Text;
            }        
            if (rdbSgrtVr.Checked)
            {
                cmd2.Parameters["@sigorta"].Value=rdbSgrtVr.Text;
            }
            else
            {
                cmd2.Parameters["@sigorta"].Value=rdbSgrtYk.Text;
            }


            int affectedRows = cmd2.ExecuteNonQuery();




            baglanti.Close();



        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbBireysel.Checked)
            {
                grpBireysel.Enabled = true;
                grpKurumsal.Enabled=false;
               
            }

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbKurumsal.Checked)
            {
                grpKurumsal.Enabled = true;
                grpBireysel.Enabled = false;
            }

        }

        private void txtKimlik_KeyPress(object sender, KeyPressEventArgs e)
        {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
                {
                e.Handled = true;
                }
                if (txtKimlik.Text.Length < 10)
                {
                   
                    return;
                }

                int[] tcDizi = new int[11];
                string txtTc = txtKimlik.Text;
                errorProvider1.Clear();
                errorProvider2.Clear();

                if (txtKimlik.Text == "0")
                {
                    errorProvider1.SetError(txtKimlik, "Kimlik Numarası sıfır ile başlayamaz");
                    e.Handled = true;
                    return;
                }

                for (int i = 0; i < 11; i++)
                {
                    if (i < txtTc.Length)
                    {
                        tcDizi[i] = txtTc[i] - '0';
                    }
                }

                int teksayilar = 0, ciftsayilar = 0;
                for (int i = 0; i < 9; i += 2)
                {
                    teksayilar += tcDizi[i];
                }
                for (int i = 1; i < 8; i += 2)
                {
                    ciftsayilar += tcDizi[i];
                }

                if ((teksayilar * 7 - ciftsayilar) % 10 != tcDizi[9])
                {
                    errorProvider1.SetError(txtKimlik, "Kimlik no 10.rakamı yanlıştır");
                }

                int teksayilar1 = 0, ciftsayilar1 = 0;
                for (int i = 0; i < 9; i += 2)
                {
                    teksayilar1 += tcDizi[i];
                }
                for (int i = 1; i < 8; i += 2)
                {
                    ciftsayilar1 += tcDizi[i];
                }

                int onuncurakam = (teksayilar1 * 7 - ciftsayilar1) % 10;
                if ((teksayilar1 + ciftsayilar1 + onuncurakam) % 10 != tcDizi[10])
                {
                    errorProvider2.SetError(txtKimlik, "Kimlik No 11.hanesini yanlıştır");
                }
            

        }

        private void txtMersisNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void txtVergiNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void label27_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            GirisEkrani giris = new GirisEkrani();
            this.Hide();
            giris.Show();
        }


        private void btnHesapla_Click(object sender, EventArgs e)
        {
            int en, boy, yukseklik;
            double agirlik, desi;
            en = int.Parse(txtEn.Text);
            boy = int.Parse(txtBoy.Text);
            yukseklik = int.Parse(txtYukseklik.Text);
            agirlik = int.Parse(txtAgirlik.Text);
            desi = (en * boy * yukseklik) / 3000;
            if (desi>agirlik)
            {
                desi *= 15;
                lblUcret.Text = desi.ToString();
            }
            else
            {
                agirlik *= 20;
                lblUcret.Text = agirlik.ToString();
            }
        }
    }
}
