using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;
using Color = UnityEngine.UI.ColorBlock;
using System.Linq;

public class ClientSide : MonoBehaviour
{
    [SerializeField] private GameObject ConnectionHandlerObject;
    [SerializeField] private GameObject plexiObject;

    private Hard_PlexiMovement hpMovement;
    public InputField messageText;

    public string hostIp = "127.0.0.1";
    public int portNo = 9000;
    
    public bool socketReady;
    private TcpClient socket;
    private NetworkStream stream;
    private StreamWriter writer;
    private StreamReader reader;

    private void Start()
    {
        hpMovement = plexiObject.GetComponent<Hard_PlexiMovement>();
    }


    public void ConnectToServer()
    {
        // whether checks it is connected
        if (socketReady)
            return;
        
        // default values
        string host = hostIp;
        int port = portNo;

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
    }

    public void SendWithParameter(string data)
    {
        ConnectToServer();
        if (!socketReady)
            return;
        writer.WriteLine(data);
        writer.Flush();
        Debug.Log("Client: Send Complete : " + data);
    }
}
