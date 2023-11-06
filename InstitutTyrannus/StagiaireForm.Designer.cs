namespace InstitutTyrannus
{
    partial class Stagiaire
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Stagiaire));
            this.nomTextBox = new System.Windows.Forms.TextBox();
            this.idMaskedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.telephoneMaskedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.infoRichTextBox = new System.Windows.Forms.RichTextBox();
            this.stagiairePictureBox = new System.Windows.Forms.PictureBox();
            this.nomLabel = new System.Windows.Forms.Label();
            this.idLabel = new System.Windows.Forms.Label();
            this.telephoneLabel = new System.Windows.Forms.Label();
            this.infosLabel = new System.Windows.Forms.Label();
            this.titreLabel = new System.Windows.Forms.Label();
            this.barreLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.stagiairePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // nomTextBox
            // 
            this.nomTextBox.Location = new System.Drawing.Point(91, 258);
            this.nomTextBox.Name = "nomTextBox";
            this.nomTextBox.Size = new System.Drawing.Size(194, 20);
            this.nomTextBox.TabIndex = 5;
            this.nomTextBox.TextChanged += new System.EventHandler(this.StagiaireTextBoxMaskedTextBox_TextChanged);
            // 
            // idMaskedTextBox
            // 
            this.idMaskedTextBox.HideSelection = false;
            this.idMaskedTextBox.Location = new System.Drawing.Point(91, 217);
            this.idMaskedTextBox.Mask = "S000";
            this.idMaskedTextBox.Name = "idMaskedTextBox";
            this.idMaskedTextBox.Size = new System.Drawing.Size(194, 20);
            this.idMaskedTextBox.TabIndex = 3;
            this.idMaskedTextBox.TextChanged += new System.EventHandler(this.StagiaireTextBoxMaskedTextBox_TextChanged);
            // 
            // telephoneMaskedTextBox
            // 
            this.telephoneMaskedTextBox.Location = new System.Drawing.Point(91, 306);
            this.telephoneMaskedTextBox.Mask = "(000)-(000)-(0000)";
            this.telephoneMaskedTextBox.Name = "telephoneMaskedTextBox";
            this.telephoneMaskedTextBox.Size = new System.Drawing.Size(194, 20);
            this.telephoneMaskedTextBox.TabIndex = 7;
            this.telephoneMaskedTextBox.TextChanged += new System.EventHandler(this.StagiaireTextBoxMaskedTextBox_TextChanged);
            // 
            // infoRichTextBox
            // 
            this.infoRichTextBox.Location = new System.Drawing.Point(24, 363);
            this.infoRichTextBox.Name = "infoRichTextBox";
            this.infoRichTextBox.Size = new System.Drawing.Size(260, 80);
            this.infoRichTextBox.TabIndex = 9;
            this.infoRichTextBox.Text = "";
            this.infoRichTextBox.SelectionChanged += new System.EventHandler(this.infoRichTextBox_SelectionChanged);
            // 
            // stagiairePictureBox
            // 
            this.stagiairePictureBox.Image = ((System.Drawing.Image)(resources.GetObject("stagiairePictureBox.Image")));
            this.stagiairePictureBox.Location = new System.Drawing.Point(85, 63);
            this.stagiairePictureBox.Name = "stagiairePictureBox";
            this.stagiairePictureBox.Size = new System.Drawing.Size(135, 121);
            this.stagiairePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.stagiairePictureBox.TabIndex = 5;
            this.stagiairePictureBox.TabStop = false;
            // 
            // nomLabel
            // 
            this.nomLabel.AutoSize = true;
            this.nomLabel.Location = new System.Drawing.Point(24, 263);
            this.nomLabel.Name = "nomLabel";
            this.nomLabel.Size = new System.Drawing.Size(32, 13);
            this.nomLabel.TabIndex = 4;
            this.nomLabel.Text = "Nom:";
            // 
            // idLabel
            // 
            this.idLabel.AutoSize = true;
            this.idLabel.Location = new System.Drawing.Point(24, 219);
            this.idLabel.Name = "idLabel";
            this.idLabel.Size = new System.Drawing.Size(19, 13);
            this.idLabel.TabIndex = 2;
            this.idLabel.Text = "Id:";
            // 
            // telephoneLabel
            // 
            this.telephoneLabel.AutoSize = true;
            this.telephoneLabel.Location = new System.Drawing.Point(24, 311);
            this.telephoneLabel.Name = "telephoneLabel";
            this.telephoneLabel.Size = new System.Drawing.Size(61, 13);
            this.telephoneLabel.TabIndex = 6;
            this.telephoneLabel.Text = "Téléphone:";
            // 
            // infosLabel
            // 
            this.infosLabel.AutoSize = true;
            this.infosLabel.Location = new System.Drawing.Point(24, 348);
            this.infosLabel.Name = "infosLabel";
            this.infosLabel.Size = new System.Drawing.Size(33, 13);
            this.infosLabel.TabIndex = 8;
            this.infosLabel.Text = "Infos:";
            // 
            // titreLabel
            // 
            this.titreLabel.AutoSize = true;
            this.titreLabel.Font = new System.Drawing.Font("Bookman Old Style", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titreLabel.ForeColor = System.Drawing.Color.Black;
            this.titreLabel.Location = new System.Drawing.Point(63, 17);
            this.titreLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.titreLabel.Name = "titreLabel";
            this.titreLabel.Size = new System.Drawing.Size(233, 28);
            this.titreLabel.TabIndex = 0;
            this.titreLabel.Text = "Institut Tyrannus";
            // 
            // barreLabel
            // 
            this.barreLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.barreLabel.BackColor = System.Drawing.Color.Black;
            this.barreLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.barreLabel.Location = new System.Drawing.Point(-4, 46);
            this.barreLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.barreLabel.Name = "barreLabel";
            this.barreLabel.Size = new System.Drawing.Size(330, 8);
            this.barreLabel.TabIndex = 1;
            // 
            // Stagiaire
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(310, 469);
            this.Controls.Add(this.barreLabel);
            this.Controls.Add(this.titreLabel);
            this.Controls.Add(this.infosLabel);
            this.Controls.Add(this.telephoneLabel);
            this.Controls.Add(this.idLabel);
            this.Controls.Add(this.nomLabel);
            this.Controls.Add(this.stagiairePictureBox);
            this.Controls.Add(this.infoRichTextBox);
            this.Controls.Add(this.telephoneMaskedTextBox);
            this.Controls.Add(this.idMaskedTextBox);
            this.Controls.Add(this.nomTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Stagiaire";
            this.Text = "Fiche des stagiaires  ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Stagiaire_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.stagiairePictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox nomTextBox;
        internal System.Windows.Forms.MaskedTextBox idMaskedTextBox;
        internal System.Windows.Forms.MaskedTextBox telephoneMaskedTextBox;
        internal System.Windows.Forms.RichTextBox infoRichTextBox;
        internal System.Windows.Forms.PictureBox stagiairePictureBox;
        internal System.Windows.Forms.Label nomLabel;
        internal System.Windows.Forms.Label idLabel;
        internal System.Windows.Forms.Label telephoneLabel;
        internal System.Windows.Forms.Label infosLabel;
        internal System.Windows.Forms.Label titreLabel;
        internal System.Windows.Forms.Label barreLabel;
    }
}