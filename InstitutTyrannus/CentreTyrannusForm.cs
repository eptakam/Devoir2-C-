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

        But:            Enregistrer et Ouvrir les fichiers portant les extensions .rtf
                        
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

        private ComboBox oComboBox;

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

            ActiverOperationsMenusBarreOutils();
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

            for(int i = 0; i < oToolStripMenuItem.DropDownItems.Count; i++)
            {
                if (!(oToolStripMenuItem.DropDownItems[i] as ToolStripMenuItem).IsMdiWindowListEntry)
                {
                    g.EnleverLesCrochetsSousMenu(windowToolStripMenuItem);

                    oToolStripMenuItem.Checked = true;
                }
                
            }

            

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

                    for (int i = 0; i <= 2; i++)
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
                ActiverOperationsMenusBarreOutils();
            }
            catch (Exception)
            {
                MessageBox.Show(g.tMessagesErreurStr[(int)ce.ceErreurOpenDocument], 
                                "Ouvrir un document", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            

        }

        #endregion

        #region Enregistrer
        private void SaveAllToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.ActiveMdiChild != null)
                {
                    ToolStripMenuItem toolStripMenuItem = sender as ToolStripMenuItem;

                    Stagiaire oStagiaire = (Stagiaire)this.ActiveMdiChild;  

                    if (toolStripMenuItem == saveAsToolStripMenuItem)
                    {
                        oStagiaire.EnregistrerSous();
                    }
                    else
                    {
                        oStagiaire.Enregistrer();
                    }
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
            foreach(FontFamily oFontFamily in oInstalledFonts.Families)
            {
                fontToolStripComboBox.Items.Add(oFontFamily.Name);
            }




            //liaison de donnees (Datablinding)
            //oComboBox.DataSource = oInstalledFonts.Families;

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
            // police dans 
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

                // 
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

                
            }
            
  
        }





        #endregion

        #region Selection d'une police dans le comboBox

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

        #region Selection d'une taille dans le comboBox

        private void sizeFontToolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            Stagiaire oStagiaire = (Stagiaire)this.ActiveMdiChild;
            Font oRichTextBoxFont = oStagiaire.infoRichTextBox.SelectionFont;

            // si le texte selectionne sont de famille differentes .selectionFont = null
            if (oRichTextBoxFont != null)
            {
                try
                {
                    oStagiaire.infoRichTextBox.SelectionFont = new Font(oRichTextBoxFont.Name, int.Parse(sizeFontToolStripComboBox.SelectedItem.ToString()), oRichTextBoxFont.Style);

                    this.ActiveMdiChild.ActiveControl.Focus();
                }
                catch (Exception)
                {
                    MessageBox.Show(g.tMessagesErreurStr[(int)g.CodeErreurs.ceErreurIndeterminee],
                                    this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                }

            }
            else
            {
                // il me genere une ecetption, a revoir ...
                oStagiaire.infoRichTextBox.SelectionFont = new Font(oRichTextBoxFont.Name, 13, FontStyle.Regular);
            }




        }

        #endregion

        #region Desactiver les Operations des menus de barre d'Outils

        private void DesactiverOperationsMenusBarreOutils()
        {
            foreach(ToolStripItem oMainToolStripMenuItem in institutTyrannusMenuStrip.Items)
            {
                if (oMainToolStripMenuItem is ToolStripMenuItem)
                {
                    foreach(ToolStripItem oCourantToolStripMenuItem in (oMainToolStripMenuItem as ToolStripMenuItem).DropDownItems) 
                    {
                        if (oCourantToolStripMenuItem is ToolStripMenuItem)
                        {
                            oCourantToolStripMenuItem.Enabled= false;
                        }

                        if(oCourantToolStripMenuItem.Text == "&Nouveau..." || oCourantToolStripMenuItem.Text == "&Ouvrir..." 
                            || oCourantToolStripMenuItem.Text == "&Quitter" || oCourantToolStripMenuItem.Text == "&Aide sur Liste CLients...")
                        {
                            oCourantToolStripMenuItem.Enabled = true;
                        }
                    }

                }


            }

            foreach(ToolStripItem oBoutonToolStripItem in institutTyrannusToolStrip.Items)
            {
                if (oBoutonToolStripItem.Text == "&Nouveau" || oBoutonToolStripItem.Text == "&Ouvrir")
                    oBoutonToolStripItem.Enabled = true;
                else
                    oBoutonToolStripItem.Enabled = false;

            }



        }

        #endregion

        #region Activer les operations des menus de barre d'Outils

        private void ActiverOperationsMenusBarreOutils()
        {
            foreach (ToolStripItem oMainToolStripMenuItem in institutTyrannusMenuStrip.Items)
            {
                if (oMainToolStripMenuItem is ToolStripMenuItem)
                {
                    foreach (ToolStripItem oCourantToolStripMenuItem in (oMainToolStripMenuItem as ToolStripMenuItem).DropDownItems)
                    {
                        if (oCourantToolStripMenuItem is ToolStripMenuItem)
                        {
                            oCourantToolStripMenuItem.Enabled = true;
                        }

                        if ((Clipboard.ContainsText() || Clipboard.ContainsImage()) && oCourantToolStripMenuItem.Text == "&Coller")
                            oCourantToolStripMenuItem.Enabled = false;
                    }

                }


            }

            foreach (ToolStripItem oBoutonToolStripItem in institutTyrannusToolStrip.Items)
            {
                if (oBoutonToolStripItem.Text == "&Couper" || oBoutonToolStripItem.Text == "&Copier")
                    oBoutonToolStripItem.Enabled = false;
                else
                    oBoutonToolStripItem.Enabled = true;

                if ((Clipboard.ContainsText() || Clipboard.ContainsImage()) && oBoutonToolStripItem.Text == "&Coller")
                    oBoutonToolStripItem.Enabled = false;
                    

            }
           

                

        }


        #endregion


        #region Edition du texte

        private void EditionTexte_Click(object sender, EventArgs e)
        {
            Stagiaire oStagiaire = new Stagiaire();
            int index = 0;
            RichTextBox oClientRichTextBox;
           

            if (index == -1) return;

            for(int i = 0; i < this.ActiveMdiChild.Controls.Count; i++)
            {
                if (this.ActiveMdiChild.Controls[i] is RichTextBox)
                {
                    index = i;
                    break;
                }
            }

            oClientRichTextBox = (RichTextBox)this.ActiveMdiChild.Controls[index];

            // couper, copier et coller dans le texte selectionne au Clipboard

            if (oClientRichTextBox.SelectedText.Length > 0)
            {
                if (sender == cutToolStripButton || sender == cutToolStripMenuItem)
                {
                    oClientRichTextBox.Cut();
                    oStagiaire.infoRichTextBox.Modified= true;
                }    

                if (sender == copyToolStripButton || sender == copyToolStripMenuItem)
                {
                    oClientRichTextBox.Copy();
                    oStagiaire.infoRichTextBox.Modified = true;
                }
                   
                if (sender == pasteToolStripButton || sender == pasteToolStripMenuItem)
                {
                    oClientRichTextBox.Paste();
                    oStagiaire.infoRichTextBox.Modified = true;
                }


            }


            



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
            filtreStr = "fichiers.rtf Format (*.rtf)|*.rtf|Tous les fichiers (*.*)|*.*";
            indexFiltreInt = 0;
            extensionStr = "rtf";
            fichierStr = "Stagiaire";
            extensionBool = true;
            verificationCheminBool = true;

            // OpenFileDialog
            institutTyrannusOpenFileDialog.Title = titreOpenStr;
            //institutTyrannusOpenFileDialog.InitialDirectory = cheminAccesStr;
            institutTyrannusOpenFileDialog.Filter = filtreStr;
            institutTyrannusOpenFileDialog.FilterIndex = indexFiltreInt;
            institutTyrannusOpenFileDialog.DefaultExt = extensionStr;
            institutTyrannusOpenFileDialog.AddExtension = extensionBool;
            institutTyrannusOpenFileDialog.FileName = fichierStr;
            institutTyrannusOpenFileDialog.CheckPathExists = verificationCheminBool;

            // SaveFileDialog
            institutTyrannusSaveFileDialog.Title = titreSaveStr;
            //institutTyrannusSaveFileDialog.InitialDirectory = cheminAccesStr;
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
