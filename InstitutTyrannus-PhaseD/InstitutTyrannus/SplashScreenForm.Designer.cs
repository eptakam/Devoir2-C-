namespace InstitutTyrannus
{
    partial class SplashScreenForm
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
            this.tyrannusLabel = new System.Windows.Forms.Label();
            this.cartePictureBox = new System.Windows.Forms.PictureBox();
            this.logoPictureBox = new System.Windows.Forms.PictureBox();
            this.splashScreenTimer = new System.Windows.Forms.Timer(this.components);
            this.loadingLabel = new System.Windows.Forms.Label();
            this.splashScreenProgressBar = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.cartePictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // tyrannusLabel
            // 
            this.tyrannusLabel.AutoSize = true;
            this.tyrannusLabel.BackColor = System.Drawing.Color.Transparent;
            this.tyrannusLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.tyrannusLabel.Font = new System.Drawing.Font("Bookman Old Style", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tyrannusLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.tyrannusLabel.Location = new System.Drawing.Point(24, 90);
            this.tyrannusLabel.Name = "tyrannusLabel";
            this.tyrannusLabel.Size = new System.Drawing.Size(329, 32);
            this.tyrannusLabel.TabIndex = 0;
            this.tyrannusLabel.Text = "INSTITUT TYRANNUS";
            // 
            // cartePictureBox
            // 
            this.cartePictureBox.Image = global::InstitutTyrannus.Properties.Resources.carteMonde;
            this.cartePictureBox.Location = new System.Drawing.Point(-7, 1);
            this.cartePictureBox.Name = "cartePictureBox";
            this.cartePictureBox.Size = new System.Drawing.Size(372, 204);
            this.cartePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.cartePictureBox.TabIndex = 1;
            this.cartePictureBox.TabStop = false;
            // 
            // logoPictureBox
            // 
            this.logoPictureBox.Image = global::InstitutTyrannus.Properties.Resources.logoTyrannus;
            this.logoPictureBox.Location = new System.Drawing.Point(108, 184);
            this.logoPictureBox.Name = "logoPictureBox";
            this.logoPictureBox.Size = new System.Drawing.Size(132, 86);
            this.logoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.logoPictureBox.TabIndex = 2;
            this.logoPictureBox.TabStop = false;
            // 
            // splashScreenTimer
            // 
            this.splashScreenTimer.Enabled = true;
            this.splashScreenTimer.Interval = 800;
            this.splashScreenTimer.Tick += new System.EventHandler(this.splashScreenTimer_Tick);
            // 
            // loadingLabel
            // 
            this.loadingLabel.AutoSize = true;
            this.loadingLabel.BackColor = System.Drawing.Color.Transparent;
            this.loadingLabel.Font = new System.Drawing.Font("Bookman Old Style", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loadingLabel.Location = new System.Drawing.Point(-1, 284);
            this.loadingLabel.Name = "loadingLabel";
            this.loadingLabel.Size = new System.Drawing.Size(108, 20);
            this.loadingLabel.TabIndex = 3;
            this.loadingLabel.Text = "Loading. . .";
            // 
            // splashScreenProgressBar
            // 
            this.splashScreenProgressBar.ForeColor = System.Drawing.Color.ForestGreen;
            this.splashScreenProgressBar.Location = new System.Drawing.Point(265, 293);
            this.splashScreenProgressBar.Name = "splashScreenProgressBar";
            this.splashScreenProgressBar.Size = new System.Drawing.Size(100, 11);
            this.splashScreenProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.splashScreenProgressBar.TabIndex = 4;
            // 
            // SplashScreenForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(370, 310);
            this.ControlBox = false;
            this.Controls.Add(this.splashScreenProgressBar);
            this.Controls.Add(this.loadingLabel);
            this.Controls.Add(this.logoPictureBox);
            this.Controls.Add(this.cartePictureBox);
            this.Controls.Add(this.tyrannusLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "SplashScreenForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.Load += new System.EventHandler(this.SplashScreenForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cartePictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label tyrannusLabel;
        private System.Windows.Forms.PictureBox cartePictureBox;
        private System.Windows.Forms.PictureBox logoPictureBox;
        private System.Windows.Forms.Timer splashScreenTimer;
        private System.Windows.Forms.Label loadingLabel;
        private System.Windows.Forms.ProgressBar splashScreenProgressBar;
    }
}

