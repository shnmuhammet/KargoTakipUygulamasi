using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class ŞifremiUnuttum : Form
    {
        public ŞifremiUnuttum()
        {
            InitializeComponent();
        }
        GirisEkrani GirisEkrani = new GirisEkrani();

        private void ŞifremiUnuttum_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            GirisEkrani giris = new GirisEkrani();
            giris.Show();
            this.Hide();
         
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
