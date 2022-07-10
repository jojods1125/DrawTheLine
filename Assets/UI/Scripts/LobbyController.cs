using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class LobbyController : MonoBehaviour
{

    List<VisualElement> m_PlayerIcons = new List<VisualElement>();
    List<Label> m_PlayerNameLabels = new List<Label>();
    Label m_RoomName;
    Label m_HostName;
    Button m_StartGame;
    Button m_CloseGame;

    GameManager gm = GameManager.Instance;

    //enum iconColors{
    //    new Color(1.0f, 1.0f, 1.0f),
    //    xFFFFFFF1,
    //    xFFFFFFF2,
    //    xFFFFFFF3,
    //    xFFFFFFF4,
    //    xFFFFFFF5,
    //    xFFFFFFF6,
    //    xFFFFFFF7,
    //}
    Color[] iconColors = {
        new Color32(255, 255, 255, 255),
        new Color32(255, 255, 255, 255),
        new Color32(255, 255, 255, 255),
        new Color32(255, 255, 255, 255),
        new Color32(255, 255, 255, 255),
        new Color32(255, 255, 255, 255),
        new Color32(255, 255, 255, 255),
        new Color32(255, 255, 255, 255),
    };


    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        for (int i = 0; i < 8; i++)
        {
            m_PlayerIcons.Add(root.Q<Label>("Player" + i + "Icon"));
            m_PlayerNameLabels.Add(root.Q<Label>("Player" + i));
        }

        m_RoomName = root.Q<Label>("RoomName");
        m_HostName = root.Q<Label>("HostName");

        m_StartGame = root.Q<Button>("Submit");
        m_CloseGame = root.Q<Button>("LeaveLobby");

        m_StartGame.clicked += StartButtonPressed;
        m_CloseGame.clicked += CloseGamePressed;

    }

    public void PlayerAdded(int playerId, string playerName)
    {
        if (playerId < 8)
        {
            m_PlayerIcons[playerId].style.backgroundColor = iconColors[playerId];
            m_PlayerNameLabels[playerId].name = playerName;
        }
    }

    void StartButtonPressed()
    {
        gameObject.SetActive(false);
    }

    void CloseGamePressed()
    {
        gameObject.SetActive(false);
    }
}
