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
            enemyClass.SetAnimationState(1);
            distanceToPlayer = Vector3.Distance(enemyClass.playerObject.transform.position, enemyClass.transform.position);

            if (enemyClass is RangedEnemy && distanceToPlayer < 10.0f)
                return new EnemyAttack(enemyClass);
            if(enemyClass is RangedEnemy && distanceToPlayer < 5.0f)
                return new EnemyMove(enemyClass);
            enemyClass.MoveToPlayer();


            if (enemyClass is MeleeEnemy && distanceToPlayer <= 3)
            {
                return new EnemyAttack(enemyClass);
            }
            return this;
        }
    }
}

