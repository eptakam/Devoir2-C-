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
                        Enregistrer et ouvrir les fichiers portant l'extension .RTF
                        Activer/Désactiver les boutons et les menus lorsque les stagiaires ouvrent et ferment
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
using System.Runtime.Remoting;

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
            DesactiverOperationsMenuBarreOutils();
        }

        #endregion        

        #region Création d'un nouveau stagiaire

        private void NewToolStripItem_Click(object sender, EventArgs e)
        {
            try
            {
                ActiverOperationsMenusBarreOutils();
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
                ActiverOperationsMenusBarreOutils();

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

        #region Edition du texte

        private void EditionTexte_Click(object sender, EventArgs e)
        {
            try
            {
                Stagiaire oStagiaire = new Stagiaire();
                RichTextBox oInfoRichTextBox;

                // Rechercher l'indice du RichTextBox
                int indiceRichTextBoox = -1; // aucun RichTextBox n'a été trouvé

                if (this.ActiveMdiChild != null)    // un formulaire Stagiaire est actif
                {
                    for (int i = 0; i < this.ActiveMdiChild.Controls.Count; i++)
                    {
                        if (this.ActiveMdiChild.Controls[i] is RichTextBox)
                        {
                            indiceRichTextBoox = i;
                            break;
                        }
                    }
                }

                if (indiceRichTextBoox != -1)   // un RichTexte a été trouvé
                {
                    oInfoRichTextBox = (RichTextBox)this.ActiveMdiChild.Controls[indiceRichTextBoox];  // Stagiaire courant

                    // Couper, Copier et Coller le texte sélectionné au presse-papier
                    // il y a 2 zones: Menu fichier (ToolStripMenuItem) et la la barre d'outils (ToolStripButton)

                    if (sender == cutToolStripMenuItem || sender == cutToolStripButton)
                    {
                        oInfoRichTextBox.Cut();
                        oStagiaire.infoRichTextBox.Modified = true;
                    }
                    else if (sender == copyToolStripMenuItem || sender == copyToolStripButton)
                    {
                        oInfoRichTextBox.Copy();
                        oStagiaire.infoRichTextBox.Modified = false;
                    }
                    else if (sender == pasteToolStripMenuItem || sender == pasteToolStripButton)
                    {
                        oInfoRichTextBox.Paste();
                        oStagiaire.infoRichTextBox.Modified = true;
                    }
                    else if (sender == clearToolStripMenuItem)
                    {
                        oInfoRichTextBox.Clear();
                    }
                    else
                    {
                        oInfoRichTextBox.SelectAll();
                    }
                }
                else    // Aucun RichTextBox n'a été trouvé
                {
                    //???
                }
            }
            catch(Exception)
            {
                MessageBox.Show(g.tMessagesErreurStr[(int)ce.ceErreurIndeterminee],
                                 this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        #endregion

        #region Aligner le texte

        private void Alignement_Click(object sender, EventArgs e)
        {
            try
            {
                Stagiaire stagiaireCourant = (Stagiaire)this.ActiveMdiChild;

                if (sender == leftJustifyToolStripButton)
                {
                    stagiaireCourant.infoRichTextBox.SelectionAlignment = HorizontalAlignment.Left;

                    leftJustifyToolStripButton.Checked = true;
                    centeredToolStripButton.Checked = false;    
                    rightJustifyToolStripButton.Checked = false;
                }
                else if (sender == centeredToolStripButton)
                {
                    stagiaireCourant.infoRichTextBox.SelectionAlignment = HorizontalAlignment.Center;

                    leftJustifyToolStripButton.Checked = false;
                    centeredToolStripButton.Checked = true;
                    rightJustifyToolStripButton.Checked = false;
                }
                else
                {
                    stagiaireCourant.infoRichTextBox.SelectionAlignment = HorizontalAlignment.Right;

                    leftJustifyToolStripButton.Checked = false;
                    centeredToolStripButton.Checked = false;
                    rightJustifyToolStripButton.Checked = true;
                }
            }
            catch(Exception)
            {
                MessageBox.Show(g.tMessagesErreurStr[(int)ce.ceErreurAlignerTexte],
                                "Aligner le texte", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion

        #region Style de police

        private void StylePolice_Click(object sender, EventArgs e)
        {
            try
            {
                Stagiaire stagiaireCourant = (Stagiaire)this.ActiveMdiChild;

                if (sender == boldToolStripButton)  // le bouton Gras est pressé
                {
                    stagiaireCourant.ChangerAttributsPolice(FontStyle.Bold);
                }
                else if (sender == italicToolStripButton)   // le bouton Italic est pressé
                {
                    stagiaireCourant.ChangerAttributsPolice(FontStyle.Italic);
                }
                else    // le bouton Souligné est pressé
                {
                    stagiaireCourant.ChangerAttributsPolice(FontStyle.Underline);
                }
            }
            catch (Exception)
            {
                MessageBox.Show(g.tMessagesErreurStr[(int)ce.ceErreurStyleTexte],
                                "Style du texte", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion

        #region Désactiver les opérations

        private void DesactiverOperationsMenuBarreOutils()
        {
            try
            {
                // Désactiver la barre des menus
                foreach (ToolStripItem oToolStripItem in institutTyrannusMenuStrip.Items)
                {
                    if (oToolStripItem is ToolStripMenuItem)
                    {
                        foreach (ToolStripItem oSousToolStripItem in (oToolStripItem as ToolStripMenuItem).DropDownItems)
                        {
                            oSousToolStripItem.Enabled = false;

                            if (oSousToolStripItem is ToolStripMenuItem)
                            {
                                foreach (ToolStripItem oSousSousToolStripItem in (oSousToolStripItem as ToolStripMenuItem).DropDownItems)
                                {
                                    oSousSousToolStripItem.Enabled = false;
                                }
                            }
                        }
                    }
                }

                // Activer les menus appropriés de la barre des menus

                // Menu fichier
                for (int i = 0; i < fileToolStripMenuItem.DropDownItems.Count; i++)
                {
                    if (i != 2 && i != 4 && i != 5)
                        fileToolStripMenuItem.DropDownItems[i].Enabled = true;
                }

                // Menu affichage
                toolsBarToolStripMenuItem.Enabled = true;

                foreach (ToolStripItem oToolStripItem in (toolsBarToolStripMenuItem.DropDownItems))
                    oToolStripItem.Enabled = true;

                // Menu aide
                helpToolStripMenuItem.Enabled = true;

                foreach (ToolStripItem oToolStripItem in (helpToolStripMenuItem.DropDownItems))
                    oToolStripItem.Enabled = true;

                // toolStripTextBox
                typeQuestionToolStripTextBox.Enabled = true;

                // Désactiver les boutons de la barre d'outils
                foreach (ToolStripItem oToolStripItem in (institutTyrannusToolStrip.Items))
                    oToolStripItem.Enabled = false;

                leftJustifyToolStripButton.Checked = false;
                    

                // Activer les boutons appropriés de la barre d'outils
                newToolStripButton.Enabled = true;
                openToolStripButton.Enabled = true;
                helpToolStripButton.Enabled = true;

            }
            catch (Exception)
            {
                MessageBox.Show(g.tMessagesErreurStr[(int)ce.ceErreurIndeterminee],
                                 "Désactiver les menus et les boutons", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        #endregion

        #region Activer les opérations

        private void ActiverOperationsMenusBarreOutils()
        {
            try
            {
                // Activer la barre des menus
                foreach (ToolStripItem oToolStripItem in institutTyrannusMenuStrip.Items)
                {
                    if (oToolStripItem is ToolStripMenuItem)
                    {
                        foreach (ToolStripItem oSousToolStripItem in (oToolStripItem as ToolStripMenuItem).DropDownItems)
                        {
                            oSousToolStripItem.Enabled = true;

                            if (oSousToolStripItem is ToolStripMenuItem)
                            {
                                foreach (ToolStripItem oSousSousToolStripItem in (oSousToolStripItem as ToolStripMenuItem).DropDownItems)
                                {
                                    oSousSousToolStripItem.Enabled = true;
                                }
                            }
                        }
                    }
                }

                // Désactiver les menus appropriés de la barre des menus

                // Menu fichier
                saveToolStripMenuItem.Enabled = false;

                // Menu édition
                for (int i = 0; i < editToolStripMenuItem.DropDownItems.Count; i++)
                {
                    if (i == 2 )
                    {
                        if (Clipboard.ContainsText())
                            pasteToolStripMenuItem.Enabled = Clipboard.GetDataObject().GetDataPresent("System.String");
                        if (Clipboard.ContainsImage())
                            pasteToolStripMenuItem.Enabled = Clipboard.GetDataObject().GetDataPresent(DataFormats.MetafilePict);
                    }                        
                    else
                        editToolStripMenuItem.DropDownItems[i].Enabled = false;
                }

                // Activer les boutons de la barre d'outils
                foreach (ToolStripItem oToolStripItem in (institutTyrannusToolStrip.Items))
                    oToolStripItem.Enabled = true;
                
                if (Clipboard.ContainsText()) // Texte présent dans le clipboard
                    leftJustifyToolStripButton.Checked = true;

                // Désactiver les menus appropriés de la barre des menus
                cutToolStripButton.Enabled = false;
                copyToolStripButton.Enabled = false;

                if (Clipboard.ContainsText() || Clipboard.ContainsImage()) // présence du texte ou d'une image dans le clipboard
                    pasteToolStripButton.Enabled = true;
            }
            catch(Exception)
            {
                MessageBox.Show(g.tMessagesErreurStr[(int)ce.ceErreurIndeterminee],
                                 "Activer les menus et les boutons", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        #endregion

        #region Vérifier état des stagiaires

        private void Parent_MdiChildActivate(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild == null)
                DesactiverOperationsMenuBarreOutils();
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

                    if (oToolStripMenuItem == saveAsToolStripMenuItem)
                        oStagiaire.EnregistrerSous();
                    else
                        oStagiaire.Enregistrer();
                }
            }
            catch(Exception)
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

        #endregion
    }
}
