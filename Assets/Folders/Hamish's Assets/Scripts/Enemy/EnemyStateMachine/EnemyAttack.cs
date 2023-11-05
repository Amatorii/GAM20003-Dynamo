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
            enemyClass.SetAnimationState(3);
            distanceToPlayer = Vector3.Distance(enemyClass.playerObject.transform.position, enemyClass.transform.position);
            EnemyState stateToReturn = enemyClass.AttackPlayer();

            if (enemyClass is RangedEnemy)
            {
                int distance = enemyClass.maxDistanceToChase;
                enemyClass.StopAgent();
            
                if(distanceToPlayer > distance)
                    return new EnemyChase(enemyClass);
            }

            return stateToReturn;
        }
    }
}
