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
            distanceToPlayer = Vector3.Distance(enemyClass.playerObject.transform.position, enemyClass.transform.position);

            if (enemyClass is ent_rangedEnemy && distanceToPlayer < 10.0f)
                return new EnemyAttack(enemyClass);
            if(enemyClass is ent_rangedEnemy && distanceToPlayer < 5.0f)
                return new EnemyMove(enemyClass);

            enemyClass.MoveToPlayer();

            return this;
        }
    }
}

