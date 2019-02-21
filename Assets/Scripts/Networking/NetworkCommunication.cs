using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using UnityEngine;

// networkCommunication sends and receieves data with the server
namespace ShipGame.Network
{
    public class NetworkCommunication
    {
        public const int PREFIX_SIZE = 2;
        public string serverHostName = "peachocean.com";
        public int port = 7423;
        private TcpClient client;
        private NetworkStream serverStream;
        private byte[] buffer;
        private byte[] prefixBuffer = new byte[PREFIX_SIZE];
        private int bufferSize, bytesRead;
        private short messageSize;
        private string incomingData;
        private string[] dataStrings;
        public bool dataReady;
        private Queue<MessageBuffer> serverFrames;
        private System.Object dataLock = new System.Object();
        private Thread receiveThread;
        private int nextFrame, lastFrame;
        // Use this for initialization

        public NetworkCommunication()
        {
            client = new TcpClient();
            client.NoDelay = true;
            serverFrames = new Queue<MessageBuffer>();
        }

        public bool DataFrameAvailable()
        {
            lock (dataLock)
            {
                return dataReady;
            }
        }

        public int AvailableDataFrames()
        {
            lock (dataLock)
            {
                return serverFrames.Count;
            }
        }

        public MessageBuffer GetDataFrame()
        {
            lock (dataLock)
            {
                if (dataReady)
                {
                    return serverFrames.Dequeue();
                }
            }
            return null;
        }

        public void ConnectToServer(string host, int serverPort)
        {
            serverHostName = host;
            port = serverPort;
            client.Connect(serverHostName, port);
            serverStream = client.GetStream();
            receiveThread = new Thread(new ThreadStart(ReceiveData));
            receiveThread.Start();
            MonoBehaviour.print("Connected to server: " + client.Client.RemoteEndPoint);
        }

        public void ReceiveData()
        {
            while (true)
            {
                // read the first 2 bytes to determine the length of the incoming message
                bytesRead = 0;
                int n = serverStream.Read(prefixBuffer, 0, PREFIX_SIZE);
                messageSize = BitConverter.ToInt16(prefixBuffer, 0);

                //MonoBehaviour.print("Reading " + bufferSize + " bytes");
                if(messageSize > 0)
                {
                    buffer = new byte[messageSize];
                    while (bytesRead < messageSize)
                    {
                        try
                        {
                            bytesRead += serverStream.Read(buffer, bytesRead, messageSize - bytesRead);
                        }
                        catch (IOException ioe)
                        {
                            UnityEngine.Debug.Log("Disconnected");
                            UnityEngine.Debug.Log(ioe);
                        }

                    }
                    lock (dataLock)
                    {
                        serverFrames.Enqueue(new MessageBuffer(buffer, bytesRead));
                        dataReady = true;
                    }

                }
                else
                {
                    UnityEngine.Debug.Log("Read: " + n + " bytes"); 
                }

                

            }
        }


        public void Quit()
        {
            try
            {
                serverStream.Close();
                client.Close();
            }
            catch (IOException ioe)
            {
                UnityEngine.Debug.Log("Connection already closed");
                UnityEngine.Debug.Log(ioe);
            }

            UnityEngine.Debug.Log("closing connections");
        }

        public void SendData(MessageBuffer buffer)
        {
            try
            {
                serverStream.Write(BitConverter.GetBytes((short)buffer.Size), 0, 2);
                serverStream.Write(buffer.Bytes, 0, buffer.Size);
            }
            catch (IOException ioe)
            {
                UnityEngine.Debug.Log(ioe);
            }
        }
    }
}

