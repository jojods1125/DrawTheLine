using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState_HCurrentScores : GameState
{
    private GameFSM _gameFSM;

    public GameState_HCurrentScores(GameFSM stateMachine) : base("H_CurrentScores", stateMachine)
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
            if (_gameFSM.CurrentRound < _gameFSM.RoundMax)
            {
                stateMachine.ChangeState(_gameFSM.H_Prompt);
            }
            else
            {
                stateMachine.ChangeState(_gameFSM.H_FinalScores);
            }
        }
    }
}
