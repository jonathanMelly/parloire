//ETML
//Auteur : JMY
//Date : 15.03.2016
//Description : Serveur du chat

using System;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace Parloire
{
    /// <summary>
    /// Server UDP pour recevoir des messages et les dispatchés aux clients connectés
    /// Pour faciliter la compréhension du programme par des novices, des tableaux simples ont été utilisés à la place de listes
    /// TODO : 1) Contrôler pseudo unique
    /// TODO : 2) Mettre de la couleur pour identifier les pseudos
    /// TODO : 3) Transformer le tableau en liste
    /// </summary>
    class Server
    {
        public const int PORT = 5565;
        public const int MAX_CLIENTS = 100;

        public const int HISTORY_SIZE = 10;

        //Clients du chat (on stocke les ip et les sockets associés)
        private IPEndPoint[] connectedClients = new IPEndPoint[MAX_CLIENTS];
        int totalClients = 0;

        //Socket réseau
        UdpClient socket;
        IPEndPoint ipEndpoint;

        //Thread pour attendre les nouveaux messages
        System.Threading.Thread messageListener;
        volatile bool running = false; // volatile car la modification peut venir du thread UI ou listener

        /// <summary>
        /// Démarre le socket et le thread pour attendre les nouveaux messsages
        /// </summary>
        public void start()
        {
            //On écoute sur l'interface réseau ETML
            ipEndpoint = new IPEndPoint(Client.getEtmlIp(), Server.PORT);
            socket = new UdpClient(ipEndpoint);

            //Le thread va rester actif
            running = true;

            //Thread pour recevoir les messages
            messageListener = new System.Threading.Thread(waitForIncomingMessage);
            messageListener.Start();
        }

        /// <summary>
        /// Attente d'un nouveau message, affichage et dispatch aux clients
        /// TODO : Diviser cette méthode en plusieurs parties
        /// </summary>
        private void waitForIncomingMessage()
        {
            //Spécifités du client de qui on va recevoir un message (on ne connait ni son IP, ni son port (dynamique))
            IPEndPoint sender = new IPEndPoint(IPAddress.Any, PORT);

            //Historique
            string[] messagesHistory = new string[HISTORY_SIZE];
            int historyCounter = 0;

            //Compter les erreurs
            int socketError=0;

            //Thread reste actif tant qu'on veut discuter
            while (running)
            {
                try
                {
                    byte[] receiveBytes = socket.Receive(ref sender);
                    if (receiveBytes != null && receiveBytes.Length > 0)
                    {
                        string message = Encoding.ASCII.GetString(receiveBytes);
                        string remoteIp = sender.Address.ToString();
                        int remotePort = sender.Port;
                        string clientId = remoteIp + ":" + remotePort;

                        //Historique
                        messagesHistory[historyCounter] = message;
                        historyCounter = (historyCounter + 1) % HISTORY_SIZE;//erase old entries


                        //Debug
                        Console.WriteLine("[Server-" + DateTime.Now + "]" + clientId + " => " + message);

                        //Affichage et envoi à tous les clients déjà connectés
                        for (int i = 0; i < totalClients; i++)
                        {
                            try
                            {
                                socket.Send(receiveBytes, receiveBytes.Length, connectedClients[i]);
                                Console.WriteLine("[Server-" + DateTime.Now + "] Message dispatched to " + connectedClients[i]);
                            }
                            //Client déconnecté
                            catch (SocketException)
                            {
                                //Client deconnecté, on l'enlève de la liste et on compacte
                                //[12345-789] => [12345789-]
                                connectedClients[i] = null;
                                for (int j = i; j < totalClients - 1; j++)
                                {
                                    connectedClients[j] = connectedClients[j + 1];
                                }
                                totalClients--;
                            }
                        }

                        //Le client est-il déjà connu ou est-ce une nouvelle connexion
                        //On cherche dans le tableau pour voir si on connait IP+port (endpoint)
                        bool alreadyRegistered = false;
                        for (int i = 0; i < totalClients; i++)
                        {
                            if (connectedClients[i].Equals(sender))
                            {
                                alreadyRegistered = true;
                            }
                        }

                        //C'est un nouveau client
                        //On l'enregistre pour lui envoyer les futurs messages
                        if (!alreadyRegistered)
                        {
                            //On n'accepte plus de nouveaux clients
                            if (totalClients == MAX_CLIENTS)
                            {
                                Console.WriteLine("[Server-" + DateTime.Now + "]Max clients reached, cannot add new client " + clientId);
                            }
                            else
                            {
                                //Enregistrement du client
                                connectedClients[totalClients] = sender;
                                totalClients++;

                                //Envoyer l'historique au client
                                //TODO : Envoyer en une seule fois plutôt qu'un message réseau par message [avec de la chance .NET fait du buffering]
                                for (int i = 0; i < historyCounter; i++)
                                {
                                    byte[] historyPart = Encoding.ASCII.GetBytes(messagesHistory[i]);
                                    socket.Send(historyPart, historyPart.Length, sender);
                                }

                                Console.WriteLine("[Server-" + DateTime.Now + "]Registered new client " + clientId);
                            }
                        }
                    }
                }
                //On a arrêté la connexion
                catch (SocketException e)
                {
                    //Si on est ici alors qu'on ne voulait pas arrêter le serveur, il y eu un souci réseau
                    if (running == true)
                    {
                        

                        //On essaie de redémarrer le socket avec une limite d'essais
                        if (socketError++ < 500)
                        {
                            ipEndpoint = new IPEndPoint(Client.getEtmlIp(), Server.PORT);
                            socket.Close();
                            socket = new UdpClient(ipEndpoint);
                        }
                        //Trop d'erreurs
                        else
                        {
                            Console.WriteLine("[Server-" + DateTime.Now + "]Socket error" + e);
                            //Force l'arrêt
                            socket.Close();
                            running = false;
                        }
                    }

                }
            }


        }

        /// <summary>
        /// Arrêter le serveur (thread + socket)
        /// </summary>
        public void stop()
        {
            //On arrête tout
            running = false; //Devrait arrêter le thread listener
            messageListener.Join(1000); //On attend encore max 1 seconde que le thread se termine
            socket.Close();
        }
    }
}
