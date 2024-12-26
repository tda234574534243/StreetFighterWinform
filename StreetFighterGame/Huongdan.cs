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
    public partial class Huongdan : Form
    {
        public Huongdan()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            FormStart st = new FormStart();
            st.Show();
            this.Close();
        }
    }
}
