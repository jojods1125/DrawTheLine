using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState_CLine : GameState
{
    private GameFSM _gameFSM;

    public GameState_CLine(GameFSM stateMachine) : base("C_Line", stateMachine)
    {
        _gameFSM = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
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
