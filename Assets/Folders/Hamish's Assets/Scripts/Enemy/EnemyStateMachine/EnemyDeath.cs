using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hamish.Enemy
{
    public class EnemyDeath : EnemyState
    {
        public EnemyDeath(Enemy _enemyClass) : base(_enemyClass)
        {
        }

        public override EnemyState RunState()
        {
            enemyClass.SetAnimationState(4);
            return this;
        }
    }
}
