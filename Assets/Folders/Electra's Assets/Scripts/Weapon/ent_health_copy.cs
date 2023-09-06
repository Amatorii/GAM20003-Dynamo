using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//H: The class name should always be capitalized. i.e. Ent_Health
//Also is this class meant to be abstract?
public class ent_health_copy : MonoBehaviour
{
    public int health; //health points
                       //H: Do you want other scripts to be able to change an entity's health? Cus usually, only the ent_health script should handle that

    //H: Make sure you add private or public to methods and variables
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
    //H: This just allows people to see what the method does by mousing over it even when in other scripts
    /// <summary>
    /// handles calculating damage and knockback from explosion
    /// </summary>
    public void DamageExplosion(Vector3 offset, float proximity, int damage, int damageFloor, float knockback) 
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
    /// <summary>
    /// handles calculating damage and knockback from shotgun
    /// </summary>
    public void DamageShotgun(Vector3 offset, float proximity, int damage, int damageFloor, float knockback) 
    {
        int damageOut = Mathf.RoundToInt(damageFloor + (damage * proximity));

        bool kill = Damage(damageOut);

        if (!kill)
        {
            Vector3 launch = offset * knockback * proximity;

            Debug.Log("[" + name + "] Shotgun Damage: Received knockback force of " + launch + ".");
            //launch method goes here
        }
    }
}
