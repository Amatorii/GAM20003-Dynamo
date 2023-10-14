using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ent_health : MonoBehaviour
{
    public int health;
    // health points

    public bool immortal;
    // if damage is ignored

    state_manager body;

    void Awake()
    {
        body = GetComponent<state_manager>();
    }

    //H: I've made this public so the enemies can call it
    public bool Damage(int damage)
    {
        if (immortal)
            return false;
        else
        {
            health -= damage;

            if (health <= 0)
            {
                Debug.Log("[" + name + "] Health: Received " + damage + " points of damage - lethal amount.");
                // death code goes here
                return true;
            }

            else
            {
                Debug.Log("[" + name + "] Health: Received " + damage + " points of damage.");
                return false;
            }
        }
    }

    public void DamageExplosion(Vector3 offset, float proximity, int damage, int damageFloor, float knockback) // handles calculating damage and knockback from explosion
    {
        int damageOut = Mathf.RoundToInt(damageFloor + (damage * proximity));

        bool kill = Damage(damageOut);

        if (!kill)
        {
            Vector3 launch = offset.normalized * knockback * proximity;

            Debug.Log("[" + name + "] Explosion Damage: Received knockback force of " + launch + ".");
            body.Launch(launch);
        }
    }

    public void DamageShotgun(Vector3 offset, float proximity, int damage, int damageFloor, float knockback) // handles calculating damage and knockback from shotgun
    {
        int damageOut = Mathf.RoundToInt(damageFloor + (damage * proximity));

        bool kill = Damage(damageOut);

        if (!kill)
        {
            Vector3 launch = (offset.normalized * knockback * proximity) + Vector3.up * 7.5f;

            Debug.Log("[" + name + "] Shotgun Damage: Received knockback force of " + launch + ".");
            body.Launch(launch);
        }
    }
}
