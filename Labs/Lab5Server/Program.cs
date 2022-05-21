using System;
using System.Net.Sockets;
using System.Text;

namespace Lab5Server
{
    class Program
    {
        //SERVER PORT
        private const int PORT_NUMBER = 9999;

        [Obsolete]
        static void Main(string[] args)
        {
            //Create server socket
            TcpListener serverSocket = new TcpListener(PORT_NUMBER);
            int requestCount = 0;
            //Create client socket
            TcpClient clientSocket = default(TcpClient);
            
            //Start server
            serverSocket.Start();
            Console.WriteLine(" >> Server Started");

            //Waiting for client connecting
            clientSocket = serverSocket.AcceptTcpClient();
            Console.WriteLine(" >> Accept connection from client");
            requestCount = 0;

            while (true)
            {
                try
                {
                    requestCount = requestCount + 1;

                    //Received data from client
                    NetworkStream networkStream = clientSocket.GetStream();
                    byte[] bytesFrom = new byte[clientSocket.ReceiveBufferSize];
                    networkStream.Read(bytesFrom, 0, (int)clientSocket.ReceiveBufferSize);
                    string dataFromClient = Encoding.ASCII.GetString(bytesFrom);
                    dataFromClient = dataFromClient.Substring(0, dataFromClient.IndexOf("$"));
                    Console.WriteLine(" >> Data from client - " + dataFromClient);
                    string serverResponse = "Last Message from client: " + dataFromClient;
                   
                    //Send data to client
                    Byte[] sendBytes = Encoding.ASCII.GetBytes(serverResponse);
                    networkStream.Write(sendBytes, 0, sendBytes.Length);
                    networkStream.Flush();

                    Console.WriteLine(" >> " + serverResponse);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());                   
                }
            }
        }
    }
}
