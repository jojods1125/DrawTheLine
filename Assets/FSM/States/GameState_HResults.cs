using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState_HResults : GameState
{
    private GameFSM _gameFSM;

    public GameState_HResults(GameFSM stateMachine) : base("H_Results", stateMachine)
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

        if (_gameFSM.DBG_CineEndVal)
        {
            _gameFSM.DBG_CineEndVal = false;
            stateMachine.ChangeState(_gameFSM.H_CurrentScores);
        }
    }
}
