using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hamish.Enemy
{
    public class DummyEnemy : Enemy
    {
        // Start is called before the first frame update
        public override void Init()
        {
            base.Init();
        }

        // Update is called once per frame
        void Update()
        {
            if (_canSeePlayer)
            {
                _agent.SetDestination(playerObject.transform.position);
                Debug.Log("[" + this + "]: Sees the player");
            }
        }
    }
}