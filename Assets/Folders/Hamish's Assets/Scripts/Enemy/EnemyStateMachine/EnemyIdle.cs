using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hamish.Enemy
{
    public class EnemyIdle : EnemyState
    {
        public EnemyIdle(Enemy _enemyClass) : base(_enemyClass)
        {
        }

        public override EnemyState RunState()
        {
            if (enemyClass._canSeePlayer)
            {
                if (enemyClass is RangedEnemy && distanceToPlayer < 5.0f)
                    return new EnemyAttack(enemyClass);

                return new EnemyMove(enemyClass);
            }
            return this;
        }
    }
}