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

            return enemyClass.MoveToPlayer();
            ;
        }
    }
}

