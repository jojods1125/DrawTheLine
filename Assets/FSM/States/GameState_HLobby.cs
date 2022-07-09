using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState_HLobby : GameState
{
    private GameFSM _gameFSM;

    public GameState_HLobby(GameFSM stateMachine) : base("H_Lobby", stateMachine)
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

        if (_gameFSM.DBG_StartVal)
        {
            _gameFSM.DBG_StartVal = false;
            if (!_gameFSM.HavePlayed)
            {
                stateMachine.ChangeState(_gameFSM.H_Intro);
            }
            else
            {
                stateMachine.ChangeState(_gameFSM.H_Prompt);
            }
        }
    }
}