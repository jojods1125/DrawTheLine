using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [Header("UI Settings")]
    public TMP_InputField roomInputField;
    public GameObject lobbyArea;

    public LobbyController lobbyController;

    [Header("Room Settings")]
    public RoomItem roomItemPrefab;
    List<RoomItem> roomItemsList = new List<RoomItem>();
    public Transform roomItemParent;

    [Header("Player Settings")]
    public List<PlayerItem> playerItemsList = new List<PlayerItem>();
    public PlayerItem playerItemPrefab;

    [Header("Networking Settings")]
    public int minimumPlayersRequired = 4; // 3 players and 1 screen
    public float timeBetweenUpdates = 1.5f;
    float nextUpdateTime;

    private void Start()
    {
        PhotonNetwork.JoinLobby();
    }

    private void Update()
    {

    }

    public void OnClickCreate()
    {
        if (roomInputField.text.Length >= 1)
        {
            PhotonNetwork.CreateRoom(roomInputField.text, 
                new RoomOptions() { MaxPlayers = 9, BroadcastPropsChangeToAll = true });

            GameFSM.Instance.DBG_ToHLobby();
        }
    }

    public void OnClickLeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        GameFSM.Instance.DBG_Exit();
    }

    public void OnClickPlayButton()
    {
        // Send PlayerItem List to GameManager
        PhotonNetwork.LoadLevel("Game");
        GameFSM.Instance.DBG_Start(PhotonNetwork.CurrentRoom.PlayerCount - 1);
    }

    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    void UpdateRoomList(List<RoomInfo> list)
    {
        foreach (RoomItem item in roomItemsList)
        {
            Destroy(item.gameObject);
        }
        roomItemsList.Clear();

        foreach (RoomInfo room in list)
        {
            RoomItem newRoom = Instantiate(roomItemPrefab, roomItemParent);
            newRoom.SetRoomName(room.Name);
            roomItemsList.Add(newRoom);
        }
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedRoom()
    {
        lobbyArea.SetActive(false);
    }

    public override void OnLeftRoom()
    {
        lobbyArea.SetActive(true);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (!newPlayer.IsMasterClient)
            lobbyController.PlayerAdded(newPlayer.NickName);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {

    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        if (Time.time >= nextUpdateTime)
        {
            UpdateRoomList(roomList);
            nextUpdateTime = Time.time + timeBetweenUpdates;
        }
        
    }

}
