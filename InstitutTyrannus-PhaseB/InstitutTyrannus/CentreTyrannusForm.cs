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

namespace InstitutTyrannus
{
    public partial class Parent : Form
    {
        #region Membres privées

        private int nombreInt;

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

        #region Méthodes privées

        private void AssocierImages()
        {
            newToolStripMenuItem.Image = newToolStripButton.Image;
            openToolStripMenuItem.Image = openToolStripButton.Image;
            saveAsToolStripMenuItem.Image = saveAsToolStripButton.Image;
            cutToolStripMenuItem.Image = cutToolStripButton.Image;
            copyToolStripMenuItem.Image = copyToolStripButton.Image;
            pasteToolStripMenuItem.Image = pasteToolStripButton.Image;
            helpOnCenterToolStripMenuItem.Image = helpToolStripButton.Image;
        }

        private void InitialiserVariables()
        {
            nombreInt = 0;
        }

        #endregion
    }
}
