using StreetFighterGame.QuanLiTaikhoan;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StreetFighterGame
{
    public partial class FormRanking : Form
    {
        public FormRanking()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Load += FormRanking_Load;
        }
        private void FormRanking_Load(object sender, EventArgs e)
        {
            HienThiTop3(lblTop1Name, lblTop1Wins, lblTop2Name, lblTop2Wins, lblTop3Name, lblTop3Wins);
        }
        private void HienThiTop3(Label lblTop1Name, Label lblTop1Wins,
                         Label lblTop2Name, Label lblTop2Wins,
                         Label lblTop3Name, Label lblTop3Wins)
        {
            var top3 = QuanLiTaiKhoan.LayTop3NguoiChoi();

            // Gán dữ liệu cho top 1
            if (top3.Count > 0)
            {
                lblTop1Name.Text = top3[0].Username;
                lblTop1Wins.Text = top3[0].Wins.ToString();
            }
            else
            {
                lblTop1Name.Text = "Không có";
                lblTop1Wins.Text = "0";
            }

            // Gán dữ liệu cho top 2
            if (top3.Count > 1)
            {
                lblTop2Name.Text = top3[1].Username;
                lblTop2Wins.Text = top3[1].Wins.ToString();
            }
            else
            {
                lblTop2Name.Text = "Không có";
                lblTop2Wins.Text = "0";
            }

            // Gán dữ liệu cho top 3
            if (top3.Count > 2)
            {
                lblTop3Name.Text = top3[2].Username;
                lblTop3Wins.Text = top3[2].Wins.ToString();
            }
            else
            {
                lblTop3Name.Text = "Không có";
                lblTop3Wins.Text = "0";
            }
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            FormStart st = new FormStart();
            st.Show();
            this.Close();
        }

        private void buttonExit_Click_1(object sender, EventArgs e)
        {
            FormStart st = new FormStart();
            st.Show();
            this.Close();
        }
    }
}
