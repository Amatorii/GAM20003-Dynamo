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
            EnemyState stateToReturn = enemyClass.AttackPlayer();
            return stateToReturn;
        }
    }
}
