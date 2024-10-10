using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using System;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Cryptography;
//using System.Diagnostics;

public class UDPManager : MonoBehaviour
{
    public GameObject player_student;
    
    //[SerializeField]
    //public StarterAssetsInputs SAInput_out;
    public string receivedMessage;
    [SerializeField]
    private StarterAssetsInputs SAInput;

    //public GameObject obj;
    private UdpClient udpClient;
    private IPEndPoint remoteEndPoint;
    private string SAInput_json;
    private float sendInterval = 1f / 60f;
    private float timeSinceLastSend = 0f;
    void Start()
    {
        //Compose Packet
        //Convert player's SAI script to Json
        SAInput = player_student.GetComponent<StarterAssetsInputs>();
        SAInput.roomId = 1;
        SAInput.dataName = "enter";
        //나중에 spring딴에서 roomId는 제공 받는다.
        //Scene manager에 저장해뒀다가 여기에 넘기면 될듯 
        SAInput_json = JsonUtility.ToJson(SAInput);
        StartUDPClient("141.164.52.130", 3001);
        // Send enter data at very first with "name": "enter"
        SendData(SAInput_json);
        SAInput.dataName = "state";
    }

    void Update()
    {
        //Convert to Json every frame. Becuase of user's inputs are rapid in game
        timeSinceLastSend += Time.deltaTime;
        SAInput_json = JsonUtility.ToJson(SAInput);
        if(timeSinceLastSend >= sendInterval)
        {
            Debug.Log("send data: "+ SAInput_json);
            SendData(SAInput_json);
            timeSinceLastSend = 0f;
        }
        //ReceiveData();
    }

    private void StartUDPClient(string ipAddress, int port) 
    {
        udpClient = new UdpClient(3008);
        remoteEndPoint = new IPEndPoint(IPAddress.Parse(ipAddress), port);
        udpClient.BeginReceive(ReceiveData, null); 
    }
    private void ReceiveData(IAsyncResult result)
    { 
        byte[] receivedBytes = udpClient.EndReceive(result, ref remoteEndPoint); //Get a byte array
        receivedMessage = System.Text.Encoding.UTF8.GetString(receivedBytes); //this is json
        Debug.Log("received!"+ receivedMessage);
        udpClient.BeginReceive(ReceiveData, null);
    }
    private void SendData(string message)
    {
        byte[] sendBytes = System.Text.Encoding.UTF8.GetBytes(message);
        // Send the message to the server 
        udpClient.Send(sendBytes, sendBytes.Length, remoteEndPoint);
    }
}