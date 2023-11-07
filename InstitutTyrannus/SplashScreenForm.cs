/*
        Programmeurs:   Ange Yemele, 
                        Ansoumane Condé,
                        Dorian Wontcheu,
                        Emmanuel Takam,
                        Yannis-Arthur Nenzeko

        Date:           Octobre 2023

        Solution:       InstitutTyrannus.sln
        Projet:         InstitutTyrannus.csproj
        Classe:         SplashScreenForm.cs

        But:            Présenter le logo de Tyrannus au démarrage du formulaire
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InstitutTyrannus
{
    public partial class SplashScreenForm : Form
    {
        #region Constructeur

        public SplashScreenForm()
        {
            InitializeComponent();
        }

        #endregion

        #region Initialiser

        private void SplashScreenForm_Load(object sender, EventArgs e)
        {            
            cartePictureBox.Controls.Add(tyrannusLabel);    // Afficher un label sur un pictureBox
        }

        #endregion

        #region SplashScreen

        private void splashScreenTimer_Tick(object sender, EventArgs e)
        {
            splashScreenProgressBar.Increment(25);  // Evolution de la progressBar

            if (splashScreenProgressBar.Value == 100)
                this.Close();
        }

        #endregion
    }
}
