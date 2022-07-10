using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ClientRanking : MonoBehaviour
{
    [SerializeField]
    VisualTreeAsset m_ListEntryTemplate;
    public GameObject NextPage;

    Label m_Prompt;
    Button m_Submit;

    private void OnEnable()
    {
        var uiDocument = GetComponent<UIDocument>();

        var ResponseListController = new ResponseListController();
        ResponseListController.InitializeResponseList(uiDocument.rootVisualElement, m_ListEntryTemplate);

        m_Prompt = uiDocument.rootVisualElement.Q<Label>("PromptText");
        m_Submit = uiDocument.rootVisualElement.Q<Button>("SubmitButton");

        m_Submit.clicked += SubmitRankings;
    }


    public void SetPrompt(string prompt)
    {
        m_Prompt.text = prompt;
    }

    void SubmitRankings()
    {
        //Do submit things here
        NextPage.SetActive(true);
        gameObject.SetActive(false);
    }
}
