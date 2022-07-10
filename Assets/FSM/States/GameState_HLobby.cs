using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState_HLobby : GameState
{
    private GameFSM _gameFSM;

    public GameState_HLobby(GameFSM stateMachine) : base("H_Lobby", stateMachine)
    {
        _gameFSM = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        UIManager.Instance.SetLobby(true);
    }

    public override void Exit()
    {
        base.Exit();
        UIManager.Instance.SetLobby(false);
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (_gameFSM.DBG_StartVal)
        {
            _gameFSM.DBG_StartVal = false;
            if (!_gameFSM.HavePlayed)
            {
                stateMachine.ChangeState(_gameFSM.H_Intro);
            }
            else
            {
                stateMachine.ChangeState(_gameFSM.H_Prompt);
            }
        }
        else if (_gameFSM.DBG_ExitVal)
        {
            _gameFSM.DBG_ExitVal = false;
            stateMachine.ChangeState(_gameFSM.Matchmaking);
        }
    }
}