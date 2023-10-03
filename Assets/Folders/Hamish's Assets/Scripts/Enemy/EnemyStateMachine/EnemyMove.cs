using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hamish.Enemy
{
    public class EnemyMove : EnemyState
    {
        public EnemyMove(Enemy _enemyClass) : base(_enemyClass)
        {
        }

        public override EnemyState RunState()
        {
            distanceToPlayer = Vector3.Distance(enemyClass.playerObject.transform.position, enemyClass.transform.position);
            if (enemyClass._canSeePlayer && distanceToPlayer < 2.5f)
            {
                enemyClass.MoveAwayFromPlayer();
                Debug.Log("[" + this + "]Message: RUN AWAY");
                return this;
            }

            if (enemyClass._canSeePlayer)
                return new EnemyChase(enemyClass);

            if (!enemyClass._canSeePlayer && !enemyClass.IsEnemyMoving())
            {
                enemyClass.Strafe();
                return this;
            }
            return this;
        }
    }
}
