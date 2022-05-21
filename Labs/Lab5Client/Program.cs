using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Lab5
{
    class Program
    {
        //Server port to connect
        private const int PORT_NUMBER = 9999;
        //Server ipddress
        private const string IP_ADDRESS = "127.0.0.1";
        //Client
        private static TcpClient clientSocket = new TcpClient();
        //Time out for response message from server
        private const int  RESPONSE_TIME_OUT = 10000;//10 Seconds
        //Time for sending
        private const int SENDING_TIME = 60000;// 1 minutes
        //Count time to send data
        private const int COUNT_TIME = 3;
        //Enable count time to send data
        private static bool IS_COUNT_TIME = false;

        static void Main(string[] args)
        {
            ClientStart();
        }

        /// <summary>
        /// Client start
        /// </summary>
        static void ClientStart()
        {
            try
            {
                clientSocket.Connect(IP_ADDRESS, PORT_NUMBER);

                Console.WriteLine("Client connect success!!");
                //Start thread sending data
                Thread tSendData = new Thread(() => SendData(clientSocket, "Nguyen Tat Thanh"));
                tSendData.Start();
                //Start thread receive data
                Thread tReceiveData = new Thread(() => ReceiveData(clientSocket));
                tReceiveData.Start();

                tSendData.Join();
                tReceiveData.Join();

                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            Console.Read();
        }

        /// <summary>
        /// Receive data from server
        /// </summary>
        /// <param name="clientSocket">client</param>
        static void ReceiveData(TcpClient clientSocket)
        {
            if(clientSocket == null)
            {
                Console.WriteLine("Client is not init!!!");
                return;
            }

            while (clientSocket.Connected)
            {
                try
                {
                    NetworkStream serverStream = clientSocket.GetStream();
                    byte[] bytesFrom = new byte[clientSocket.ReceiveBufferSize];
                    serverStream.Read(bytesFrom, 0, (int)clientSocket.ReceiveBufferSize);
                    string dataFromServer = Encoding.ASCII.GetString(bytesFrom);
                    dataFromServer = dataFromServer.Substring(0, dataFromServer.IndexOf("\0"));
                    Console.WriteLine("DataFromServer: " + dataFromServer);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ReceiveData - Exception: " + ex.Message);

                    //Waitting for timeout
                    Thread.Sleep(RESPONSE_TIME_OUT);
                    //Enable count time to send data 
                    IS_COUNT_TIME = true;
                }
            }

            Console.WriteLine("Client closed: ReceiveData Stop!!!!");
        }

        /// <summary>
        /// Send data to server
        /// </summary>
        /// <param name="clientSocket">Client</param>
        /// <param name="message">Message</param>
        static void SendData(TcpClient clientSocket, string message)
        {
            if (clientSocket == null)
            {
                Console.WriteLine("Client is not init!!!");
                return;
            }

            int sendingCount = 0;

            while (clientSocket.Connected)
            {
                try
                {                   
                    //Send data to server
                    NetworkStream serverStream = clientSocket.GetStream();
                    byte[] outStream = Encoding.ASCII.GetBytes(message + "$");
                    serverStream.Write(outStream, 0, outStream.Length);
                    serverStream.Flush();
                    Console.WriteLine("Send data to server!!!");

                    //sending count
                    if (IS_COUNT_TIME)
                    {
                        sendingCount++;
                        Console.WriteLine("Try send counts: " + sendingCount);
                    }

                    //Try to send by countime
                    if (sendingCount >= COUNT_TIME)
                    {
                        break;
                    }

                    //Waitting for sending new message
                    Thread.Sleep(SENDING_TIME);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("SendData - Exception: " + ex.Message);
                }
            }

            //Close socket 
            if (clientSocket.Connected)
                clientSocket.Close();

            Console.WriteLine("Client closed: Send data Stop!!!!");
        }
    }
}
