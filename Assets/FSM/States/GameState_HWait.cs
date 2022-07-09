using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState_HWait : GameState
{
    private GameFSM _gameFSM;

    public GameState_HWait(GameFSM stateMachine) : base("H_Wait", stateMachine)
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

        if (_gameFSM.DBG_ClientPings >= _gameFSM.NumPlayers)
        {
            _gameFSM.DBG_ClientPings = 0;

            switch (_gameFSM.previousState)
            {
                case GameState_HPrompt _:
                    stateMachine.ChangeState(_gameFSM.H_PostAnswers);
                    break;

                case GameState_HPostAnswers _:
                    stateMachine.ChangeState(_gameFSM.H_Spectrum);
                    break;

                case GameState_HSpectrum _:
                    stateMachine.ChangeState(_gameFSM.H_Results);
                    break;

                default:
                    stateMachine.ChangeState(_gameFSM.Title);
                    break;
            }
        }
    }
}
