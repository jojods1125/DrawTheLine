using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MatchmakingController : MonoBehaviour
{
    TextField m_RoomNameInput;
    Button m_CreateRoom;
    DropdownField m_ChooseRoom;
    Button m_JoinRoom;


    public GameObject NextPage;

    private void OnEnable()
    {
        var uiDocument = GetComponent<UIDocument>();

        m_RoomNameInput = uiDocument.rootVisualElement.Q<TextField>("NameInput");
        m_CreateRoom = uiDocument.rootVisualElement.Q<Button>("StartRoom");
        m_ChooseRoom = uiDocument.rootVisualElement.Q<DropdownField>("Chooseroom");
        m_JoinRoom = uiDocument.rootVisualElement.Q<Button>("JoinRoom");

        m_CreateRoom.clicked += StartRoom;
        m_JoinRoom.clicked += JoinRoom;
    }

    void StartRoom()
    {
        //TODO: Do Start room things
        //TODO: FSM will do page changses, nextpage will be obsolete. It's just a convenience for griffin
        NextPage.SetActive(true);
        gameObject.SetActive(false);
    }

    void JoinRoom()
    {

    }

}
