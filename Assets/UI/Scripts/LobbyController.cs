using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class LobbyController : MonoBehaviour
{

    Dictionary<int, VisualElement> m_PlayerIcons = new Dictionary<int, VisualElement>();
    List<Label> m_PlayerNameLabels = new List<Label>();
    Label m_RoomName;
    Label m_HostName;
    Button m_StartGame;
    Button m_CloseGame;

    GameManager gm = GameManager.Instance;

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

        // m_RoomName.text = TODO: ADD HOST NAME FROM GM HERE
        // m_HostName.text = TODO: ADD HOST NAME HERE

        m_StartGame = root.Q<Button>("Submit");
        m_CloseGame = root.Q<Button>("LeaveLobby");

        m_StartGame.clicked += StartButtonPressed;
        m_CloseGame.clicked += CloseGamePressed;

    }

    public void PlayerAdded(int playerId, string playerName)
    {
        if (playerId < 8) {
            Debug.Log("Playerid: " + playerId + "playerName:" + playerName);
            m_PlayerIcons[playerId].style.unityBackgroundImageTintColor = iconColors[playerId];
            m_PlayerNameLabels[playerId].text = playerName;
        }
    }

    void StartButtonPressed()
    {
        //Todo: delete this dummy data
        PlayerAdded(0, "Joseph");
        PlayerAdded(1, "Joseph");
        PlayerAdded(2, "Joseph");
        PlayerAdded(3, "Joseph");
        PlayerAdded(4, "Joseph");
        PlayerAdded(5, "Joseph");
        PlayerAdded(6, "Joseph");
        PlayerAdded(7, "Joseph");
       
        //gameObject.SetActive(false);
    }

    void CloseGamePressed()
    {
        gameObject.SetActive(false);
    }
}
