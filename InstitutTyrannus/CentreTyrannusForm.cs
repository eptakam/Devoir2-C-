/*
        Programmeurs:   Ange Yemele, 
                        Ansoumane Condé,
                        Dorian Wontcheu,
                        Emmanuel Takam,
                        Yannis-Arthur Nenzeko

        Date:           Octobre 2023

        Solution:       InstitutTyrannus.sln
        Projet:         InstitutTyrannus.csproj
        Classe:         CentreTyrannusForm.cs

        But:            Créer une interface MDI
                        Ajouter une barre de menu, d'outils et d'état
                        Créer un nouveau stagiaire
                        Enregistrer et ouvrir les fichier portant l'extension rtf
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using g = InstitutTyrannus.InstitutTyrannusClass;
using ce = InstitutTyrannus.InstitutTyrannusClass.CodeErreurs;
using System.Security.Cryptography;
using System.IO;
using System.Globalization;

namespace InstitutTyrannus
{
    public partial class Parent : Form
    {
        #region Membres privées

        private int nombreInt;

        private string titreOpenStr;
        internal string titreSaveStr;

        internal string filtreStr;
        internal int indexFiltreInt;
        internal string extensionStr;    // extension par defaut des fichiers
        internal string fichierStr;      // nom de fichier par defaut
        internal bool extensionBool;     // Verifier l'extension lors de la sauvegarde
        internal bool verificationCheminBool;


        #endregion

        #region Constructeur

        public Parent()
        {
            InitializeComponent();
        }

        #endregion

        #region Initialiser

        private void Parent_Load(object sender, EventArgs e)
        {
            g.InitMessagessErreurs();
            InitialiserVariables();
            AssocierImages();          
            DesactiverOperationsMenusBarreOutils();
            VerifierCaps(); // Phase E
        }

        #endregion        

        #region Création d'un nouveau stagiaire

        private void NewToolStripItem_Click(object sender, EventArgs e)
        {
            try
            {
                nombreInt++;
                Stagiaire oStagiaire = new Stagiaire();
                oStagiaire.MdiParent = this;
                oStagiaire.Text += nombreInt;
                oStagiaire.Show();
            }
            catch(Exception ex)
            {
                MessageBox.Show(g.tMessagesErreurStr[(int)ce.ceErreurNewDocument] + Environment.NewLine + Environment.NewLine + ex.ToString(), "Erreur création nouveau document", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        #endregion

        #region Style des menus

        private void StyleToolStripMenuItems_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oToolStripMenuItem = sender as ToolStripMenuItem;

            g.EnleverLesCrochetsSousMenu(toolsBarToolStripMenuItem);

            oToolStripMenuItem.Checked = true;

            institutTyrannusMenuStrip.RenderMode = (ToolStripRenderMode)(toolsBarToolStripMenuItem.DropDownItems.IndexOf(oToolStripMenuItem) + 1);
        }

        #endregion

        #region Menu Fenêtre

        private void WindowMDILayout_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oToolStripMenuItem = sender as ToolStripMenuItem;

            g.EnleverLesCrochetsSousMenu(windowToolStripMenuItem);

            oToolStripMenuItem.Checked = true;

            this.LayoutMdi((MdiLayout)(windowToolStripMenuItem.DropDownItems.IndexOf(oToolStripMenuItem)));
        }

        #endregion

        #region Menu et barre d'outils dans les panneaux

        private void ToolStripPanel_ControlAdded(object sender, ControlEventArgs e)
        {
            ToolStripPanel oToolStripPanel = sender as ToolStripPanel;

            if (e.Control == institutTyrannusMenuStrip)
            {
                if (oToolStripPanel == topToolStripPanel || oToolStripPanel == bottomToolStripPanel)
                {
                    institutTyrannusMenuStrip.TextDirection = ToolStripTextDirection.Horizontal;
                    institutTyrannusMenuStrip.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
                    typeQuestionToolStripTextBox.Visible = true;
                }
                else
                {
                    typeQuestionToolStripTextBox.Visible = false;

                    if (oToolStripPanel == leftToolStripPanel)
                    {
                        institutTyrannusMenuStrip.TextDirection = ToolStripTextDirection.Vertical270;
                        institutTyrannusMenuStrip.LayoutStyle = ToolStripLayoutStyle.VerticalStackWithOverflow;
                    }
                    else
                    {
                        institutTyrannusMenuStrip.TextDirection = ToolStripTextDirection.Vertical90;
                        institutTyrannusMenuStrip.LayoutStyle = ToolStripLayoutStyle.VerticalStackWithOverflow;
                    }
                }
            }
            else
            {
                if (oToolStripPanel == topToolStripPanel || oToolStripPanel == bottomToolStripPanel)
                {
                    institutTyrannusToolStrip.TextDirection = ToolStripTextDirection.Horizontal;
                    institutTyrannusToolStrip.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
                    fontToolStripComboBox.Visible = true;
                    sizeFontToolStripComboBox.Visible = true;
                }
                else
                {
                    fontToolStripComboBox.Visible = false;
                    sizeFontToolStripComboBox.Visible = false;

                    if (oToolStripPanel == leftToolStripPanel)
                    {
                        institutTyrannusToolStrip.TextDirection = ToolStripTextDirection.Vertical270;
                        institutTyrannusToolStrip.LayoutStyle = ToolStripLayoutStyle.VerticalStackWithOverflow;
                    }
                    else
                    {
                        institutTyrannusToolStrip.TextDirection = ToolStripTextDirection.Vertical90;
                        institutTyrannusToolStrip.LayoutStyle = ToolStripLayoutStyle.VerticalStackWithOverflow;
                    }
                }
            }
        }

        #endregion

        #region Ouvrir un stagiaire

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                if (institutTyrannusOpenFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Stagiaire oStagiaire = new Stagiaire();
                    oStagiaire.MdiParent = this;
                    oStagiaire.Text = institutTyrannusOpenFileDialog.FileName;

                    RichTextBox ortf = new RichTextBox();   // ortf: objet richTextBox format

                    ortf.LoadFile(institutTyrannusOpenFileDialog.FileName);

                    // supprimer le contenu issue des controles dans le document rtf (les premières lignes) sauvegardé
                    // pour pouvoir placer les infos du fichier .rtf dans le richTextBox

                    oStagiaire.idMaskedTextBox.Text = ortf.Lines[0];
                    oStagiaire.nomTextBox.Text = ortf.Lines[1];
                    oStagiaire.telephoneMaskedTextBox.Text = ortf.Lines[2];

                    for (int i = 0; i < 3; i++)
                    {
                        ortf.SelectionStart = 0;
                        ortf.SelectionLength += ortf.Lines[0].Length + 1;
                        ortf.SelectedText = String.Empty;
                    }

                    oStagiaire.infoRichTextBox.Rtf = ortf.Rtf;
                    oStagiaire.Enregistrement = true;
                    oStagiaire.infoRichTextBox.Modified = false;     // juste pour le rtf
                    oStagiaire.Modification = false;   // pour les autres controles

                    oStagiaire.Show();
                }
            }
            catch (Exception)
            {
                MessageBox.Show(g.tMessagesErreurStr[(int)ce.ceErreurOpenDocument], 
                                "Ouvrir un document", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion

        #region Enregistrer

        private void SaveAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.ActiveMdiChild != null)
                {
                    ToolStripMenuItem oToolStripMenuItem = sender as ToolStripMenuItem;

                    Stagiaire oStagiaire;
                    oStagiaire = (Stagiaire)this.ActiveMdiChild;

                    if (oToolStripMenuItem == saveToolStripMenuItem)
                        oStagiaire.EnregistrerSous();
                    else
                        oStagiaire.Enregistrer();
                }
            }
            catch (Exception)
            {
                MessageBox.Show(g.tMessagesErreurStr[(int)ce.ceErreurIndeterminee],
                 this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            
        }

        #endregion

        #region Fermer un stagiaire

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild != null)
                this.ActiveMdiChild.Close();
        }

        #endregion

        #region Fermer l'application

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Méthodes privées

        private void AssocierImages()
        {
            newToolStripMenuItem.Image = newToolStripButton.Image;
            openToolStripMenuItem.Image = openToolStripButton.Image;
            saveAsToolStripMenuItem.Image = saveToolStripButton.Image;
            cutToolStripMenuItem.Image = cutToolStripButton.Image;
            copyToolStripMenuItem.Image = copyToolStripButton.Image;
            pasteToolStripMenuItem.Image = pasteToolStripButton.Image;
            helpOnCenterToolStripMenuItem.Image = helpToolStripButton.Image;
        }

        private void InitialiserVariables()
        {
            nombreInt = 0;
            titreOpenStr = "Ouvrir un stagiaire...";
            titreSaveStr = "Enregistrer un stagiaire...";
            titreOpenStr = "Ouvrir un stagiaire...";
            titreSaveStr = "Enregistrer un stagiaire...";
            filtreStr = "Fichiers rtf (*.rtf)|*.rtf|Tous les fichiers (*.*)|*.*";
            indexFiltreInt = 0;
            extensionStr = "rtf";
            fichierStr = "Stagiaire";
            extensionBool = true;
            verificationCheminBool = true;

            // OpenFileDialog
            institutTyrannusOpenFileDialog.Title = titreOpenStr;
            
            institutTyrannusOpenFileDialog.Filter = filtreStr;
            institutTyrannusOpenFileDialog.FilterIndex = indexFiltreInt;
            institutTyrannusOpenFileDialog.DefaultExt = extensionStr;
            institutTyrannusOpenFileDialog.AddExtension = extensionBool;
            institutTyrannusOpenFileDialog.FileName = fichierStr;
            institutTyrannusOpenFileDialog.CheckPathExists = verificationCheminBool;

            // SaveFileDialog
            institutTyrannusSaveFileDialog.Title = titreSaveStr;
            
            institutTyrannusSaveFileDialog.Filter = filtreStr;
            institutTyrannusSaveFileDialog.FilterIndex = indexFiltreInt;
            institutTyrannusSaveFileDialog.DefaultExt = extensionStr;
            institutTyrannusSaveFileDialog.AddExtension = extensionBool;
            institutTyrannusSaveFileDialog.FileName = fichierStr;
            institutTyrannusSaveFileDialog.CheckPathExists = verificationCheminBool;
        }

        private void DesactiverOperationsMenusBarreOutils()
        {
            // Desactiver tout le Menu
            foreach (ToolStripItem oToolStripItem in institutTyrannusMenuStrip.Items)
            {
                if (oToolStripItem is ToolStripMenuItem)
                {
                    foreach (ToolStripItem oSousToolStripItem in (oToolStripItem as ToolStripMenuItem).DropDownItems)
                    {
                        //if (oSousToolStripItem is ToolStripMenuItem)
                        //    oSousToolStripItem.Enabled = false;
                    }
                        
                }
                    
            }

            // Activer quelque menus

            //foreach (ToolStripItem oToolStripItem in institutTyrannusMenuStrip.Items)
            //{
            //    foreach (ToolStripItem oSousToolStripItem in (oToolStripItem as ToolStripMenuItem).DropDownItems)
            //    {

            //    }
            //}
              
            for (int i = 0; i < fileToolStripMenuItem.DropDownItems.Count; i++)
            {
                if (i == 0 || i == 1 || i == 7)
                    fileToolStripMenuItem.DropDownItems[i].Enabled = true;
            }

            toolsBarToolStripMenuItem.Enabled = true;

            foreach (ToolStripItem oToolStripItem in (toolsBarToolStripMenuItem.DropDownItems))
                oToolStripItem.Enabled = true;

            // Menu aide
            helpToolStripMenuItem.Enabled = true;

            foreach (ToolStripItem oToolStripItem in (helpToolStripMenuItem.DropDownItems))
                oToolStripItem.Enabled = true;


        }

        #endregion

        #region Menu Edition
        private void EditionText_Click(object sender, EventArgs e)
        {
            try
            {
                Stagiaire oStalgiaire = new Stagiaire();
                RichTextBox oInfoRichTextBox;
                // L'indice de oInfoRichTextBox sur le stagiaire est 7

                oInfoRichTextBox = (RichTextBox)this.ActiveMdiChild.Controls[7];

                if (sender == cutToolStripButton || sender == cutToolStripMenuItem)
                {
                    oInfoRichTextBox.Cut();
                    oStalgiaire.infoRichTextBox.Modified = true;    // 
                }
                //else if(sender == )
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        #endregion

        #region Phase E

        string statusInsertion = "INS";
        private void VerifierCaps()
        {
            if (System.Console.CapsLock)
                capsToolStripStatusLabel.Text = "MAJ";
            else
                capsToolStripStatusLabel.Text = "    ";
        }

        private void cultureToolStripButton_Click(object sender, EventArgs e)
        {
            languageToolStripStatusLabel.Text = CultureInfo.CurrentCulture.NativeName;
        }

        private void Parent_KeyDown(object sender, KeyEventArgs e)
        {
            if (Control.IsKeyLocked(Keys.CapsLock))
                capsToolStripStatusLabel.Text = "MAJ";
            else
                capsToolStripStatusLabel.Text = "   ";

            if (e.KeyCode == Keys.Insert)
            {
                if (statusInsertion == "INS")
                    statusInsertion = "RFP";
                else
                    statusInsertion = "INS";

            }
        }
        #endregion

        #region Phase G
        private void RechercherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.OwnedForms.Length == 0 && this.ActiveMdiChild != null)
            {
                try
                {
                    RechercherForm oRecherche = new RechercherForm();

                    oRecherche.Owner = this;

                    oRecherche.Mot = (this.ActiveMdiChild.Controls[7] as RichTextBox).SelectedText;

                    oRecherche.Show();
                }
                catch (Exception)
                {
                    MessageBox.Show(g.tMessagesErreurStr[(int)ce.ceErreurIndeterminee],
                        this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }




        #endregion
        
    }
}
