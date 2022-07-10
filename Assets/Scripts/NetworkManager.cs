using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

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
    public SortedDictionary<int, List<string>> aggregateRankings = new();
    // Average Ranking
    public List<string> averageRanking; // Calculated externally
    public List<string> averageRankingSplits; // Calculated externally
    // Aggregate Line Positons
    public SortedDictionary<int, string> aggregateLinePos = new(); // Aggregated by clients and handled by Host

    [Header("Client")]
    // Input Fields for Responses
    public TMP_InputField firstResponse; // Received from UI
    public TMP_InputField secondResponse; // Received from UI
    // Ranking for Responses
    public List<string> rankingResponse; // Received from UI
    // Line Position for Ranking
    public (string, string) lineResponse; // Received from UI

    [Header("UI")]
    // Selects UI Panel based on state and whether host or client
    public GameObject clientScreen;
    public GameObject hostScreen;
    public TMP_Text hostResponses;
    bool hasReceivedFirstResponses = false;

    PhotonView view;
    Player player;
    public int playerID;


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
            player = PhotonNetwork.LocalPlayer;
            playerID = player.ActorNumber;
            GameManager.Instance.PlayerNames.Add(player.ActorNumber, player.NickName);
            GameFSM.Instance.DBG_Start(8);
            clientScreen.SetActive(true);
            hostScreen.SetActive(false);
        }
        // Reset Variables - ResetVariables();
    }

    private void ResetVariables()
    {
        // Potentially Handled by GameManager
    }

    // Client return responses to Host
    public void OnClickSubmitResponses()
    {
        if (firstResponse.text.Length >= 1 && secondResponse.text.Length >= 1)
        {
            view.RPC("ReceiveClientResponses", RpcTarget.MasterClient, firstResponse.text, secondResponse.text, playerID);
            GameFSM.Instance.DBG_ClientSubmit();
        }
    }

    public void OnClickSubmitRankings()
    {
        // Dummy Info
        string[] rankings = { "test", "test2", "test3", "test4" };
        view.RPC("ReceiveClientsRankings", RpcTarget.MasterClient, rankings, playerID);
        GameFSM.Instance.DBG_ClientSubmit();
    }

    public void OnClickSubmitLine()
    {
        // Dummy Info
        view.RPC("ReceiveClientLine", RpcTarget.MasterClient, "test", playerID);
        GameFSM.Instance.DBG_ClientSubmit();
    }

    // Host sending info to clients
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
        GameManager.Instance.CreateResponseData(response1, playerID);
        GameManager.Instance.CreateResponseData(response2, playerID);
        // Pings Host and GameManager
        GameFSM.Instance.DBG_IncClientPings();

        if (!hasReceivedFirstResponses)
        {
            hasReceivedFirstResponses = true;
            hostResponses.text = "";
        }
        hostResponses.text += response1 + " " + response2 + "\n";
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
        GameFSM.Instance.DBG_IncClientPings();
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
        GameFSM.Instance.DBG_IncClientPings();
    }

    [PunRPC]
    // Host -> Clients
    public void ReceiveHostFinalScores()
    {
        GameFSM.Instance.DBG_HostPing();
    }
    #endregion
}
