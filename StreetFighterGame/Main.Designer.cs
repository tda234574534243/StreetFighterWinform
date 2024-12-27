namespace StreetFighterGame
{
    partial class StreetFighterGame
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StreetFighterGame));
            this.GameTimer = new System.Windows.Forms.Timer(this.components);
            this.labelWiner = new System.Windows.Forms.Label();
            this.LbCountdown = new System.Windows.Forms.Label();
            this.Lbstart = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.SuspendLayout();
            // 
            // GameTimer
            // 
            this.GameTimer.Enabled = true;
            this.GameTimer.Interval = 20;
            // 
            // labelWiner
            // 
            this.labelWiner.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelWiner.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labelWiner.Font = new System.Drawing.Font("Wide Latin", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelWiner.ForeColor = System.Drawing.Color.Orange;
            this.labelWiner.Location = new System.Drawing.Point(320, 199);
            this.labelWiner.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelWiner.Name = "labelWiner";
            this.labelWiner.Size = new System.Drawing.Size(355, 80);
            this.labelWiner.TabIndex = 0;
            this.labelWiner.Text = "Goku WIN";
            this.labelWiner.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelWiner.Click += new System.EventHandler(this.labelWiner_Click);
            // 
            // LbCountdown
            // 
            this.LbCountdown.AutoSize = true;
            this.LbCountdown.BackColor = System.Drawing.Color.Transparent;
            this.LbCountdown.Font = new System.Drawing.Font("Microsoft Sans Serif", 28.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LbCountdown.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.LbCountdown.Location = new System.Drawing.Point(474, 37);
            this.LbCountdown.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LbCountdown.Name = "LbCountdown";
            this.LbCountdown.Size = new System.Drawing.Size(61, 44);
            this.LbCountdown.TabIndex = 1;
            this.LbCountdown.Text = "60";
            // 
            // Lbstart
            // 
            this.Lbstart.AutoSize = true;
            this.Lbstart.BackColor = System.Drawing.Color.Transparent;
            this.Lbstart.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbstart.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.Lbstart.Location = new System.Drawing.Point(467, 199);
            this.Lbstart.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Lbstart.Name = "Lbstart";
            this.Lbstart.Size = new System.Drawing.Size(68, 73);
            this.Lbstart.TabIndex = 2;
            this.Lbstart.Text = "3";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(34, 16);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(72, 55);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox3.BackgroundImage")));
            this.pictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox3.Location = new System.Drawing.Point(864, 16);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(72, 55);
            this.pictureBox3.TabIndex = 5;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox2.BackgroundImage")));
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.Location = new System.Drawing.Point(74, 6);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(230, 48);
            this.pictureBox2.TabIndex = 6;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox4.BackgroundImage")));
            this.pictureBox4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox4.Location = new System.Drawing.Point(664, 6);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(246, 48);
            this.pictureBox4.TabIndex = 7;
            this.pictureBox4.TabStop = false;
            // 
            // StreetFighterGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(970, 474);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.LbCountdown);
            this.Controls.Add(this.labelWiner);
            this.Controls.Add(this.Lbstart);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox4);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.Name = "StreetFighterGame";
            this.Text = "Street Fighter 2.0";
            this.Load += new System.EventHandler(this.StreetFighterGame_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer GameTimer;
        private System.Windows.Forms.Label labelWiner;
        private System.Windows.Forms.Label LbCountdown;
        private System.Windows.Forms.Label Lbstart;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox4;
    }
}

