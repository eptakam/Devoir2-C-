/*
        Programmeurs:   Ange Yemele, 
                        Ansoumane Condé,
                        Dorian Wontcheu,
                        Emmanuel Takam,
                        Yannis-Arthur Nenzeko

        Date:           Octobre 2023

        Solution:       InstitutTyrannus.sln
        Projet:         InstitutTyrannus.csproj
        Classe:         InstitutTyrannusClass.cs

        But:            Définir les règles de l'Institut Tyrannus.

        Info:           Couche Métier
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using ce = InstitutTyrannus.InstitutTyrannusClass.CodeErreurs;

namespace InstitutTyrannus
{
    /// <summary>
    /// Classe générale (couche métier)
    /// </summary>
    internal class InstitutTyrannusClass
    {
        #region Enumeration des messagdes d'erreurs

        /// <summary>
        /// Enumération pour les messages d'erreurs
        /// </summary>
        public enum CodeErreurs
        {           
            ceErreurNewDocument,   // = 0
            ceErreurOpenDocument, // = 1
            ceErreurSaveDocument,   // 2
            ceErreurIndeterminee    // = 3
        }

        #endregion

        #region Messages d'erreurs

        public static string[] tMessagesErreurStr = new string[4];

        /// <summary>
        /// Initialiser les messages d'erreurs
        /// </summary>
        public static void InitMessagessErreurs()
        {           
            tMessagesErreurStr[(int)ce.ceErreurNewDocument] = "Impossible de créer un nouveau document.";
            tMessagesErreurStr[(int)ce.ceErreurOpenDocument] = "Vous ne pouvez ouvrir que des fichiers portant l'extension .rtf avec l'application Institut Tyrannus.";
            tMessagesErreurStr[(int)ce.ceErreurSaveDocument] = "L'extension RTF doit être utilisée.";
            tMessagesErreurStr[(int)ce.ceErreurIndeterminee] = "Une erreur indeterminée s'est produite, veuillez contacter la personne ressource.";
        }

        #endregion

        #region Menu mutuellement exclusif

        /// <summary>
        /// Enlever les crochets des sous-menus d'un menu parent
        /// </summary>
        /// <param name="parentMenu">Menu dont les sous-menus doivent être décochés</param>
        /// <remarks>Pour produire un menu mutuellement exclusif</remarks>
        public static void EnleverLesCrochetsSousMenu(ToolStripMenuItem parentMenu)
        {
            if (parentMenu != null)
            {
                foreach (ToolStripItem oToolStripItem in parentMenu.DropDownItems)
                {
                    if (oToolStripItem is ToolStripMenuItem)
                        if (!((oToolStripItem as ToolStripMenuItem).IsMdiWindowListEntry))
                            (oToolStripItem as ToolStripMenuItem).Checked = false;
                }
            }
        }

        #endregion
    }
}
