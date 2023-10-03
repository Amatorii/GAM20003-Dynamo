using Hamish.Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState
{
    protected Enemy enemyClass;
    protected float distanceToPlayer;
    public EnemyState(Enemy _enemyClass)
    {
        enemyClass = _enemyClass;
        distanceToPlayer = Vector3.Distance(enemyClass.playerObject.transform.position, enemyClass.transform.position);
    }

    public abstract EnemyState RunState();
}
