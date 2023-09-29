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
                return new EnemyChase(enemyClass);
            return this;
        }
    }
}