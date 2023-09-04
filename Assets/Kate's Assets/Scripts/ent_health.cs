using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ent_health : MonoBehaviour
{
    public int health; //health points

    public void DamageExplosion(Vector3 offset, float proximity, int damage, int damageFloor, float knockback) //handles calculating damage and knockback from explosion
    {
        int damageOut = Mathf.RoundToInt(damageFloor + (damage * proximity));

        bool kill = Damage(damageOut);

        if (!kill)
        {
            Vector3 launch = offset * knockback * proximity;

            Debug.Log("[" + name + "] Explosion Damage: Received knockback force of " + launch + ".");
            //launch method goes here
        }
    }

    bool Damage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Debug.Log("[" + name + "] Health: Received " + damage + " points of damage - lethal amount.");
            //death code goes here
            return true;
        }

        else
        {
            Debug.Log("[" + name + "] Health: Received " + damage + " points of damage.");
            return false;
        }
    }
}
