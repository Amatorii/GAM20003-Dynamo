using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hamish.Enemy
{
    public class DummyEnemy : Enemy
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public override void TakeDamage(float damage)
        {
            throw new System.NotImplementedException();
        }
    }
}