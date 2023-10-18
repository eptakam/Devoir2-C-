/*
        Programmeurs:   Ange Yemele, 
                        Ansoumane Condé,
                        Dorian Wontcheu,
                        Emmanuel Takam,
                        Yannis-Arthur Nenzeko

        Date:           Octobre 2023

        Solution:       InstitutTyrannus.sln
        Projet:         InstitutTyrannus.csproj
        Classe:         StagiaireForm.cs

        But:            Enregistrer les informations d'un nouveau stagiaire
                        Enregistrer les fichiers portant l'extension .RTF
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using g = InstitutTyrannus.InstitutTyrannusClass;

namespace InstitutTyrannus
{
    public partial class Stagiaire : Form
    {
        #region Variables

        private bool enregistrementBool = false;
        private bool modificationBool = false;

        #endregion

        #region Constructeur

        public Stagiaire()
        {
            InitializeComponent();
        }

        #endregion

        #region Propriétés

        public bool Enregistrement
        {
            get
            {
                return enregistrementBool;
            }
            set
            {
                enregistrementBool = value;
            }
        }

        public bool Modification
        {
            get
            {
                return modificationBool;
            }
            set
            {
                modificationBool = value;
            }
        }

        #endregion

        #region Changement dans les TextBox et MaskedTextBox

        private void StagiaireTextBoxMaskedTextBox_TextChanged(object sender, EventArgs e)
        {
            Modification = true;
        }

        #endregion

        #region Enregistrer

        public void Enregistrer()
        {
            try
            {
                if (infoRichTextBox.Modified || Modification)
                {
                    if (!Enregistrement)
                        EnregistrerSous();
                    else
                    {
                        RichTextBox ortf = new RichTextBox();

                        CopierVersRichTextBox(ortf);    // créer et remplir le richTextbox temporaire
                        ortf.SaveFile(this.Text);

                        // Pas de changement
                        Modification = false;
                        infoRichTextBox.Modified = false;
                    }
                }
            }
            catch(Exception)
            {
                MessageBox.Show(g.tMessagesErreurStr[(int)g.CodeErreurs.ceErreurIndeterminee],
                                this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            
        }

        public void EnregistrerSous()
        {           
            try
            {
                // Obtenir une référence vers le formulaire parent (CentreTyrannusForm)
                Parent parentForm = this.MdiParent as Parent;

                SaveFileDialog sfd = parentForm.institutTyrannusSaveFileDialog; //sfd: save file dialog

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    string cheminFichier = sfd.FileName;

                    // Vérifiez si l'extension du fichier est .rtf
                    if (Path.GetExtension(cheminFichier).Equals(".rtf", StringComparison.OrdinalIgnoreCase))
                    {
                        RichTextBox ortf = new RichTextBox();

                        CopierVersRichTextBox(ortf);    // créer et remplir le richTextbox temporaire
                        ortf.SaveFile(sfd.FileName);
                        this.Text = sfd.FileName;

                        // Enregistré et pas de changement
                        Enregistrement = true;
                        Modification = false;
                        infoRichTextBox.Modified = false;
                    }
                    else
                    {
                        MessageBox.Show(g.tMessagesErreurStr[(int)g.CodeErreurs.ceErreurSaveDocument],
                                "Enregistrer sous du document", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show(g.tMessagesErreurStr[(int)g.CodeErreurs.ceErreurIndeterminee],
                                this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }        
        }

        #endregion

        #region Fermer le formulaire actif

        private void Stagiaire_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                DialogResult oDialogResult;

                if (infoRichTextBox.Modified || Modification) // modification sur le rtf et ou sur les autres controles
                {
                    oDialogResult = MessageBox.Show("Modification. Enregistrer ?", "Modification", MessageBoxButtons.YesNoCancel);

                    switch (oDialogResult)
                    {
                        case DialogResult.Yes:
                            Enregistrer();  // ou enregistrer sous
                            this.Dispose();
                            break;

                        case DialogResult.Cancel:
                            e.Cancel = true;
                            break;

                        case DialogResult.No:
                            this.Dispose();
                            break;
                    }
                }
            }
            catch(Exception)
            {
                MessageBox.Show(g.tMessagesErreurStr[(int)g.CodeErreurs.ceErreurIndeterminee],
                                this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }            
        }

        #endregion

        #region Méthode privée

        private void CopierVersRichTextBox(RichTextBox ortf)
        {
            // Remplir le richTextbox temporaire
            ortf.Rtf = infoRichTextBox.Rtf;
            ortf.SelectionStart = 0;
            ortf.SelectionLength = 0;
            ortf.SelectedText = idMaskedTextBox.Text + Environment.NewLine +
                                nomTextBox.Text + Environment.NewLine +
                                telephoneMaskedTextBox.Text + Environment.NewLine;
        }

        #endregion
    }
}
