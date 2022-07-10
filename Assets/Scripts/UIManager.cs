using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }


    public GameObject ClientWait;
    public GameObject ClientAnswers;
    public GameObject ClientRanking;
    public GameObject ClientLine;

    public GameObject HostLobby;
    public GameObject HostPrompt;
    public GameObject HostSpectrum;
    public GameObject HostResults;



    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void SetClientWait(bool active)
    {
        ClientWait.SetActive(active);
    }

    public void SetClientAnswers(bool active)
    {
        ClientAnswers.SetActive(active);
    }

    public void SetClientRanking(bool active)
    {
        ClientRanking.SetActive(active);
    }

    public void SetClientLine(bool active)
    {
        ClientLine.SetActive(active);
    }

    public void SetLobby(bool active)
    {
        HostLobby.SetActive(active);
    }

    public void SetHostPrompt(bool active)
    {
        HostPrompt.SetActive(active);
    }

    public void SetHostSpectrum(bool active)
    {
        HostSpectrum.SetActive(active);
    }

    public void SetHostResults(bool active)
    {
        HostResults.SetActive(active);
    }
}
