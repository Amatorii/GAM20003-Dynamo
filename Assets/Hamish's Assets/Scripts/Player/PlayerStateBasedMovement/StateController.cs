using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateController : MonoBehaviour
{
    protected Movement _movement;
    public Movement currentMove { get; private set; }

    public void StartState(Movement movement)
    {
        _movement = movement;
        currentMove = movement;
        _movement.ExecuteMovement();
    }

    public void RunStateMachine()
    {
        Movement nextMove = currentMove?.ExecuteMovement();
        SetState(nextMove);
    }

    private void SetState(Movement move)
    {
        currentMove = move;
    }
}
