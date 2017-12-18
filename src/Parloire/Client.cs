//ETML
//Auteur : JMY
//Date : 15.03.2016
//Description : Client du chat

using System;
using System.Net.Sockets;
using System.Text;
using System.Net;

namespace Parloire
{
    /// <summary>
    /// Client réseau UDP qui se connecte à un serveur pour envoyer et recevoir des messages
    /// </summary>
    class Client
    {
        //Conneion réseau
        UdpClient socket;

        //Adresses réseau
        private IPAddress serverIp;
        private string clientIp;

        //Pour recevoir les mises à jour du serveur (messages des autres participants)
        System.Threading.Thread listener;

        //Etat de connexion
        volatile bool running = false;

        //Pour mettre à jour l'affichage des messages
        private ChatWindow chatWindow;
        //Pour informer le serveur de qui parle
        private string nickname;

        public Client(ChatWindow chatWindow, IPAddress serverIp, string nickname)
        {
            this.serverIp = serverIp;
            this.chatWindow = chatWindow;
            this.nickname = nickname;

            clientIp = getEtmlIp().ToString();
        }

        public Client(ChatWindow chatWindow, String serverIp, string nickname) : this(chatWindow, IPAddress.Parse(serverIp), nickname) { }

        /// <summary>
        /// Permet de récupérer l'adresse IP sur l'interface réseau ETML
        /// </summary>
        /// <returns></returns>
        public static IPAddress getEtmlIp()
        {
            string hostName = Dns.GetHostName();
            foreach (IPAddress candidate in Dns.GetHostByName(hostName).AddressList)
            {
                //ETML address
                if (candidate.ToString().StartsWith("172"))
                {
                    return candidate;
                }
            }

            return IPAddress.Parse("127.0.0.1");
        }

        /// <summary>
        /// Connexion au serveur et démarrage du thread pour écouter les notifications du serveur (nouveaux messages)
        /// </summary>
        public void start()
        {
            IPEndPoint ipEndpoint = new IPEndPoint(serverIp, Server.PORT);
            socket = new UdpClient();
            socket.Connect(ipEndpoint);
            send("--" + nickname + " has joined, welcome!", false);

            running = true;
            listener = new System.Threading.Thread(waitForIncomingMessage);
            listener.Start();

        }

        /// <summary>
        /// Permet d'envoyer un message au serveur en ajoutant optionnellemet le pseudo
        /// </summary>
        /// <param name="text">le message à envoyer</param>
        /// <param name="prependNickName">vrai si on doit ajouter le pseudo</param>
        private void send(string text, bool prependNickName)
        {
            //Vérification du socket
            if (socket != null && socket.Client.Connected)
            {
                //Envoi du message
                byte[] message = Encoding.ASCII.GetBytes((prependNickName ? nickname + ":" : "") + text);
                socket.Send(message, message.Length);
            }
            else
            {
                throw new Exception("Client not connected, message cannot be sent");
            }
        }

        /// <summary>
        /// Envoi avec pseudo forcé
        /// </summary>
        /// <param name="text">le message</param>
        public void send(string text)
        {
            send(text, true);
        }

        /// <summary>
        /// Fermeture du socket et arrêt du thread associé
        /// </summary>
        public void stop()
        {
            running = false;
            socket.Close();
            listener.Join(1000);//on attend encore max 1sec. pour que le thread s'arrête
        }

        /// <summary>
        /// Démarrage du thread pour écouter les messages venant du serveur
        /// </summary>
        private void waitForIncomingMessage()
        {
            //On écoute sur le port dynamiquement alloué par l'OS pour ce client et uniquement les messages venant de l'IP du serveur
            int port = ((IPEndPoint)socket.Client.RemoteEndPoint).Port;
            IPEndPoint sender = new IPEndPoint(serverIp, port);

            Console.WriteLine("[Client-" + DateTime.Now + "]Waiting for incoming message from " + serverIp + ":" + port);

            //Tant qu'on veut chatter, on va attendre un message du serveur
            while (running)
            {
                try
                {
                    //Attend un nouveau message
                    byte[] message = socket.Receive(ref sender);

                    //Affichage dans la fenêtre de chat
                    chatWindow.updateContent(Encoding.ASCII.GetString(message));

                }
                catch (Exception e)
                {
                    //Si on a arrêté, c'est normal que le socket ne puisse plus écouter, sinon c'est qu'il y a eu un problème
                    if (running)
                    {
                        Console.WriteLine("[Client-" + DateTime.Now + "]Socket error " + e);
                        running = false;//on force l'arrêt du thread
                        socket.Close();

                        //On notifie l'UI
                        chatWindow.disconnected();
                    }

                }
            }

        }
    }
}
