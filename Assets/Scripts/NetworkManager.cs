using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class NetworkManager : MonoBehaviour
{
    [Header("Client")]
    public TMP_Text hostMessage;
    public TMP_InputField clientMessage;

    [Header("Host")]
    public TMP_Text clientMessages;
    public TMP_Text messageCounter;
    bool hasReceivedMessage = false;
    int messagesReceived = 0;

    public GameObject clientScreen;
    public GameObject hostScreen;

    PhotonView view;

    private void Start()
    {
        view = GetComponent<PhotonView>();
        if (PhotonNetwork.IsMasterClient)
        {
            clientScreen.SetActive(false);
            hostScreen.SetActive(true);
        }
        else
        {
           
            clientScreen.SetActive(true);
            hostScreen.SetActive(false);
        }
    }

    public void OnClickSend()
    {
        if (clientMessage.text.Length >= 1)
        {
            view.RPC("ReceiveMessage", RpcTarget.MasterClient, clientMessage.text);
            clientMessage.text = "Enter new text...";
        }
        
    }

    [PunRPC]
    public void SendMessage(int message)
    {
        // Change Log Text
        hostMessage.text = "Messages: " + message;
    }

    [PunRPC]
    public void ReceiveMessage(string message)
    {
        if (!hasReceivedMessage)
        {
            hasReceivedMessage = true;
            clientMessages.text = "";
        }
        // Change Log Text
        clientMessages.text += "\n" + message;
        messagesReceived++;
        messageCounter.text = "Messages: " + messagesReceived;
        view.RPC("SendMessage", RpcTarget.Others, messageCounter);
    }
}
