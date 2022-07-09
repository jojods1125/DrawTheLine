using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState_CPostGame : GameState
{
    private GameFSM _gameFSM;

    public GameState_CPostGame(GameFSM stateMachine) : base("C_PostGame", stateMachine)
    {
        _gameFSM = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        _gameFSM.HavePlayed = true;
        _gameFSM.CurrentRound = 0;
    }

    public override void Exit()
    {
        base.Exit();
        _gameFSM.DBG_HostPingVal = false;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (_gameFSM.DBG_ReplayVal)
        {
            _gameFSM.DBG_ReplayVal = false;
            stateMachine.ChangeState(_gameFSM.C_Lobby);
        }
        else if (_gameFSM.DBG_ExitVal)
        {
            _gameFSM.DBG_ExitVal = false;
            stateMachine.ChangeState(_gameFSM.Matchmaking);
        }
    }
}
