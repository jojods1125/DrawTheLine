using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState_CWait : GameState
{
    private GameFSM _gameFSM;

    public GameState_CWait(GameFSM stateMachine) : base("C_Wait", stateMachine)
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
        _gameFSM.DBG_ClientSubmitVal = false;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (_gameFSM.DBG_HostPingVal)
        {
            _gameFSM.DBG_HostPingVal = false;

            switch (_gameFSM.previousState)
            {
                case GameState_CLobby _:
                    stateMachine.ChangeState(_gameFSM.C_Answers); // Prompt ping
                    break;

                case GameState_CAnswers _:
                    stateMachine.ChangeState(_gameFSM.C_Ranking); // Ranking ping
                    break;

                case GameState_CRanking _:
                    stateMachine.ChangeState(_gameFSM.C_Line); // Line ping
                    break;

                case GameState_CLine _:
                    if (_gameFSM.CurrentRound >= _gameFSM.RoundMax)
                    {
                        stateMachine.ChangeState(_gameFSM.C_PostGame);
                    }
                    else
                    {
                        stateMachine.ChangeState(_gameFSM.C_Answers);
                    }
                    break;

                default:
                    stateMachine.ChangeState(_gameFSM.Title);
                    break;
            }
        }
    }
}
