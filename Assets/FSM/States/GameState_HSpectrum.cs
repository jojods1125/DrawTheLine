using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState_HSpectrum : GameState
{
    private GameFSM _gameFSM;

    public GameState_HSpectrum(GameFSM stateMachine) : base("H_Spectrum", stateMachine)
    {
        _gameFSM = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        GameManager.Instance.StartCinematic(2f, "Spectrum Cinematic");
    }

    public override void Exit()
    {
        base.Exit();
        _gameFSM.DBG_ClientPings = 0;
        GameManager.Instance.NetworkManager.SendAverageRankingToClients(GameManager.Instance.CalculateAverageRanking()); ;
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
