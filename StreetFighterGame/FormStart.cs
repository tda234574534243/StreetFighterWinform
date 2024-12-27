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
    public partial class FormStart : Form
    {
        public FormStart()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            if (QuanLiTaiKhoan.CheckIsAdmin()) buttonAdmin.Visible = true;
            else buttonAdmin.Visible = false;
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            ChonNhanVat cnv = new ChonNhanVat(false);
            cnv.Show();
            this.Close();
        }

        private void buttonLogOut_Click(object sender, EventArgs e)
        {
            FormLogincs.instance.HienThi();
            this.Close();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát?", "Xác nhận thoát", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void buttonAdmin_Click(object sender, EventArgs e)
        {
            FormAdmin adminForm = new FormAdmin();
            adminForm.Show();
            this.Hide();
        }

        private void buttonRanking_Click(object sender, EventArgs e)
        {
            FormRanking rk = new FormRanking();
            rk.Show();
            this.Close();
        }

        private void buttonLichSuDau_Click(object sender, EventArgs e)
        {
            FormLichSuDau lsd = new FormLichSuDau();
            lsd.Show();
            this.Close();
        }

        private void buttonOption_Click(object sender, EventArgs e)
        {
            FormThanhVien tv = new FormThanhVien();
            tv.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ChonNhanVat cnv = new ChonNhanVat(true);
            cnv.Show();
            this.Close();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Huongdan hd = new Huongdan();  
            hd.Show();
            this.Close();
        }

        private void FormStart_Load(object sender, EventArgs e)
        {

        }
    }
}
