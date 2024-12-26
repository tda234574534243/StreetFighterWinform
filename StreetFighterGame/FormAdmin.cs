using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StreetFighterGame.QuanLiTaikhoan;

namespace StreetFighterGame
{
    public partial class FormAdmin : Form
    {
        public FormAdmin()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void FormAdmin_Load(object sender, EventArgs e)
        {
            QuanLiTaiKhoan.LoadDanhSachTaiKhoanLenGridView(dataGridViewAccounts);
            panelXoaTk.Visible = false;
        }
       

        private void buttonExit_Click(object sender, EventArgs e)
        {
            FormStart formStart = new FormStart();
            formStart.Show();
            this.Close();
        }

        private void buttonFind_Click(object sender, EventArgs e)
        {
            QuanLiTaiKhoan.TimKiemTaiKhoan(dataGridViewAccounts, textBoxKeyWord.Text);
        }

        private void buttonXoa_Click(object sender, EventArgs e)
        {
            if (QuanLiTaiKhoan.XoaTaiKhoan(textBoxXoa.Text))
            {
                QuanLiTaiKhoan.LoadDanhSachTaiKhoanLenGridView(dataGridViewAccounts);
                panelXoaTk.Visible = false;
            }
        }

        private void buttonDelAccount_Click(object sender, EventArgs e)
        {
            panelXoaTk.Visible = true;
        }

        private void BtnHuy_Click(object sender, EventArgs e)
        {
            panelXoaTk.Visible = false;
        }

        private void buttonExit_Click_1(object sender, EventArgs e)
        {
            FormStart formStart = new FormStart();
            formStart.Show();
            this.Close();
        }
    }
}
