using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState_HIntro : GameState
{
    private GameFSM _gameFSM;

    public GameState_HIntro(GameFSM stateMachine) : base("H_Intro", stateMachine)
    {
        _gameFSM = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        GameManager.Instance.StartCinematic(2f, "Intro Cinematic");
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if(_gameFSM.DBG_CineEndVal)
        {
            _gameFSM.DBG_CineEndVal = false;
            stateMachine.ChangeState(_gameFSM.H_Prompt);
        }
    }
}
