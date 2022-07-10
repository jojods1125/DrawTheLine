using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    // Ref to GameManager???

    [Header("Game & Host")]
    public int numberOfPlayers = 2; // Taken from GameManager
    public SortedDictionary<int, int> scores; // Handled by Host - <playerID, score>
    public List<PlayerItem> playerItemsList = new List<PlayerItem>(); // Taken from GameManager
    // Current Prompt
    public string currentPrompt; // GameManager will randomly select a prompt (Not repeats)
    // Total Responses
    public SortedDictionary<int, string> totalResponses; // Aggregated by clients and handled by Host
    // Anonymous Responses
    public List<string> anonymousResponses;
    // Aggregate Rankings
    public SortedDictionary<int, List<string>> aggregateRankings;
    // Average Ranking
    public List<string> averageRanking; // Calculated externally
    public List<string> averageRankingSplits; // Calculated externally
    // Aggregate Line Positons
    public SortedDictionary<int, string> aggregateLinePos; // Aggregated by clients and handled by Host

    [Header("Client")]
    // Input Fields for Responses
    public TMP_InputField firstResponse; // Received from UI
    public TMP_InputField secondResponse; // Received from UI
    // Ranking for Responses
    public List<string> rankingResponse; // Received from UI
    // Line Position for Ranking
    public (string, string) lineResponse; // Received from UI

    [Header("Temp")]
    // Client
    public TMP_Text hostMessage; // Temp
    public TMP_InputField clientMessage; // Temp
    // Host
    public TMP_Text clientMessages; // Temp
    public TMP_Text messageCounter; // Temp
    bool hasReceivedMessage = false; // Temp
    int messagesReceived = 0; // Temp

    [Header("UI")]
    // Selects UI Panel based on state and whether host or client
    public GameObject clientScreen;
    public GameObject hostScreen;

    PhotonView view;



    private void Start()
    {
        numberOfPlayers = PhotonNetwork.CurrentRoom.PlayerCount;
        GameManager.Instance.NetworkManager = this;
        view = GetComponent<PhotonView>();
        if (PhotonNetwork.IsMasterClient)
        {
            clientScreen.SetActive(false);
            hostScreen.SetActive(true);
            // Set numberOfPlayers = GameManager.Instance ...
        }
        else
        {
            // Add Players to List
            GameFSM.Instance.DBG_Start();
            clientScreen.SetActive(true);
            hostScreen.SetActive(false);
        }
        // Reset Variables - ResetVariables();
    }

    private void ResetVariables()
    {
        // Potentially Handled by GameManager
    }

    // Temp
    public void OnClickSend()
    {
        if (clientMessage.text.Length >= 1)
        {
            view.RPC("ReceiveMessage", RpcTarget.MasterClient, clientMessage.text);
            clientMessage.text = "Enter new text...";
        }
        
    }

    // Temp

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

    public void SendPromptToClients(string prompt)
    {
        view.RPC("ReceiveHostPrompt", RpcTarget.Others, prompt);
    }

    public void SendResponsesToClients(string[] responses)
    {
        view.RPC("ReceiveHostResponses", RpcTarget.Others, responses);
    }

    public void SendAverageRankingToClients(string[] averageRanking)
    {
        view.RPC("ReceiveHostAverageRanking", RpcTarget.Others, averageRanking);
    }

    public void SendFinalScoresToClients()
    {
        view.RPC("ReceiveHostFinalScores", RpcTarget.Others);
    }

    #region Network Functions
    [PunRPC]
    //Host -> Clients
    public void ReceiveHostPrompt(string prompt)
    {
        // Change Client Prompt to Host's new Prompt
        currentPrompt = prompt;
        // Update Client UI
        // Host will wait for Client's responses
        GameFSM.Instance.DBG_HostPing();
    }

    [PunRPC]
    // Host <- Clients
    public void ReceiveClientResponses(string response1, string response2, int playerID)
    {
        // Host will receive the clients' responses and add them to the total responses (array or list)
        totalResponses.Add(playerID, response1);
        totalResponses.Add(playerID, response2);
    }

    [PunRPC]
    // Host -> Clients
    public void ReceiveHostResponses(string[] responses)
    {
        // Assume totalResponses from GameManager has converted to array
        // Clients receive the total responses (every clients' responses) from the Host
        anonymousResponses = new List<string>(responses);
        // Update Client UI
        GameFSM.Instance.DBG_HostPing();
    }

    [PunRPC]
    // Host <- Clients
    public void ReceiveClientsRankings(string[] rankingResponse, int playerID)
    {
        // Host will receive the clients' rankings, store them, and average the rankings
        List<string> playerResponse = new List<string>(rankingResponse);
        aggregateRankings.Add(playerID, playerResponse);
    }

    [PunRPC]
    // Host -> Clients
    public void ReceiveHostAverageRanking(string[] averageRanking)
    {
        // Clients receive the Host's average rankings and then decide
        averageRankingSplits = new List<string>(averageRanking);
        // Update Client UI
        GameFSM.Instance.DBG_HostPing();
    }

    [PunRPC]
    // Host <- Clients
    public void ReceiveClientLine(string rankingLine, int playerID)
    {
        // Host will receive the Clients' Line position and display them
        aggregateLinePos.Add(playerID, rankingLine);
    }

    [PunRPC]
    // Host -> Clients
    public void ReceiveHostFinalScores()
    {
        GameFSM.Instance.DBG_HostPing();
    }
    #endregion
}
