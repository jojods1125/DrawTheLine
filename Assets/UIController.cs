using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{

    public Button startButton;
    public Button messageButton;
    public Label messageText;

    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        startButton = root.Q<Button>("StartLobbyButton");
        messageButton = root.Q<Button>("PressMe");
        messageText = root.Q<Label>("Message");

        startButton.clicked += StartButtonPressed;
        messageButton.clicked += MessageButtonPressed;

    }

    void StartButtonPressed()
    {
        SceneManager.LoadScene("Game");
    }

    void MessageButtonPressed()
    {
        messageText.text = "You failed the test. Don't try again";
        messageText.style.display = DisplayStyle.Flex;
    }
}
