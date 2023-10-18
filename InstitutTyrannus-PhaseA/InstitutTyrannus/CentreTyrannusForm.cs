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

namespace InstitutTyrannus
{
    public partial class Parent : Form
    {
        #region Constructeur

        public Parent()
        {
            InitializeComponent();
        }

        #endregion

        #region Initialiser

        private void Parent_Load(object sender, EventArgs e)
        {
            AssocierImages();
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

        #endregion
    }
}
