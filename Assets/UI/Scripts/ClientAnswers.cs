using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ClientAnswers : MonoBehaviour
{
    [SerializeField]
    public GameObject NextPage;

    Label m_Prompt;
    Button m_Submit;
    TextField m_Answer1;
    TextField m_Answer2;

    string m_Response1;
    string m_Response2;

    private void OnEnable()
    {
        var uiDocument = GetComponent<UIDocument>();

        m_Prompt = uiDocument.rootVisualElement.Q<Label>("PromptText");
        m_Submit = uiDocument.rootVisualElement.Q<Button>("SubmitButton");
        m_Answer1 = uiDocument.rootVisualElement.Q<TextField>("FirstResponse");
        m_Answer2 = uiDocument.rootVisualElement.Q<TextField>("SecondResponse");

        m_Submit.clicked += SubmitRankings;
    }


    public void SetPrompt(string prompt)
    {
        m_Prompt.text = prompt;
    }

    void SubmitRankings()
    {
        m_Response1 = m_Answer1.value;
        m_Response2 = m_Answer2.value;
        Debug.Log("Responses: " + m_Response1 + ", " + m_Response2);

        //Do submit things here
        GameManager.Instance.NetworkManager.OnClickSubmitResponses(m_Response1, m_Response2);
        gameObject.SetActive(false);
    }
}
