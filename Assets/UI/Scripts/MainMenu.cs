using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public Button startButton;
    public GameObject StartPage;
    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        startButton = root.Q<Button>("StartLobbyButton");

        startButton.clicked += StartButtonPressed;

    }

    void StartButtonPressed()
    {
        gameObject.SetActive(false);
        StartPage.SetActive(true);
    }
}
