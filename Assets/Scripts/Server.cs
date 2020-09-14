using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Server : MonoBehaviour
{
    // public GameObject messagePrefab;
    // public GameObject chatContainer;
    [SerializeField] private GameObject ConnectionHandlerObject;
    [SerializeField] GameObject ClientSideObject;
    [SerializeField] private GameObject plexiObject;

    private ClientSide clientSide;
    private Hard_PlexiMovement hpMovement;
    public InputField messageText;

    public Text messageData;
    public Text serverStatus;

    public int portMobile = 7000;
    public int portReceiver = 8000;
    public int portSender = 9000;

    private List<ServerClient> clientsMobile;
    private List<ServerClient> disconnectListMobile;


    private List<ServerClient> clientsSender;
    private List<ServerClient> disconnectListSender;

    private List<ServerClient> clientsReceiver;
    private List<ServerClient> disconnectListReceiver;

    private TcpListener serverMobile;
    private TcpListener serverSender;
    private TcpListener serverReceiver;

    private bool serverMobileStarted;
    private bool serverSenderStarted;
    private bool serverReceiverStarted;

    private void Awake()
    {
        hpMovement = plexiObject.GetComponent<Hard_PlexiMovement>();

        clientsSender = new List<ServerClient>();
        disconnectListSender = new List<ServerClient>();

        clientsReceiver = new List<ServerClient>();
        disconnectListReceiver = new List<ServerClient>();

        clientsMobile = new List<ServerClient>();
        disconnectListMobile = new List<ServerClient>();

        IPAddress temp = IPAddress.Any;

        try
        {
            serverSender = new TcpListener(temp, portSender);
            serverSender.Start();

            serverReceiver = new TcpListener(temp, portReceiver);
            serverReceiver.Start();

            serverMobile = new TcpListener(temp, portMobile);
            serverMobile.Start();

            StartListening();

            serverSenderStarted = true;
            serverReceiverStarted = true;
            serverMobileStarted = true;

            serverStatus.text = "Ip " + temp + " Port " + portSender;
        }
        catch (Exception e)
        {
            Debug.Log("Socket Error: " + e.Message);
            serverStatus.text = "Socket Error: " + e.Message;
        }

        clientSide = ClientSideObject.GetComponent<ClientSide>();
        clientSide.SendWithParameter("Desktop Client Initialized.");
    }

    private void Update()
    {
        if (!serverSenderStarted || !serverReceiverStarted || !serverMobileStarted)
            return;

        foreach (ServerClient c in clientsSender)
        {
            if (!IsConnected(c.tcp))
            {
                c.tcp.Close();
                disconnectListSender.Add(c);
                continue;
            }
            else
            {
                NetworkStream s = c.tcp.GetStream();
                if (s.DataAvailable)
                {
                    StreamReader reader = new StreamReader(s, true);
                    string data = reader.ReadLine();

                    if (data != null)
                    {
                        OnIncomingData(c, data);
                    }
                }
            }
        }

        foreach (ServerClient c in clientsReceiver)
        {
            if (!IsConnected(c.tcp))
            {
                c.tcp.Close();
                disconnectListReceiver.Add(c);
                continue;
            }
            else
            {
                NetworkStream s = c.tcp.GetStream();
                if (s.DataAvailable)
                {
                    StreamReader reader = new StreamReader(s, true);
                    string data = reader.ReadLine();

                    if (data != null)
                    {
                        OnIncomingData(c, data);
                    }
                }
            }
        }
        foreach (ServerClient c in clientsMobile)
        {
            if (!IsConnected(c.tcp))
            {
                c.tcp.Close();
                disconnectListMobile.Add(c);
                continue;
            }
            else
            {
                NetworkStream s = c.tcp.GetStream();
                if (s.DataAvailable)
                {
                    StreamReader reader = new StreamReader(s, true);
                    string data = reader.ReadLine();

                    if (data != null)
                    {
                        OnIncomingData(c, data);
                    }
                }
            }
        }
    }

    private void OnIncomingData(ServerClient c, string data)
    {
        string[] splitArray = data.Split(char.Parse(" "));
        if (splitArray.Count() == 8)
        {
            int positionX = int.Parse(splitArray[1]);
            int positionY = int.Parse(splitArray[2]);
            int positionDistance = int.Parse(splitArray[3]);

            int motorAngleSouth = int.Parse(splitArray[4]);
            int motorAngleNorth = int.Parse(splitArray[5]);
            int motorAngleWest = int.Parse(splitArray[6]);
            int motorAngleEast = int.Parse(splitArray[7]);

            hpMovement.MoveSimulation(positionX, positionY, positionDistance, motorAngleSouth, motorAngleNorth, motorAngleEast, motorAngleWest);
            messageData.text = "Position X = " + positionX + "\n" + "Position Y = " + positionY + "\n" + "Distance = " + positionDistance + "\n" + "Motor Angle South = " + motorAngleSouth + "\n" + "Motor Angle North = " + motorAngleNorth + "\n" + "Motor Angle West = " + motorAngleWest + "\n" + "Motor Angle East = " + motorAngleEast;
        }
        else if(splitArray.Count() == 4 && splitArray[0].Equals("M"))
        {
            BroadCast(data, clientsMobile);
            Debug.Log("Server: Sent data to mobile");
        }
        else
        {
            BroadCast(data, clientsSender);
            Debug.Log("Server: Sent data to embedded.");
        }
    }

    private bool IsConnected(TcpClient c)
    {
        try
        {
            if (c != null && c.Client != null && c.Client.Connected)
            {
                if (c.Client.Poll(0, SelectMode.SelectRead))
                {
                    return !(c.Client.Receive(new byte[1], SocketFlags.Peek) == 0);
                }

                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            return false;
        }
    }

    private void StartListening()
    {
        serverSender.BeginAcceptTcpClient(AcceptTcpClientSender, serverSender);
        serverReceiver.BeginAcceptTcpClient(AcceptTcpClientReceiver, serverReceiver);
        serverMobile.BeginAcceptTcpClient(AcceptTcpClientMobile, serverMobile);
    }

    private void AcceptTcpClientSender(IAsyncResult ar)
    {
        TcpListener listener = (TcpListener) ar.AsyncState;

        clientsSender.Add(new ServerClient(listener.EndAcceptTcpClient(ar), "Sender"));
        StartListening();
        
        BroadCast(clientsSender[clientsSender.Count - 1].clientName + " has connected", clientsSender);
    }

    private void AcceptTcpClientReceiver(IAsyncResult ar)
    {
        TcpListener listener = (TcpListener)ar.AsyncState;

        clientsReceiver.Add(new ServerClient(listener.EndAcceptTcpClient(ar), "Receiver"));
        StartListening();

        BroadCast(clientsReceiver[clientsReceiver.Count - 1].clientName + " has connected", clientsReceiver);
    }

    private void AcceptTcpClientMobile(IAsyncResult ar)
    {
        TcpListener listener = (TcpListener)ar.AsyncState;

        clientsMobile.Add(new ServerClient(listener.EndAcceptTcpClient(ar), "Mobile"));
        StartListening();

        BroadCast(clientsMobile[clientsMobile.Count - 1].clientName + " has connected", clientsMobile);
    }

    private void BroadCast(string data, List<ServerClient> cl)
    {
        foreach (ServerClient c in cl)
        {
            try
            {
                StreamWriter writer = new StreamWriter(c.tcp.GetStream());
                writer.WriteLine(data);
                writer.Flush();
            }
            catch (Exception e)
            {
                //Debug.Log("write error: " + e.Message + " to client " + c.clientName);
            }
        }
    }

    public void RestartServer()
    {
        serverSender.Stop();
        serverSenderStarted = false;

        serverReceiver.Stop();
        serverReceiverStarted = false;

        serverMobile.Stop();
        serverMobileStarted = false;

        ClientSideObject.GetComponent<ClientSide>().socketReady = false;

        clientsSender = new List<ServerClient>();
        disconnectListSender = new List<ServerClient>();

        clientsReceiver = new List<ServerClient>();
        disconnectListReceiver = new List<ServerClient>();

        clientsMobile = new List<ServerClient>();
        disconnectListMobile = new List<ServerClient>();

        try
        {
            IPAddress temp = IPAddress.Any;

            serverSender = new TcpListener(temp, portSender);
            serverSender.Start();

            serverReceiver = new TcpListener(temp, portReceiver);
            serverReceiver.Start();

            serverMobile = new TcpListener(temp, portMobile);
            serverMobile.Start();

            StartListening();

            serverSenderStarted = true;
            serverReceiverStarted = true;
            serverMobileStarted = true;

            serverStatus.text = "Ip " + temp + " Port " + portSender;
        }
        catch (Exception e)
        {
            Debug.Log("Socket Error: " + e.Message);
            serverStatus.text = "Socket Error: " + e.Message;
        }

        clientSide.SendWithParameter("Desktop Client Initialized.");
    }
}

public class ServerClient
{
    public TcpClient tcp;
    public string clientName;
    
    public ServerClient(TcpClient clientSocket, string name)
    {
        clientName = "";
        tcp = clientSocket;
    }
}
