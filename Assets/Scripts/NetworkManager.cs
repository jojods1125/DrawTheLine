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
            GameFSM.Instance.DBG_Start();
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

    #region Network Functions
    [PunRPC]
    //Host -> Clients
    public void ReceiveHostPrompt(string prompt)
    {
        // Change Client Prompt to Host's new Prompt
        // Host will wait for Client's responses
    }

    [PunRPC]
    // Host <- Clients
    public void ReceiveClientResponses(string response1, string response2)
    {
        // Host will receive the clients' responses and add them to the total responses (array or list)
    }

    [PunRPC]
    // Host -> Clients
    public void ReceiveHostResponses(string[] responses)
    {
        // Clients receive the total responses (every clients' responses) from the Host
    }

    [PunRPC]
    // Host <- Clients
    public void ReceiveClientsRankings(string[] responses)
    {
        // Host will receive the clients' rankings, store them, and average the rankings
    }

    [PunRPC]
    // Host -> Clients
    public void ReceiveHostAverageRanking((string, string)[] averageRanking)
    {
        // Clients receive the Host's average rankings and then decide
    }

    [PunRPC]
    // Host <- Clients
    public void ReceiveClientLine((string, string)[] rankingLine)
    {
        // Host will receive the Clients' Line position and display them
    }
    #endregion
}
