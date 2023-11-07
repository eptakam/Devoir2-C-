namespace InstitutTyrannus
{
    partial class RechercherForm
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
            this.rechercherLabel = new System.Windows.Forms.Label();
            this.rechercherTextBox = new System.Windows.Forms.TextBox();
            this.suivantButton = new System.Windows.Forms.Button();
            this.annulerButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rechercherLabel
            // 
            this.rechercherLabel.AutoSize = true;
            this.rechercherLabel.Location = new System.Drawing.Point(17, 56);
            this.rechercherLabel.Name = "rechercherLabel";
            this.rechercherLabel.Size = new System.Drawing.Size(83, 16);
            this.rechercherLabel.TabIndex = 0;
            this.rechercherLabel.Text = "&Rechercher :";
            // 
            // rechercherTextBox
            // 
            this.rechercherTextBox.Location = new System.Drawing.Point(127, 53);
            this.rechercherTextBox.Name = "rechercherTextBox";
            this.rechercherTextBox.Size = new System.Drawing.Size(265, 22);
            this.rechercherTextBox.TabIndex = 1;
            // 
            // suivantButton
            // 
            this.suivantButton.Location = new System.Drawing.Point(449, 25);
            this.suivantButton.Name = "suivantButton";
            this.suivantButton.Size = new System.Drawing.Size(116, 31);
            this.suivantButton.TabIndex = 2;
            this.suivantButton.Text = "&Suivant";
            this.suivantButton.UseVisualStyleBackColor = true;
            this.suivantButton.Click += new System.EventHandler(this.suivantButton_Click);
            // 
            // annulerButton
            // 
            this.annulerButton.Location = new System.Drawing.Point(449, 76);
            this.annulerButton.Name = "annulerButton";
            this.annulerButton.Size = new System.Drawing.Size(116, 31);
            this.annulerButton.TabIndex = 3;
            this.annulerButton.Text = "Annuler";
            this.annulerButton.UseVisualStyleBackColor = true;
            this.annulerButton.Click += new System.EventHandler(this.annulerButton_Click);
            // 
            // RechercherForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 131);
            this.Controls.Add(this.annulerButton);
            this.Controls.Add(this.suivantButton);
            this.Controls.Add(this.rechercherTextBox);
            this.Controls.Add(this.rechercherLabel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RechercherForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Rechercher";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label rechercherLabel;
        private System.Windows.Forms.TextBox rechercherTextBox;
        private System.Windows.Forms.Button suivantButton;
        private System.Windows.Forms.Button annulerButton;
    }
}