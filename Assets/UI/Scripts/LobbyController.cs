using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class LobbyController : MonoBehaviourPunCallbacks
{

    Dictionary<int, VisualElement> m_PlayerIcons = new Dictionary<int, VisualElement>();
    List<Label> m_PlayerNameLabels = new List<Label>();
    Label m_RoomName;
    Label m_HostName;
    Button m_StartGame;
    Button m_CloseGame;

    GameManager gm = GameManager.Instance;

    int playerIndex = 0;

    Color[] iconColors = {
        new Color32(206, 103, 103, 255),
        new Color32(214, 147, 109, 255),
        new Color32(244, 202, 108, 255),
        new Color32(122, 180, 121, 255),
        new Color32(116, 167, 183, 255),
        new Color32(150, 132, 188, 255),
        new Color32(198, 145, 183, 255),
        new Color32(128, 100, 84, 255)
    };

    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        for (int i = 0; i < 8; i++)
        {
            Debug.Log("Adding the player icons to thing");
            m_PlayerIcons.Add(i,root.Q<VisualElement>("Player"+i+"Icon"));
            m_PlayerNameLabels.Add(root.Q<Label>("Player" + i));
        }

        m_RoomName = root.Q<Label>("RoomName");
        m_HostName = root.Q<Label>("HostName");

        //m_RoomName.text = PhotonNetwork.CurrentRoom.Name;
        //m_HostName.text = PhotonNetwork.MasterClient.NickName;

        m_StartGame = root.Q<Button>("Submit");
        m_CloseGame = root.Q<Button>("LeaveLobby");

        m_StartGame.clicked += StartButtonPressed;
        m_CloseGame.clicked += CloseGamePressed;

    }

    public void PlayerAdded(string playerName)
    {
        if (playerIndex < 8) {
            Debug.Log("Playerid: " + playerIndex + "playerName:" + playerName);
            m_PlayerIcons[playerIndex].style.unityBackgroundImageTintColor = iconColors[playerIndex];
            m_PlayerNameLabels[playerIndex].text = playerName;
            playerIndex++;
        }
    }

    void StartButtonPressed()
    {
        Debug.Log("PRESSING BUTTON");

        PhotonNetwork.LoadLevel("Game");
        GameFSM.Instance.DBG_Start(PhotonNetwork.CurrentRoom.PlayerCount - 1);
        //gameObject.SetActive(false);
    }

    void CloseGamePressed()
    {
        gameObject.SetActive(false);
    }
}
