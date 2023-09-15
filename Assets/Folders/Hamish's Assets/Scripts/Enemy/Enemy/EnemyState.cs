using Hamish.Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState
{
    protected EnemyClass enemyClass;
    protected EnemyStateMachine stateMachine;

    public EnemyState(EnemyClass _enemyClass, EnemyStateMachine _stateMachine)
    {
        this.enemyClass = _enemyClass;
        this.stateMachine = _stateMachine;
    }

    public abstract EnemyState RunState();
}
