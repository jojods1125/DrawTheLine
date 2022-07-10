using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private IEnumerator _coroutine;

    public float TimerDuration = 3f;

    public static GameManager Instance { get; private set; }

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

    public void StartCinematic(float waitTime, string cineTitle)
    {
        _coroutine = CinematicDelay(waitTime, cineTitle);
        StartCoroutine(_coroutine);
    }

    private IEnumerator CinematicDelay(float waitTime, string cineTitle)
    {
        yield return new WaitForSeconds(waitTime);
        print("Cinematic Ended: " + cineTitle);
        GameFSM.Instance.DBG_CineEnd();
    }

    public void StartTimer(float waitTime)
    {
        GameState state = GameFSM.Instance.currentState;
        _coroutine = TimerDelay(waitTime, state);
        StartCoroutine(_coroutine);
    }

    private IEnumerator TimerDelay(float waitTime, GameState triggeringState)
    {
        yield return new WaitForSeconds(waitTime);
        if (GameFSM.Instance.currentState == triggeringState)
        {
            print("Timer Activated");
            GameFSM.Instance.DBG_TimerEnd();
        }
    }
}
