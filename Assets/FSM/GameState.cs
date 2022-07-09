using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameState
{
    public string name;
    protected StateMachine stateMachine;

    public GameState(string name, StateMachine stateMachine)
    {
        this.name = name;
        this.stateMachine = stateMachine;
    }

    public virtual void Enter() { }
    public virtual void UpdateLogic() { }
    public virtual void UpdatePhysics() { }
    public virtual void Exit() { }
}