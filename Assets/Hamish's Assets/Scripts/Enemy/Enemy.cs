using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hamish.Enemy
{
    public abstract class Enemy : MonoBehaviour
    {
        [SerializeField] protected float health;
        private Rigidbody rb;
        private BoxCollider hitBox;

        public virtual void Init()
        {
            rb = GetComponent<Rigidbody>();
            hitBox = GetComponent<BoxCollider>();
        }

        public abstract void TakeDamage(float damage);
    }
}