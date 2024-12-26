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
    public partial class FormLichSuDau : Form
    {
        public FormLichSuDau()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void FormLichSuDau_Load(object sender, EventArgs e)
        {
            QuanLiTaiKhoan.HienThiLichSuTranDau(dataGridView1);
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            FormStart st = new FormStart();
            st.Show();
            this.Close();
        }

        private void buttonFind_Click(object sender, EventArgs e)
        {
            var lichSu = QuanLiTaiKhoan.TimKiemLichSu(textBoxKeyWord.Text);
            dataGridView1.DataSource = lichSu;
        }

        private void buttonLamMoi_Click(object sender, EventArgs e)
        {
            QuanLiTaiKhoan.HienThiLichSuTranDau(dataGridView1);
        }

        private void buttonExit_Click_1(object sender, EventArgs e)
        {
            FormStart st = new FormStart();
            st.Show();
            this.Close();
        }

        
    }
}
