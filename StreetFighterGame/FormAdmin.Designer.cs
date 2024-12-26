namespace StreetFighterGame
{
    partial class FormAdmin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridViewAccounts = new System.Windows.Forms.DataGridView();
            this.textBoxKeyWord = new System.Windows.Forms.TextBox();
            this.buttonFind = new System.Windows.Forms.Button();
            this.buttonDelAccount = new System.Windows.Forms.Button();
            this.panelXoaTk = new System.Windows.Forms.Panel();
            this.BtnHuy = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonXoa = new System.Windows.Forms.Button();
            this.textBoxXoa = new System.Windows.Forms.TextBox();
            this.buttonExit = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAccounts)).BeginInit();
            this.panelXoaTk.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewAccounts
            // 
            this.dataGridViewAccounts.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dataGridViewAccounts.BackgroundColor = System.Drawing.Color.Silver;
            this.dataGridViewAccounts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewAccounts.Location = new System.Drawing.Point(3, 179);
            this.dataGridViewAccounts.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridViewAccounts.Name = "dataGridViewAccounts";
            this.dataGridViewAccounts.RowHeadersWidth = 51;
            this.dataGridViewAccounts.RowTemplate.Height = 24;
            this.dataGridViewAccounts.Size = new System.Drawing.Size(658, 291);
            this.dataGridViewAccounts.TabIndex = 0;
            // 
            // textBoxKeyWord
            // 
            this.textBoxKeyWord.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxKeyWord.Location = new System.Drawing.Point(11, 113);
            this.textBoxKeyWord.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxKeyWord.Name = "textBoxKeyWord";
            this.textBoxKeyWord.Size = new System.Drawing.Size(189, 28);
            this.textBoxKeyWord.TabIndex = 1;
            // 
            // buttonFind
            // 
            this.buttonFind.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonFind.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonFind.Location = new System.Drawing.Point(218, 115);
            this.buttonFind.Margin = new System.Windows.Forms.Padding(2);
            this.buttonFind.Name = "buttonFind";
            this.buttonFind.Size = new System.Drawing.Size(65, 29);
            this.buttonFind.TabIndex = 2;
            this.buttonFind.Text = "Tìm kiếm";
            this.buttonFind.UseVisualStyleBackColor = false;
            this.buttonFind.Click += new System.EventHandler(this.buttonFind_Click);
            // 
            // buttonDelAccount
            // 
            this.buttonDelAccount.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonDelAccount.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDelAccount.Location = new System.Drawing.Point(561, 113);
            this.buttonDelAccount.Margin = new System.Windows.Forms.Padding(2);
            this.buttonDelAccount.Name = "buttonDelAccount";
            this.buttonDelAccount.Size = new System.Drawing.Size(100, 47);
            this.buttonDelAccount.TabIndex = 4;
            this.buttonDelAccount.Text = "Xóa tài khoản";
            this.buttonDelAccount.UseVisualStyleBackColor = false;
            this.buttonDelAccount.Click += new System.EventHandler(this.buttonDelAccount_Click);
            // 
            // panelXoaTk
            // 
            this.panelXoaTk.BackColor = System.Drawing.Color.Gray;
            this.panelXoaTk.Controls.Add(this.BtnHuy);
            this.panelXoaTk.Controls.Add(this.label1);
            this.panelXoaTk.Controls.Add(this.buttonXoa);
            this.panelXoaTk.Controls.Add(this.textBoxXoa);
            this.panelXoaTk.Location = new System.Drawing.Point(242, 165);
            this.panelXoaTk.Margin = new System.Windows.Forms.Padding(2);
            this.panelXoaTk.Name = "panelXoaTk";
            this.panelXoaTk.Size = new System.Drawing.Size(192, 149);
            this.panelXoaTk.TabIndex = 7;
            // 
            // BtnHuy
            // 
            this.BtnHuy.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.BtnHuy.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnHuy.Location = new System.Drawing.Point(108, 101);
            this.BtnHuy.Margin = new System.Windows.Forms.Padding(2);
            this.BtnHuy.Name = "BtnHuy";
            this.BtnHuy.Size = new System.Drawing.Size(65, 29);
            this.BtnHuy.TabIndex = 10;
            this.BtnHuy.Text = "Hủy";
            this.BtnHuy.UseVisualStyleBackColor = false;
            this.BtnHuy.Click += new System.EventHandler(this.BtnHuy_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(22, 15);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 17);
            this.label1.TabIndex = 9;
            this.label1.Text = "Tên tài khoản";
            // 
            // buttonXoa
            // 
            this.buttonXoa.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonXoa.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonXoa.Location = new System.Drawing.Point(25, 101);
            this.buttonXoa.Margin = new System.Windows.Forms.Padding(2);
            this.buttonXoa.Name = "buttonXoa";
            this.buttonXoa.Size = new System.Drawing.Size(65, 29);
            this.buttonXoa.TabIndex = 8;
            this.buttonXoa.Text = "Xóa";
            this.buttonXoa.UseVisualStyleBackColor = false;
            this.buttonXoa.Click += new System.EventHandler(this.buttonXoa_Click);
            // 
            // textBoxXoa
            // 
            this.textBoxXoa.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxXoa.Location = new System.Drawing.Point(25, 34);
            this.textBoxXoa.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxXoa.Name = "textBoxXoa";
            this.textBoxXoa.Size = new System.Drawing.Size(150, 28);
            this.textBoxXoa.TabIndex = 8;
            // 
            // buttonExit
            // 
            this.buttonExit.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonExit.BackColor = System.Drawing.Color.Transparent;
            this.buttonExit.BackgroundImage = global::StreetFighterGame.Properties.Resources.logOut;
            this.buttonExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonExit.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.buttonExit.Location = new System.Drawing.Point(616, 24);
            this.buttonExit.Margin = new System.Windows.Forms.Padding(2);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(38, 41);
            this.buttonExit.TabIndex = 19;
            this.buttonExit.UseVisualStyleBackColor = false;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click_1);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.DimGray;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 25.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(-4, 15);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.label2.Size = new System.Drawing.Size(668, 67);
            this.label2.TabIndex = 20;
            this.label2.Text = "Quản lí tài khoản";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FormAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(662, 470);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.panelXoaTk);
            this.Controls.Add(this.buttonDelAccount);
            this.Controls.Add(this.buttonFind);
            this.Controls.Add(this.textBoxKeyWord);
            this.Controls.Add(this.dataGridViewAccounts);
            this.Controls.Add(this.label2);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "FormAdmin";
            this.Text = "FormAdmin";
            this.Load += new System.EventHandler(this.FormAdmin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAccounts)).EndInit();
            this.panelXoaTk.ResumeLayout(false);
            this.panelXoaTk.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewAccounts;
        private System.Windows.Forms.TextBox textBoxKeyWord;
        private System.Windows.Forms.Button buttonFind;
        private System.Windows.Forms.Button buttonDelAccount;
        private System.Windows.Forms.Panel panelXoaTk;
        private System.Windows.Forms.Button buttonXoa;
        private System.Windows.Forms.TextBox textBoxXoa;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BtnHuy;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.Label label2;
    }
}