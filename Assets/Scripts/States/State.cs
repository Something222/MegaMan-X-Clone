using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class State
{
    public Phase phase;
    protected State nextState;
    public enum Phase
    {
        ENTER, UPDATE, EXIT
    };
    public virtual void Enter()
    {
        phase = Phase.UPDATE;
    }
    public virtual void Update() { phase = Phase.UPDATE; }
    public virtual void Exit() { phase = Phase.EXIT; }
    public virtual State Process()
    {
        if (phase == Phase.ENTER) Enter();
        else if (phase == Phase.UPDATE) Update();
        else if (phase == Phase.EXIT)
        {
            Exit();
            return nextState;
        }
        return this;
    }
}
