using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState_HFinalScores : GameState
{
    private GameFSM _gameFSM;

    public GameState_HFinalScores(GameFSM stateMachine) : base("H_FinalScores", stateMachine)
    {
        _gameFSM = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        GameManager.Instance.StartCinematic(2f, "Final Scores Cinematic");
    }

    public override void Exit()
    {
        base.Exit();
        GameManager.Instance.NetworkManager.SendFinalScoresToClients();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (_gameFSM.DBG_CineEndVal)
        {
            _gameFSM.DBG_CineEndVal = false;
            stateMachine.ChangeState(_gameFSM.H_PostGame);
        }
    }
}
