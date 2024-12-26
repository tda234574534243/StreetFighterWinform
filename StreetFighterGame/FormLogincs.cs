using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StreetFighterGame.QuanLiTaikhoan;

namespace StreetFighterGame
{
    public partial class FormLogincs : Form
    {
        public static FormLogincs instance;
        public FormLogincs()
        {
            InitializeComponent();
            instance = this;
        }

        private void FormLogincs_Load(object sender, EventArgs e)
        {
            panelCreatAccount.Visible = false;
            panelLogin.Visible = true;
            panelDoiMatKhau.Visible = false;
        }

        private void linkLabelCreatAccount_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            panelLogin.Visible = false;
            panelCreatAccount.Visible = true;
            labelCanhBao.Visible = false;
        }

        private void buttonCreatAccount_Click(object sender, EventArgs e)
        {
            labelCanhBao.Visible = false;
            if (QuanLiTaiKhoan.CreatAccount(textBoxCreatAccount.Text, textBoxCreatPassWord2.Text, textBoxCreatAccount.Text, textBoxCreatPassWord.Text, textBoxCreatPassWord2.Text, labelCanhBao))
            {
                panelLogin.Visible = true;
                panelCreatAccount.Visible = false;
                labelCanhBao.Visible = false;
            }
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            QuanLiTaiKhoan.DangNhap(textBoxUserName.Text, textBoxPassWord.Text);
        }
        public void HienThi()
        {
            this.Show();
            textBoxCreatAccount.Text = textBoxCreatPassWord.Text = textBoxCreatPassWord2.Text = textBoxUserName.Text = textBoxPassWord.Text = "";
            panelCreatAccount.Visible = false;
            panelLogin.Visible = true;
        }

        private void linkLabelback_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            panelLogin.Visible = true;
            panelCreatAccount.Visible = false;
            labelCanhBao.Visible = false;
        }

        private void panelCreatAccount_Paint(object sender, PaintEventArgs e)
        {

        }

        private void linkLabelDoiMatKhau_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            panelDoiMatKhau.Visible = true;
            panelLogin.Visible = false;
            labelDoiMatKhau.Visible = false;
        }

        private void buttonDoiMatKhau_Click(object sender, EventArgs e)
        {
            if (QuanLiTaiKhoan.DoiMatKhau(textBoxUserName.Text, textBoxOldPassWord.Text, textBoxNewPassWord1.Text, textBoxNewPassWord2.Text, labelDoiMatKhau))
            {
                panelDoiMatKhau.Visible = false;
                panelLogin.Visible = true;
            };
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            panelDoiMatKhau.Visible = false;
            panelLogin.Visible = true;
        }
    }
}
