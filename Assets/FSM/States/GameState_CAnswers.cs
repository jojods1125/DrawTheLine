using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState_CAnswers : GameState
{
    private GameFSM _gameFSM;

    public GameState_CAnswers(GameFSM stateMachine) : base("C_Answers", stateMachine)
    {
        _gameFSM = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        _gameFSM.CurrentRound++;
    }

    public override void Exit()
    {
        base.Exit();
        _gameFSM.DBG_HostPingVal = false;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (_gameFSM.DBG_ClientSubmitVal)
        {
            _gameFSM.DBG_ClientSubmitVal = false;
            stateMachine.ChangeState(_gameFSM.C_Wait);
        }
    }
}
