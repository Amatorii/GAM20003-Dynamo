using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hamish.Enemy
{
    public class EnemyChase : EnemyState
    {
        public EnemyChase(Enemy _enemyClass) : base(_enemyClass)
        {
        }

        public override EnemyState RunState()
        {
            if (!enemyClass._canSeePlayer)
                return new EnemyIdle(enemyClass);

            if (enemyClass is ent_rangedEnemy && Vector3.Distance(enemyClass.playerObject.transform.position, enemyClass.transform.position) > 5.0f)
                return new EnemyAttack(enemyClass);

            enemyClass.MoveToPlayer();

            return this;
        }
    }
}

