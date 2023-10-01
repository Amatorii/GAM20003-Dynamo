using Hamish.Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState
{
    protected Enemy enemyClass;
    public EnemyState(Enemy _enemyClass)
    {
        enemyClass = _enemyClass;
    }

    public abstract EnemyState RunState();
}
