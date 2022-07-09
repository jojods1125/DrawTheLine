using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState_Title : GameState
{
    private GameFSM _gameFSM;

    public GameState_Title(GameFSM stateMachine) : base("Title", stateMachine)
    {
        _gameFSM = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (_gameFSM.DBG_ToMatchVal)
        {
            _gameFSM.DBG_ToMatchVal = false;
            stateMachine.ChangeState(_gameFSM.Matchmaking);
        }
    }
}