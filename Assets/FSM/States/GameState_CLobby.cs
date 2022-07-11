using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState_CLobby : GameState
{
    private GameFSM _gameFSM;

    public GameState_CLobby(GameFSM stateMachine) : base("C_Lobby", stateMachine)
    {
        _gameFSM = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        UIManager.Instance.SetClientWait(true);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (_gameFSM.DBG_StartVal)
        {
            _gameFSM.DBG_StartVal = false;
            stateMachine.ChangeState(_gameFSM.C_Wait);
        }
        else if (_gameFSM.DBG_ExitVal)
        {
            _gameFSM.DBG_ExitVal = false;
            stateMachine.ChangeState(_gameFSM.Matchmaking);
        }
    }
}