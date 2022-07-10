using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState_HPrompt : GameState
{
    private GameFSM _gameFSM;

    public GameState_HPrompt(GameFSM stateMachine) : base("H_Prompt", stateMachine)
    {
        _gameFSM = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        _gameFSM.CurrentRound++;
        GameManager.Instance.StartCinematic(2f, "Prompt Cinematic");
    }

    public override void Exit()
    {
        base.Exit();
        _gameFSM.DBG_ClientPings = 0;
        GameManager.Instance.NetworkManager.SendPromptToClients(GameManager.Instance.GeneratePrompt());
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if (_gameFSM.DBG_CineEndVal)
        {
            _gameFSM.DBG_CineEndVal = false;
            stateMachine.ChangeState(_gameFSM.H_Wait);
        }
    }
}
