using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hamish.Enemy
{
    public class EnemyAttack : EnemyState
    {
        public EnemyAttack(Enemy _enemyClass) : base(_enemyClass)
        {
        }

        public override EnemyState RunState()
        {
            enemyClass.StopAgent();
            EnemyState stateToReturn = enemyClass.AttackPlayer();
            if(enemyClass is ent_rangedEnemy && distanceToPlayer > 15.0f)
                return new EnemyChase(enemyClass);
            return stateToReturn;
        }
    }
}
