/*
        Programmeurs:   Ange Yemele, 
                        Ansoumane Condé,
                        Dorian Wontcheu,
                        Emmanuel Takam,
                        Yannis-Arthur Nenzeko

        Date:           Novembre 2023

        Solution:       InstitutTyrannus.sln
        Projet:         InstitutTyrannus.csproj
        Classe:         CentreTyrannusForm.cs

        But:            Créer une interface MDI
                        Ajouter une barre de menu, d'outils et d'état
                        Créer un nouveau stagiaire
                        Enregistrer et ouvrir les fichiers portant l'extension .RTF
                        Activer/Désactiver les boutons et les menus lorsque les stagiaires ouvrent et ferment
                        Mettre à jour l'état des boutons de la barre d'état
                        Activer/Désactiver les boutons et les menus
                        Changer la police du texte sélectionné
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
using System.Globalization;
using System.Drawing.Text;

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
        string culture;
        string statusInsertion;

        private ComboBox oComboBox;
        FontDialog fontDialog;

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
            VerifierCaps();
            RemoveHandlerComboBox();
            DrawComboBox();
            AfficherPolicesInstallees();
            AddHandlerComboBox();
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
                oStagiaire.StatusInsert = true;     // Le mode INSERTION est activée
                oStagiaire.Show();
                
            }
            catch (Exception ex)
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
                    oStagiaire.StatusInsert = true;     // Le mode INSERTION est activée
                    sizeFontToolStripComboBox.Text = "8";
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

                if (indiceRichTextBoox != -1)   // un RichTextBox a été trouvé
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
                    MessageBox.Show(g.tMessagesErreurStr[(int)ce.ceErreurRichTextBox],
                                 "RichTextBox", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            if (this.ActiveMdiChild == null)    // Aucun stagiaire ouvert
            {
                DesactiverOperationsMenuBarreOutils();
                createToolStripStatusLabel.Text = "Créer ou ouvrir un stagiaire...";
                insertToolStripStatusLabel.Text = "          ";
            }
            else    // Un stagiaire est actif
            {
                Stagiaire oStagiaire = (Stagiaire)this.ActiveMdiChild;
                createToolStripStatusLabel.Text = oStagiaire.Text;

                if (oStagiaire.StatusInsert)    // Le mode insertion est activée
                {
                    statusInsertion = "INS";
                    insertToolStripStatusLabel.Text = statusInsertion;
                }
                else    // Le mode insertion n'est pas activée
                {
                    statusInsertion = "RFP";
                    insertToolStripStatusLabel.Text = statusInsertion;
                }
                   
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
                    createToolStripStatusLabel.Text = oStagiaire.Text;

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

        #region Méthode KeyDown

        private void Parent_KeyDown(object sender, KeyEventArgs e)
        {
            if (Control.IsKeyLocked(Keys.CapsLock))     // La touche CapsLock du clavier est pressée
                capsToolStripStatusLabel.Text = "MAJ";
            else    // La touche CapsLock du clavier n'est pas pressée
                capsToolStripStatusLabel.Text = "          ";

            Stagiaire oStagiaire = (Stagiaire)this.ActiveMdiChild;

            if (this.ActiveMdiChild != null)   // Un stagiaire est actif
            {
                if (e.KeyCode == Keys.Insert)   // La touche INSERT du clavier est presée
                {
                    if (statusInsertion == "INS")   // Le mode précédent était l'insertion 
                    {
                        statusInsertion = "RFP";
                        oStagiaire.StatusInsert = false;
                    }
                    else    // Le mode précédent était refrapper
                    {
                        statusInsertion = "INS";
                        oStagiaire.StatusInsert = true;
                    }

                    insertToolStripStatusLabel.Text = statusInsertion;  // Mettre à jour l'étiquette pour l'insertion
                }
            }
        }

        #endregion

        #region Dessinage dans le ComboBox

        private void DrawComboBox()
        {
            //notre objet fontToolStripComboBox n'a pas la methode DrawMode on obtient donc un objet comboBox a partir d'un toolStripComboBo
            oComboBox = fontToolStripComboBox.ComboBox;
            oComboBox.DrawMode = DrawMode.OwnerDrawVariable;
            oComboBox.Width *= 2;
        }

        #endregion

        #region Enlever le lien entre la methode et l'evenement pour les ComboBox de la police et la taille

        private void RemoveHandlerComboBox()
        {
            fontToolStripComboBox.SelectedIndexChanged -= nameFontToolStripComboBox_SelectedIndexChanged;
            sizeFontToolStripComboBox.SelectedIndexChanged -= sizeFontToolStripComboBox_SelectedIndexChanged;
        }

        #endregion

        #region Remettre le lien entre la methode  et l'evenement pour les ComboBox de la police et la taille

        private void AddHandlerComboBox()
        {
            fontToolStripComboBox.SelectedIndexChanged += nameFontToolStripComboBox_SelectedIndexChanged;
            sizeFontToolStripComboBox.SelectedIndexChanged += sizeFontToolStripComboBox_SelectedIndexChanged;
        }

        #endregion 

        #region Remplir les polices 

        private void AfficherPolicesInstallees()
        {
            InstalledFontCollection oInstalledFonts = new InstalledFontCollection();
            foreach (FontFamily oFontFamily in oInstalledFonts.Families)
            {
                fontToolStripComboBox.Items.Add(oFontFamily.Name);
            }

            //personnalisation de l'apparence des elements dans notre comboBox
            // on utilise le gestionnaire d'event DrawItemEventHandler et MeasureItemEventHandler pour personnaliser
            // chaque element de notre liste deroulante 
            oComboBox.DrawItem += new DrawItemEventHandler(oComboBox_DrawItem);
            oComboBox.MeasureItem += new MeasureItemEventHandler(oComboBox_MeasureItem);
            oComboBox.DisplayMember = "Name";

        }

        // Si on definit la propriété Draw sur DrawMode.OwnerDrawVariable,  
        // on doit gérer l'événement MeasureItem. Ce gestionnaire d'événement 
        // définira la hauteur et la largeur de chaque élément avant qu'il ne soit dessiné.
        private void oComboBox_MeasureItem(object sender, MeasureItemEventArgs e)
        { 
            Font aFont = new Font(oComboBox.Items[e.Index].ToString(), 10);

            // mesure la taille de notre texte lorsqu'on le dessine avec une police particuliere 
            Size tailleSize = e.Graphics.MeasureString(oComboBox.Items[e.Index].ToString(), aFont).ToSize();

            e.ItemWidth = tailleSize.Width;
            e.ItemHeight = tailleSize.Height;
        }

        private void oComboBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            // Si l'index est invalide, ne faire rien, tu commot juste 
            if (e.Index == -1) return;

            // Dessiner l'arrière-plan de l'élément sur lequel notre police est 
            e.DrawBackground();

            // Devrions-nous dessiner le rectangle de focus ?
            if ((e.State & DrawItemState.Focus) == DrawItemState.Focus)
                e.DrawFocusRectangle();

            try
            {
                // Créer un nouveau pinceau d'arrière-plan.
                Brush pinceau = new SolidBrush(e.ForeColor);

                FontStyle style;
                // obtenir la police pour chaque comboBox
                string strFont = oComboBox.Items[e.Index].ToString();

                Font oFont = new Font(strFont, 10);

                // Ceux-ci ne sont disponibles qu'en italique, pas en "régulier",
                // donc on les teste , sinon, exception !
                // certains polices ne permettent de voir leur nom dans leur police, dans ce cas on dessine avec la police Calibri, 
                // par exemple la police Wingdings 3 affiche son nom de sa famille en sa police or elle est illisible 
                if (strFont == "Monotype Corsiva" || strFont == "Brush Script MT"
                    || strFont == "Harlow Solid Italic" || strFont == "Palace Script MT"
                    || strFont == "Vivaldi" || strFont == "AGA Arabesque" || strFont == "AGA Arabesque Desktop"
                    || strFont == "MT Extra" || strFont == "MS Reference Specialty" || strFont == "MS Outlook"
                    || strFont == "Marlett" || strFont == "HoloLens MDL2 Assets" || strFont == "Bookshelf Symbol 7"
                    || strFont == "Segoe MDL2 Assets" || strFont == "Segoe Fluent Icons" || strFont == "Wingdings 3")
                {
                    // Définir le style en italique, pour éviter "Regular" & Exception
                    style = FontStyle.Bold | FontStyle.Italic | FontStyle.Regular;

                    e.Graphics.DrawString(strFont, new Font("Calibri", 8, style), pinceau, e.Bounds);
                }
                else
                {
                    e.Graphics.DrawString(strFont, oFont, pinceau, e.Bounds);
                }

            }
            catch (Exception)
            {
                MessageBox.Show(g.tMessagesErreurStr[(int)g.CodeErreurs.ceErreurIndeterminee],
                               this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        #endregion

        #region Selection de la taille d'une police dans le comboBox

        private void sizeFontToolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Stagiaire oStagiaire = (Stagiaire)this.ActiveMdiChild;

                if (oStagiaire != null)
                {
                    // Récupere la police active dans l'enfant
                    Font oRichTextBoxFont = oStagiaire.infoRichTextBox.SelectionFont;
                   
                    // verifier si  seulement une famille police est selectionne 
                    if (oRichTextBoxFont != null)
                    { 
                        // Modifie la police du texte dans le RichTextBox
                        oStagiaire.infoRichTextBox.SelectionFont = new Font(oRichTextBoxFont.Name, int.Parse(sizeFontToolStripComboBox.Text), oRichTextBoxFont.Style);

                        // Replace le focus sur l'enfant actif
                        this.ActiveMdiChild.ActiveControl.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(g.tMessagesErreurStr[(int)g.CodeErreurs.ceErreurIndeterminee] + Environment.NewLine + Environment.NewLine + ex.ToString(),
                               this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        #endregion

        #region Selection de la police dans le comboBox

        private void nameFontToolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // recupere le font du textbox selectionne dans mon richtextbox de mon formulaire enfant  
            Font oRichTextBoxFont = (this.ActiveMdiChild as Stagiaire).infoRichTextBox.SelectionFont;

            // verifier si  seulement une famille police est selectionne 
            if (oRichTextBoxFont != null)
            {
                try
                {
                    (this.ActiveMdiChild as Stagiaire).infoRichTextBox.SelectionFont = new Font(fontToolStripComboBox.Text, oRichTextBoxFont.Size, oRichTextBoxFont.Style);

                    this.ActiveMdiChild.ActiveControl.Focus();

                }
                catch (Exception)
                {
                    MessageBox.Show(g.tMessagesErreurStr[(int)g.CodeErreurs.ceErreurIndeterminee],
                                this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        #endregion

        #region Format | Police 

        private void policeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Stagiaire oStagiaire = (Stagiaire)this.ActiveMdiChild;

                //  si l'enfant actif est de type Enfant et si sa RichTextBox est non null
                if (oStagiaire != null && oStagiaire.infoRichTextBox != null)
                {
                    //  objet FontDialog et initialiser la police avec la police actuelle de la sélection
                    
                    fontDialog.MinSize = 8;
                    fontDialog.MaxSize = 16;
                    fontDialog.Font = oStagiaire.infoRichTextBox.SelectionFont;
                    fontDialog.ShowApply = true;
                    fontDialog.Apply += FontDialog_Apply;

                    if (fontDialog.ShowDialog() == DialogResult.OK)
                    {
                        // update de la police de la sélection dans la RichTextBox avec la police sélectionnée dans la boîte de dialogue
                        oStagiaire.infoRichTextBox.SelectionFont = fontDialog.Font;
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show(g.tMessagesErreurStr[(int)g.CodeErreurs.ceErreurIndeterminee],
                                this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void FontDialog_Apply(object sender, EventArgs e)
        {
            Stagiaire oStagiaire = (Stagiaire)this.ActiveMdiChild;

            oStagiaire.infoRichTextBox.SelectionFont = fontDialog.Font;
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
            culture = CultureInfo.CurrentCulture.NativeName;    // Capter la culture de la machine
            cultureToolStripStatusLabel.Text = culture;
            createToolStripStatusLabel.Text = "Créer ou ouvrir un stagiaire...";
            statusInsertion = "INS";
            fontDialog = new FontDialog();

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

        private void VerifierCaps()
        {
            if (System.Console.CapsLock)    // La touche Majuscule du clavier est pressée
                capsToolStripStatusLabel.Text = "MAJ";
            else    // // La touche Majuscule du clavie n'est pas pressée
                capsToolStripStatusLabel.Text = "          ";
        }







        #endregion

    }
}

