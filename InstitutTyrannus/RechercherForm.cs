
/*
        Programmeurs:   Ange Yemele, 
                        Ansoumane Condé,
                        Dorian Wontcheu,
                        Emmanuel Takam,
                        Yannis-Arthur Nenzeko

        Date:           Novembre 2023

        Solution:       InstitutTyrannus.sln
        Projet:         InstitutTyrannus.csproj
        Classe:         RechercherForm.cs

        But:            Rechercher l'ocurrence d'un mot dans un movement circulaire
                        dans le RichTextBox du StagiaireForm (infoRichTextBox).
 
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Reflection;

namespace InstitutTyrannus
{
    public partial class RechercherForm : Form
    {
        #region Propriete pour Mot

       public String Mot
       {
            get { return rechercherTextBox.Text; }
            set { rechercherTextBox.Text = value; }
       }
        
        #endregion

        #region Constructeur
        public RechercherForm()
        {
            InitializeComponent();
        }

        #endregion

        #region Suivant
        private void suivantButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.ActiveMdiChild != null)    // Si un enfant est actif
                {
                    RichTextBox oRichTextBox = new RichTextBox();   // Nouvelle instance du RichTextBox

                    oRichTextBox = (this.Owner.ActiveMdiChild as Stagiaire).infoRichTextBox;

                    int positionDepartInt = oRichTextBox.SelectionStart;    // Position de depart

                    if (oRichTextBox.SelectionLength == 0)
                    {
                        if (oRichTextBox.Find(Mot, positionDepartInt, RichTextBoxFinds.None) == -1)
                        {
                            oRichTextBox.Find(Mot, 0, RichTextBoxFinds.None);
                            //oRichTextBox.Select(positionDepartInt, oRichTextBox.Text.Length);
                            //oRichTextBox.Focus();
                        }
                    }
                    else
                    {
                        if(oRichTextBox.Find(Mot, positionDepartInt + 1, RichTextBoxFinds.None) == -1)
                        {
                            oRichTextBox.Find(Mot, 0, RichTextBoxFinds.None);
                            //oRichTextBox.Select(positionDepartInt, oRichTextBox.Text.Length);
                            //oRichTextBox.Focus();
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show(g.tMessagesErreurStr[(int)ce.ceErreurIndeterminee],
                    this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Annuler
        private void annulerButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion
    }
}
