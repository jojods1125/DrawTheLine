using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PromptRevealController : MonoBehaviour
{
    Label m_PromptNumber;
    Label m_Prompt;

    GameManager gm;

    private void OnEnable()
    {
        var uiDocument = GetComponent<UIDocument>();

        gm = GameManager.Instance;

        m_PromptNumber = uiDocument.rootVisualElement.Q<Label>("PromptNumber");
        m_Prompt = uiDocument.rootVisualElement.Q<Label>("Prompt");

        m_Prompt.visible = false;
        ChangePrompt(GameManager.Instance.CurrentPrompt, GameFSM.Instance.CurrentRound);
    }

    public void ChangePrompt(string prompt, int roundNumber)
    {
        m_PromptNumber.text = "Prompt" + roundNumber;
        m_Prompt.text = prompt;
    }
    public void RevealPrompt()
    {
        m_Prompt.visible = true;
    }
}
