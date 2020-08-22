using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;
using Color = UnityEngine.UI.ColorBlock;

public class ClientSide : MonoBehaviour
{
    public InputField messageText;

    private bool socketReady;
    private TcpClient socket;
    private NetworkStream stream;
    private StreamWriter writer;
    private StreamReader reader;

    public void ConnectToServer()
    {
        // whether checks it is connected
        if (socketReady)
            return;
        
        // default values
        string host = "127.0.0.1";
        int port = 9000;

        // string h;
        // int p;
        //
        // h = GameObject.Find("HostInfo").GetComponent<InputField>().text;
        // if (h != "")
        //     host = h;
        // int.TryParse(GameObject.Find("PortInfo").GetComponent<InputField>().text, out p);
        // if (p != 0)
        //     port = p;
        
        // create the socket
        try
        {
            socket = new TcpClient(host, port);
            stream = socket.GetStream();
            writer = new StreamWriter(stream);
            reader = new StreamReader(stream);
            socketReady = true;
            Debug.Log("Client Connected");
        }
        catch (Exception e)
        {
            Debug.Log("Socket error: " + e.Message);
        }
    }

    private void Update()
    {
        if (socketReady)
        {
            if (stream.DataAvailable)
            {
                string data = reader.ReadLine();
                if (data != null)
                {
                    OnIncomingData(data);
                }
            }
        }
    }

    private void OnIncomingData(string data)
    {
        Debug.Log("Incoming Data: " + data);
    }

    public void Send()
    {
        ConnectToServer();
        if (!socketReady)
            return;
        writer.WriteLine("desktop:" + messageText.text);
        writer.Flush();
    }
}
