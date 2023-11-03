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
                        Édition, Police et Alignement du texte
                        Modifier les boutons de la barre d’outils et des menus d’après le texte sélectionné
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using g = InstitutTyrannus.InstitutTyrannusClass;
using ce = InstitutTyrannus.InstitutTyrannusClass.CodeErreurs;

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

        #region Mettre à jour les menus et la barre d'outils

        private void infoRichTextBox_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                Parent oParent = (Parent)this.MdiParent;    // Accéder à CentreTyrannus (formulaire parent)

                // Mettre à jour les boutons Gras, Italic et Souligné 
                if (infoRichTextBox.SelectionFont != null)  // Du texte ayant un style a été sélectionné
                {
                    oParent.boldToolStripButton.Checked = infoRichTextBox.SelectionFont.Bold;
                    oParent.italicToolStripButton.Checked = infoRichTextBox.SelectionFont.Italic;
                    oParent.underlineToolStripButton.Checked = infoRichTextBox.SelectionFont.Underline;
                }

                if (Clipboard.ContainsText() || Clipboard.ContainsImage())  // Texte ou image présent dans le clipboard
                {
                    oParent.pasteToolStripMenuItem.Enabled = true;
                    oParent.pasteToolStripButton.Enabled = true;
                }

                // Mettre à jour les boutons copier, coller, couper, sélectionner et effacer
                if (infoRichTextBox.SelectionLength > 0)    // Du texte a été sélectionné
                {
                    oParent.copyToolStripButton.Enabled = true;
                    oParent.copyToolStripMenuItem.Enabled = true;
                    oParent.cutToolStripButton.Enabled = true;
                    oParent.cutToolStripMenuItem.Enabled = true;
                    oParent.pasteToolStripButton.Enabled = true;
                    oParent.pasteToolStripMenuItem.Enabled = true;
                    oParent.clearToolStripMenuItem.Enabled = true;
                    oParent.selectToolStripMenuItem.Enabled = true;
                }
                else
                {
                    oParent.copyToolStripButton.Enabled = false;
                    oParent.copyToolStripMenuItem.Enabled = false;
                    oParent.cutToolStripButton.Enabled = false;
                    oParent.cutToolStripMenuItem.Enabled = false;
                    oParent.pasteToolStripButton.Enabled = false;
                    oParent.pasteToolStripMenuItem.Enabled = false;
                    oParent.clearToolStripMenuItem.Enabled = false;
                    oParent.selectToolStripMenuItem.Enabled = false;
                }

                // Mettre à jour les boutons d'alignement du texte 
                if (infoRichTextBox.SelectionAlignment == HorizontalAlignment.Left)
                {
                    oParent.leftJustifyToolStripButton.Checked = true;
                    oParent.centeredToolStripButton.Checked = false;
                    oParent.rightJustifyToolStripButton.Checked = false;
                }
                else if (infoRichTextBox.SelectionAlignment == HorizontalAlignment.Center)
                {
                    oParent.leftJustifyToolStripButton.Checked = false;
                    oParent.centeredToolStripButton.Checked = true;
                    oParent.rightJustifyToolStripButton.Checked = false;
                }
                else if (infoRichTextBox.SelectionAlignment == HorizontalAlignment.Right)
                {
                    oParent.leftJustifyToolStripButton.Checked = false;
                    oParent.centeredToolStripButton.Checked = false;
                    oParent.rightJustifyToolStripButton.Checked = true;
                }                
                else
                {
                    // rien faire
                }
            }
            catch(Exception) 
            {
                MessageBox.Show(g.tMessagesErreurStr[(int)ce.ceErreurIndeterminee],
                                this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        #endregion

        #region Etat des menus et de la barre d'outils en fonction du stagiaire actif

        private void Stagiaire_Activated(object sender, EventArgs e)
        {
            infoRichTextBox_SelectionChanged(null, null);
        }

        #endregion

        #region Gérer les styles du texte

        public void ChangerAttributsPolice(FontStyle style)
        {
            try
            {
                if (infoRichTextBox.SelectionFont != null)
                {
                    if (infoRichTextBox.SelectionFont.FontFamily.IsStyleAvailable(style))
                    {
                        infoRichTextBox.SelectionFont = new Font(infoRichTextBox.SelectionFont.FontFamily,
                            infoRichTextBox.SelectionFont.Size, infoRichTextBox.SelectionFont.Style ^ style);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show(g.tMessagesErreurStr[(int)ce.ceErreurStyleTexte],
                                "Style du texte", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
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
                parentForm.fichierStr = this.Text;

                SaveFileDialog sfd = parentForm.institutTyrannusSaveFileDialog; //sfd: save file dialog
                sfd.FileName = parentForm.fichierStr;   // le nom par défaut du fichier enregistré
                                                        // est celui du formulaire actif

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
