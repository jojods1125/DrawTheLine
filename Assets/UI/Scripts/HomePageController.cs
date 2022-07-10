using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HomePageController : MonoBehaviour
{
    TextField m_NameInput;
    Button m_Start;

    public GameObject NextPage;

    private void OnEnable()
    {
        var uiDocument = GetComponent<UIDocument>();

        m_NameInput = uiDocument.rootVisualElement.Q<TextField>("NameInput");
        m_Start = uiDocument.rootVisualElement.Q<Button>("Submit");

        m_Start.clicked += Submit;
    }

    void Submit()
    {
        //TODO: set player name here using m_NameInput.value
        GameManager.Instance.ConnectToServer.OnClickConnect(m_NameInput.value);
    }

}
