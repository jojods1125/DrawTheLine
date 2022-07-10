using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState_CRanking : GameState
{
    private GameFSM _gameFSM;

    public GameState_CRanking(GameFSM stateMachine) : base("C_Ranking", stateMachine)
    {
        _gameFSM = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        GameManager.Instance.StartTimer(GameManager.Instance.TimerDuration);
        UIManager.Instance.SetClientRanking(true);
    }

    public override void Exit()
    {
        base.Exit();
        _gameFSM.DBG_HostPingVal = false;
        GameManager.Instance.CancelTimer();
        UIManager.Instance.SetClientRanking(false);
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (_gameFSM.DBG_ClientSubmitVal)
        {
            _gameFSM.DBG_ClientSubmitVal = false;
            stateMachine.ChangeState(_gameFSM.C_Wait);
        }
        else if (_gameFSM.DBG_TimerEndVal)
        {
            _gameFSM.DBG_TimerEndVal = false;
            stateMachine.ChangeState(_gameFSM.C_Wait);
        }
    }
}
