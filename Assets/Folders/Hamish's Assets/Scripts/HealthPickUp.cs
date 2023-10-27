using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hamish
{
    public class HealthPickUp : MonoBehaviour
    {
        [SerializeField] int healingAmount = 25;
        BoxCollider bxCollider;
        void Start()
        {
            bxCollider = GetComponentInChildren<BoxCollider>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<ent_health>().Damage(-25);
            }
        }
    }
}