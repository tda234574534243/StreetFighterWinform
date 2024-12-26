namespace StreetFighterGame
{
    partial class FormLichSuDau
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLichSuDau));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.buttonFind = new System.Windows.Forms.Button();
            this.textBoxKeyWord = new System.Windows.Forms.TextBox();
            this.buttonLamMoi = new System.Windows.Forms.Button();
            this.buttonExit = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(54, 176);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(651, 266);
            this.dataGridView1.TabIndex = 0;
            // 
            // buttonFind
            // 
            this.buttonFind.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonFind.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonFind.Location = new System.Drawing.Point(256, 127);
            this.buttonFind.Margin = new System.Windows.Forms.Padding(2);
            this.buttonFind.Name = "buttonFind";
            this.buttonFind.Size = new System.Drawing.Size(65, 29);
            this.buttonFind.TabIndex = 15;
            this.buttonFind.Text = "Tìm kiếm";
            this.buttonFind.UseVisualStyleBackColor = false;
            this.buttonFind.Click += new System.EventHandler(this.buttonFind_Click);
            // 
            // textBoxKeyWord
            // 
            this.textBoxKeyWord.Location = new System.Drawing.Point(54, 127);
            this.textBoxKeyWord.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxKeyWord.Multiline = true;
            this.textBoxKeyWord.Name = "textBoxKeyWord";
            this.textBoxKeyWord.Size = new System.Drawing.Size(189, 30);
            this.textBoxKeyWord.TabIndex = 14;
            // 
            // buttonLamMoi
            // 
            this.buttonLamMoi.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonLamMoi.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonLamMoi.Location = new System.Drawing.Point(640, 128);
            this.buttonLamMoi.Margin = new System.Windows.Forms.Padding(2);
            this.buttonLamMoi.Name = "buttonLamMoi";
            this.buttonLamMoi.Size = new System.Drawing.Size(65, 29);
            this.buttonLamMoi.TabIndex = 16;
            this.buttonLamMoi.Text = "Làm mới";
            this.buttonLamMoi.UseVisualStyleBackColor = false;
            this.buttonLamMoi.Click += new System.EventHandler(this.buttonLamMoi_Click);
            // 
            // buttonExit
            // 
            this.buttonExit.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonExit.BackColor = System.Drawing.Color.Transparent;
            this.buttonExit.BackgroundImage = global::StreetFighterGame.Properties.Resources.logOut;
            this.buttonExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonExit.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.buttonExit.Location = new System.Drawing.Point(724, 26);
            this.buttonExit.Margin = new System.Windows.Forms.Padding(2);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(38, 41);
            this.buttonExit.TabIndex = 18;
            this.buttonExit.UseVisualStyleBackColor = false;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click_1);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 25.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(0, 15);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.label2.Size = new System.Drawing.Size(772, 67);
            this.label2.TabIndex = 21;
            this.label2.Text = "Lịch sử đấu";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FormLichSuDau
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(771, 501);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.buttonLamMoi);
            this.Controls.Add(this.buttonFind);
            this.Controls.Add(this.textBoxKeyWord);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "FormLichSuDau";
            this.Text = "FormLichSuDau";
            this.Load += new System.EventHandler(this.FormLichSuDau_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button buttonFind;
        private System.Windows.Forms.TextBox textBoxKeyWord;
        private System.Windows.Forms.Button buttonLamMoi;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.Label label2;
    }
}