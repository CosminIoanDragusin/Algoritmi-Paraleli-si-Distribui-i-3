using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;
using System.Collections;
using System.ComponentModel;
using System.Threading;

namespace Server
{
    class Program
    {
        public static Hashtable clientsList = new Hashtable();

        static void Main(string[] args)
        {
            TcpListener server = new TcpListener(5000);
            TcpClient clientSocket = default(TcpClient);

            String textReceived = null;

            ArrayList users = new ArrayList();

            server.Start();
            Console.WriteLine("Chat Server Started....");

            while (true)
            {
                clientSocket = server.AcceptTcpClient();
                NetworkStream stream = clientSocket.GetStream();
                StreamReader streamReader = new StreamReader(stream);
                StreamWriter streamWriter = new StreamWriter(stream);

                textReceived = streamReader.ReadLine();
                if (textReceived.Substring(textReceived.Length - 1) == "#")
                {
                    String nume = textReceived.Substring(0, textReceived.Length - 1);
                    bool gasit = false;
                    if (users.Contains(nume))
                    {
                        gasit = true;
                        streamWriter.WriteLine("Already connected");
                        streamWriter.Flush();
                    }
                    if (!gasit)
                    {
                        Console.WriteLine(nume + " connected");

                        clientsList.Add(nume, clientSocket);
                        String detrimis = null;
                        foreach (DictionaryEntry Item in clientsList)
                        {
                            detrimis += Item.Key + ".";
                        }
                        Console.WriteLine(detrimis);
                        foreach (DictionaryEntry Item in clientsList)
                        {
                            TcpClient clientSocketul;
                            clientSocketul = (TcpClient)Item.Value;
                            NetworkStream networkStream = clientSocketul.GetStream();
                            StreamWriter networkWriter = new StreamWriter(networkStream);
                            networkWriter.WriteLine("Joined" + " " + detrimis);
                            networkWriter.Flush();
                        }
                    }

                    Console.WriteLine(nume + " " + "joined");

                    handleClinet client = new handleClinet();
                    client.startClient(clientSocket, nume, clientsList);
                }
            }
            clientSocket.Close();
            server.Stop();
            Console.WriteLine("Exit");
            Console.ReadLine();
        }

        public static void broadcast(string msg, string uName, bool flag)
        {
            foreach (DictionaryEntry Item in clientsList)
            {
                TcpClient broadcastSocket;
                broadcastSocket = (TcpClient)Item.Value;
                NetworkStream broadcastStream = broadcastSocket.GetStream();
                StreamWriter broadcastWriter = new StreamWriter(broadcastStream);
                String broadcast = null;

                if (flag == true)
                {
                    broadcast = uName + " : " + msg;
                }
                else
                {
                    broadcast = msg;
                }

                broadcastWriter.WriteLine("Public" + broadcast);
                broadcastWriter.Flush();
            }
        }  //end broadcast function
    }

    public class handleClinet
    {
        TcpClient clientSocket;
        string clName;
        Hashtable clientsList;

        public void startClient(TcpClient inClientSocket, string clineNo, Hashtable cList)
        {
            this.clientSocket = inClientSocket;
            this.clName = clineNo;
            this.clientsList = cList;
            Thread ctThread = new Thread(doChat);
            ctThread.Start();
        }

        private void doChat()
        {
            string dataFromClient = null;
            Byte[] sendBytes = null;
            string serverResponse = null;
            string rCount = null;

            while (true)
            {
                try
                {
                    NetworkStream stream = clientSocket.GetStream();

                    StreamReader streamReader = new StreamReader(stream);
                    dataFromClient = streamReader.ReadLine();

                    Console.WriteLine("From client " + clName + " : " + dataFromClient);

                    Program.broadcast(dataFromClient, clName, true);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }//end while
        }//end doChat
    }
}
