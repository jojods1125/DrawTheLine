using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ClientRanking : MonoBehaviour
{
    [SerializeField]
    VisualTreeAsset m_ListEntryTemplate;

    Label m_StageLabel;
    Label m_Prompt;

    private void OnEnable()
    {
        var uiDocument = GetComponent<UIDocument>();

        var ResponseListController = new ResponseListController();
        ResponseListController.InitializeResponseList(uiDocument.rootVisualElement, m_ListEntryTemplate);

        //Stage label says "Section A/B/C" to show what round we're on
        //TODO: tie this value to the state machine
        m_StageLabel = uiDocument.rootVisualElement.Q<Label>("StageLabel");
        m_Prompt = uiDocument.rootVisualElement.Q<Label>("PromptText");
    }


    public void SetPrompt(string prompt)
    {
        m_Prompt.text = prompt;
    }

    public void SetStage(int stage)
    {
        switch (stage)
        {
            case 1:
                m_StageLabel.text = "Section A";
                break;
            case 2:
                m_StageLabel.text = "Section B";
                break;
            case 3:
                m_StageLabel.text = "Section C";
                break;
        }
    }

}
