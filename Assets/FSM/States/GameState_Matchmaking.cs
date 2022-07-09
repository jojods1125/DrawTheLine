using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState_Matchmaking : GameState
{
    private GameFSM _gameFSM;

    public GameState_Matchmaking(GameFSM stateMachine) : base("Matchmaking", stateMachine)
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

        if (_gameFSM.DBG_ToHLobbyVal)
        {
            _gameFSM.DBG_ToHLobbyVal = false;
            stateMachine.ChangeState(_gameFSM.H_Lobby);
        }
        else if (_gameFSM.DBG_ToCLobbyVal)
        {
            _gameFSM.DBG_ToCLobbyVal = false;
            stateMachine.ChangeState(_gameFSM.C_Lobby);
        }
    }
}