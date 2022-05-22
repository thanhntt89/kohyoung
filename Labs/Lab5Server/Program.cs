using System;
using System.Net.Sockets;
using System.Text;

namespace Lab5Server
{
    class Program
    {
        [Obsolete]
        static void Main(string[] args)
        {
            Server.Start();
        }

        /// <summary>
        /// Server settings
        /// </summary>
        public class Settings
        {
            //SERVER PORT
            public const int PORT_NUMBER = 9999;
        }

        /// <summary>
        /// Server
        /// </summary>
        [Obsolete]
        public class Server
        {
            public static void Start()
            {
                //Create server socket
                TcpListener serverSocket = new TcpListener(Settings.PORT_NUMBER);

                //Create client socket
                TcpClient clientSocket = default(TcpClient);

                //Start server
                serverSocket.Start();
                Console.WriteLine(" >> Server Started");

                //Waiting for client connecting
                clientSocket = serverSocket.AcceptTcpClient();

                Console.WriteLine(" >> Accept connection from client");

                //Response to clients
                Response(clientSocket);
            }

            /// <summary>
            /// Response to client
            /// </summary>
            /// <param name="clientSocket">Client</param>
            private static void Response(TcpClient clientSocket)
            {              
                string serverResponse = string.Empty;

                while (true)
                {
                    try
                    {
                        //Reset data 
                        serverResponse = string.Empty;
                        
                        ReceiveData(clientSocket, ref serverResponse);

                        SendData(clientSocket, serverResponse);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Response - Error:" + ex.Message);
                    }
                }
            }

            /// <summary>
            /// Receive data from client
            /// </summary>
            /// <param name="clientSocket">client</param>
            /// <param name="serverResponse">response message</param>
            static void ReceiveData(TcpClient clientSocket, ref string serverResponse)
            {
                try
                {
                    //Received data from client
                    NetworkStream networkStream = clientSocket.GetStream();
                    byte[] bytesFrom = new byte[clientSocket.ReceiveBufferSize];
                    networkStream.Read(bytesFrom, 0, (int)clientSocket.ReceiveBufferSize);
                    string dataFromClient = Encoding.ASCII.GetString(bytesFrom);
                    dataFromClient = dataFromClient.Substring(0, dataFromClient.IndexOf("$"));
                    Console.WriteLine(" >> Data from client - " + dataFromClient);
                    serverResponse = "Last Message from client " + dataFromClient;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ReceiveData - Error:" + ex.Message);
                }
            }

            /// <summary>
            /// Send data to client
            /// </summary>
            /// <param name="clientSocket">Client</param>
            /// <param name="message">Message</param>
            private static void SendData(TcpClient clientSocket, string message)
            {
                try
                {
                    //Received data from client
                    NetworkStream networkStream = clientSocket.GetStream();
                    //Send data to client
                    Byte[] sendBytes = Encoding.ASCII.GetBytes(message);
                    networkStream.Write(sendBytes, 0, sendBytes.Length);
                    networkStream.Flush();

                    Console.WriteLine(" >> Server Send: " + message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("SendData - Error:" + ex.Message);
                }
            }
        }
    }
}
