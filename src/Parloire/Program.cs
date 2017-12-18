//ETML
//Auteur : JMY
//Date : 15.03.2016
//Description : Chat basique
using System;
using System.Windows.Forms;
namespace Parloire
{
    /// <summary>
    /// Démarre un chat basique en affichant la fenêtre principale
    /// </summary>
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ChatWindow());
        }
    }
}
