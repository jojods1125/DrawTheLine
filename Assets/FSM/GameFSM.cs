using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFSM : StateMachine
{
    [HideInInspector]
    public GameState_Title Title;
    [HideInInspector]
    public GameState_Matchmaking Matchmaking;

    [HideInInspector]
    public GameState_HLobby H_Lobby;
    [HideInInspector]
    public GameState_HIntro H_Intro;
    [HideInInspector]
    public GameState_HPrompt H_Prompt;
    [HideInInspector]
    public GameState_HPostAnswers H_PostAnswers;
    [HideInInspector]
    public GameState_HSpectrum H_Spectrum;
    [HideInInspector]
    public GameState_HResults H_Results;
    [HideInInspector]
    public GameState_HCurrentScores H_CurrentScores;
    [HideInInspector]
    public GameState_HFinalScores H_FinalScores;
    [HideInInspector]
    public GameState_HPostGame H_PostGame;
    [HideInInspector]
    public GameState_HWait H_Wait;

    [HideInInspector]
    public GameState_CLobby C_Lobby;
    [HideInInspector]
    public GameState_CAnswers C_Answers;
    [HideInInspector]
    public GameState_CRanking C_Ranking;
    [HideInInspector]
    public GameState_CLine C_Line;
    [HideInInspector]
    public GameState_CPostGame C_PostGame;
    [HideInInspector]
    public GameState_CWait C_Wait;

    [HideInInspector]
    public bool HavePlayed = false;
    [HideInInspector]
    public int CurrentRound = 0;
    
    public int RoundMax = 3;
    public int NumPlayers = 4;

    public static GameFSM Instance { get; private set; }

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

        DontDestroyOnLoad(gameObject);

        Title = new GameState_Title(this);
        Matchmaking = new GameState_Matchmaking(this);

        H_Lobby = new GameState_HLobby(this);
        H_Intro = new GameState_HIntro(this);
        H_Prompt = new GameState_HPrompt(this);
        H_PostAnswers = new GameState_HPostAnswers(this);
        H_Spectrum = new GameState_HSpectrum(this);
        H_Results = new GameState_HResults(this);
        H_CurrentScores = new GameState_HCurrentScores(this);
        H_FinalScores = new GameState_HFinalScores(this);
        H_PostGame = new GameState_HPostGame(this);
        H_Wait = new GameState_HWait(this);

        C_Lobby = new GameState_CLobby(this);
        C_Answers = new GameState_CAnswers(this);
        C_Ranking = new GameState_CRanking(this);
        C_Line = new GameState_CLine(this);
        C_PostGame = new GameState_CPostGame(this);
        C_Wait = new GameState_CWait(this);
    }

    protected override GameState GetInitialState()
    {
        return Title;
    }

    public override void ChangeState(GameState newState)
    {
        DBG_CineEndVal = false;

        base.ChangeState(newState);
    }

    [HideInInspector]
    public bool DBG_ToMatchVal = false;
    public void DBG_ToMatch()
    {
        DBG_ToMatchVal = true;
    }

    [HideInInspector]
    public bool DBG_ToHLobbyVal = false;
    public void DBG_ToHLobby()
    {
        DBG_ToHLobbyVal = true;
    }

    [HideInInspector]
    public bool DBG_ToCLobbyVal = false;
    public void DBG_ToCLobby()
    {
        DBG_ToCLobbyVal = true;
    }

    [HideInInspector]
    public bool DBG_StartVal = false;
    public void DBG_Start()
    {
        DBG_StartVal = true;
    }

    [HideInInspector]
    public bool DBG_CineEndVal = false;
    public void DBG_CineEnd()
    {
        DBG_CineEndVal = true;
    }

    [HideInInspector]
    public int DBG_ClientPings = 0;
    public void DBG_IncClientPings()
    {
        DBG_ClientPings++;
    }

    [HideInInspector]
    public bool DBG_TimerEndVal = false;
    public void DBG_TimerEnd()
    {
        DBG_TimerEndVal = true;
    }

    [HideInInspector]
    public bool DBG_ReplayVal = false;
    public void DBG_Replay()
    {
        DBG_ReplayVal = true;
    }

    [HideInInspector]
    public bool DBG_ExitVal = false;
    public void DBG_Exit()
    {
        DBG_ExitVal = true;
    }

    [HideInInspector]
    public bool DBG_HostPingVal = false;
    public void DBG_HostPing()
    {
        DBG_HostPingVal = true;
    }

    [HideInInspector]
    public bool DBG_ClientSubmitVal = false;
    public void DBG_ClientSubmit()
    {
        DBG_ClientSubmitVal = true;
    }
}