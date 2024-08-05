using UnityEngine;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class UDPReceive : MonoBehaviour
{
    Thread receiveThread;
    UdpClient client;
    public int port = 5055;
    public bool startRecieving = true;
    public bool printToConsole = false;
    public string data;
    private bool trigger = false;
    private void Start()
    {
        receiveThread = new Thread(new ThreadStart(ReceiveData));
        receiveThread.IsBackground = true;
        receiveThread.Start();
    }

    private void ReceiveData()
    {
        if (client == null)
        {
            client = new UdpClient(port);
        }

        while (startRecieving)
        {
            try
            {
                if (client != null)
                {
                    IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);
                    byte[] dataByte = client.Receive(ref anyIP);
                    data = Encoding.UTF8.GetString(dataByte);

                    if (printToConsole)
                    {
                        //print(data);
                    }
                }
            }
            catch (Exception err)
            {
                print(err.ToString());
            }
        }
    }

    private void OnDisable()
    {
        client.Dispose();
        trigger = true;
        startRecieving = false;
    }

    private void OnApplicationQuit()
    {
        if (trigger == true)
        {
            client.Close();

        }
    }

}