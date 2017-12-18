//ETML
//Auteur : JMY
//Date : 15.03.2016
//Description : GUI chat

using System;
using System.Windows.Forms;

namespace Parloire
{
    /// <summary>
    /// Affichage basique pour un chat
    /// TODO : 1) Régler l'ordre des focus (touche tab doit passer les contrôles dans un bon ordre)
    /// TODO : 2) Envoi du message par simple pression de "Enter" (pas besoin d'utiliser la souris pour appuyer sur le bouton)
    /// TODO : 3) Afficher l'adresse IP du serveur pour faciliter sa distribution
    /// TODO : 4) Peaufiner l'interface : Champ IP ne s'affiche que si on clique sur le radio "client",...
    /// TODO : 5) Gérer un mode "serveur dédié"
    /// </summary>
    public partial class ChatWindow : Form
    {
        //Référence sur les classes techniques qui gèrent le réseau
        Client client;
        Server server;

        //Etat de connexion
        bool running = false;

        public ChatWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Clic sur le bouton "start/stop".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStart_Click(object sender, EventArgs e)
        {
            //Vérification du pseudo
            if (txtNickname.Text == "")
            {
                MessageBox.Show("Veuillez remplir le pseudo");
                return;
            }

            //Si l'état est "off"
            if (!running)
            {
                //Mode=serveur, Démarrage du serveur
                if (rbServer.Checked)
                {
                    server = new Server();
                    running = true;
                    server.start();
                }

                //Mode=client OU server, Démarrage du client (on démarre automatique un client avec le serveur) 
                if (rbClient.Checked || rbServer.Checked)
                {
                    //En mode serveur, on récupère l'adresse IP locale ETML, sinon on prend la valeur mise dans le champ
                    client = new Client(this, rbServer.Checked ? Client.getEtmlIp().ToString() : txtServerIp.Text, txtNickname.Text);
                    running = true;
                    client.start();
                }
                else
                {
                    MessageBox.Show("Veuillez choisir le mode client ou serveur");
                }

            }
            //Etat=on, on demande l'arrêt
            else
            {
                //On arrête ce qui a été démarré
                if (client != null)
                {
                    client.stop();
                }
                if (server != null)
                {
                    server.stop();
                }
                running = false;
            }

            //Mise à jour des états des contrôles de l'interface
            activateControls(running);
        }

        /// <summary>
        /// Active / Désactive les contrôles selon l'état start/stop
        /// </summary>
        /// <param name="running"></param>
        private void activateControls(bool running)
        {
            //Adapter le texte à l'action
            btnStart.Text = running ? "Stop" : "Start";

            //Activer les éléments de manière cohérente
            rbClient.Enabled = !running;
            rbServer.Enabled = !running;
            txtServerIp.Enabled = !running;
            //btnSend.Enabled = running;
            txtNickname.Enabled = !running;

            //Efface l'ancien chat si on en démarre un nouveau
            if (running)
            {
                chatContent.Clear();
            }
        }

        //Désactivation du champ IP si pas client
        private void rbServer_CheckedChanged(object sender, EventArgs e)
        {
            txtServerIp.Enabled = false;
        }

        //Activation du champ IP si client
        private void rbClient_CheckedChanged(object sender, EventArgs e)
        {
            txtServerIp.Enabled = true;
        }

        //Envoi d'un message
        private void btnSend_Click(object sender, EventArgs e)
        {
            client.send(txtMessage.Text);
            txtMessage.Text = "";
            btnSend.Enabled = false;
        }

        //Mise à jour du contenu du chat (appelé par les éléments techniques client/server)
        public void updateContent(String text)
        {
            //Petite astuce pour garantir que ce soit le thread UI qui fasse l'update parce que si c'est le thread
            //lié au socket réseau, cela ne fonctionne pas
            //TODO : utiliser un modèle MVC pour éviter cette petite astuce
            this.Invoke((MethodInvoker)delegate
            {
                chatContent.AppendText(text + "\r\n");
            });
        }

        //Afficher un message en cas d'impossibilité de communiquer avec le serveur
        public void disconnected()
        {
            //Petite astuce pour garantir que ce soit le thread UI qui fasse l'update parce que si c'est le thread
            //lié au socket réseau, cela ne fonctionne pas
            //TODO : utiliser un modèle MVC pour éviter cette petite astuce
            this.Invoke((MethodInvoker)delegate
            {
                MessageBox.Show("Le serveur n'est pas accessible");
                running = false;
                activateControls(running);
            });
        }

        //Gérer l'activation du bouton envoyer pour éviter les textes vides
        private void txtMessage_TextChanged(object sender, EventArgs e)
        {
            btnSend.Enabled = txtMessage.Text != "";
        }

        /// <summary>
        /// Donations
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.paypal.me/JonathanMelly/1chf");
        }

    }
}
