using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState_HPostGame : GameState
{
    private GameFSM _gameFSM;

    public GameState_HPostGame(GameFSM stateMachine) : base("H_PostGame", stateMachine)
    {
        _gameFSM = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        _gameFSM.HavePlayed = true;
        _gameFSM.CurrentRound = 0;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (_gameFSM.DBG_ReplayVal)
        {
            _gameFSM.DBG_ReplayVal = false;
            stateMachine.ChangeState(_gameFSM.H_Lobby);
        }
        else if (_gameFSM.DBG_ExitVal)
        {
            _gameFSM.DBG_ExitVal = false;
            stateMachine.ChangeState(_gameFSM.Matchmaking);
        }
    }
}
