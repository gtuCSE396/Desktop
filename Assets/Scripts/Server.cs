using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;

public class Server : MonoBehaviour
{
    // public GameObject messagePrefab;
    // public GameObject chatContainer;
    public Text messageData;
    public Text serverStatus;
    
    public int port = 9000;
    private List<ServerClient> clients;
    private List<ServerClient> disconnectList;

    private TcpListener server;
    private bool serverStarted;

    private void Start()
    {
        clients = new List<ServerClient>();
        disconnectList = new List<ServerClient>();

        try
        {
            server = new TcpListener(IPAddress.Any, port);
            server.Start();

            StartListening();
            serverStarted = true;
            Debug.Log("Server is started on port " + port);
            serverStatus.text = "Server is started on port " + port;
        }
        catch (Exception e)
        {
            Debug.Log("Socket Error: " + e.Message);
            serverStatus.text = "Socket Error: " + e.Message;
        }
    }

    private void Update()
    {
        if (!serverStarted)
            return;

        foreach (ServerClient c in clients)
        {
            if (!IsConnected(c.tcp))
            {
                c.tcp.Close();
                disconnectList.Add(c);
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
        // messagePrefab.GetComponentInChildren<Text>().text = data;
        // GameObject go = Instantiate(messagePrefab, chatContainer.transform) as GameObject;
        //
        // go.GetComponentInChildren<Text>().text = data;
        Debug.Log(c.clientName + data);
        messageData.text = data;
        BroadCast(data, clients);
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
        server.BeginAcceptTcpClient(AcceptTcpClient, server);
    }

    private void AcceptTcpClient(IAsyncResult ar)
    {
        TcpListener listener = (TcpListener) ar.AsyncState;
        
        clients.Add(new ServerClient(listener.EndAcceptTcpClient(ar), "Mobile"));
        StartListening();
        
        BroadCast(clients[clients.Count - 1].clientName + " has connected", clients);
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
                Debug.Log("write error: " + e.Message + " to client " + c.clientName);
            }
        }
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
