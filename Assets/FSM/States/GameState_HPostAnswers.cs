using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState_HPostAnswers : GameState
{
    private GameFSM _gameFSM;

    public GameState_HPostAnswers(GameFSM stateMachine) : base("H_PostAnswers", stateMachine)
    {
        _gameFSM = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        GameManager.Instance.StartCinematic(2f, "Post Answers Cinematic");
    }

    public override void Exit()
    {
        base.Exit();
        _gameFSM.DBG_ClientPings = 0;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (_gameFSM.DBG_CineEndVal)
        {
            _gameFSM.DBG_CineEndVal = false;
            stateMachine.ChangeState(_gameFSM.H_Wait);
        }
    }
}
