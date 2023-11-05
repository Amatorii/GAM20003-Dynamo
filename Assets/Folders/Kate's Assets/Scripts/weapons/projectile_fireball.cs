using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile_fireball : MonoBehaviour
{
    [Header("Projectile")] //variables about the fireball
    public float speed;
    public float width;

    public string[] contactLayers; //what the fireball can hit
    int contactLayermask; //converting to layermask

    [Header("Explosion")] //variables about the explosion/damage
    public float radius;
    public GameObject explosionEffect;

    //to be applied if enemy is hit
    public int enemyDamage; //scaling damage
    public int enemyDamageFloor; //minimum damage applied
    public float enemyKnockback;

    //to be applied if player is hit
    public int playerDamage;
    public int playerDamageFloor;
    public float playerKnockback;

    public string[] explosionLayers; //what the fireball can damage
    int explosionLayermask; //converting to layermask

    void Awake()
    {
        //layermask conversion
        explosionLayermask = LayerMask.GetMask(explosionLayers);
        contactLayermask = LayerMask.GetMask(contactLayers);
    }

    void FixedUpdate()
    {
        RaycastHit contact;
        if (Physics.SphereCast(transform.position, width, transform.forward, out contact, speed * Time.deltaTime, contactLayermask))
        {
            Explosion(contact); //if something is hit by the projectile, do an explosion
        }

        else
            transform.position += transform.forward * speed * Time.deltaTime; //if nothing is hit, move forward
    }

    void Explosion(RaycastHit contact)
    {
        Debug.Log("[" + name + "] Fireball: Impact at position " + contact.point + ".");

        Collider[] hits = Physics.OverlapSphere(contact.point, radius, explosionLayermask); //actual explosion
        for (int i = 0; i < hits.Length; i++)
        {
            Vector3 offset = hits[i].ClosestPoint(contact.point) - contact.point; //relative position of contact
            float proximity = 1 - (offset.magnitude / radius); //multiplier for distance from explosion         

            if (hits[i].tag == "Player") //if player is hit
            {
                Debug.Log("[" + name + "] Explosion: Player contact at relative position " + offset + ". Entity name " + hits[i].name + ".");

                hits[i].gameObject.GetComponent<ent_health>().DamageExplosion(offset, proximity, playerDamage, playerDamageFloor, playerKnockback);
            }

            else if (hits[i].CompareTag("Enemy"))//if non-player is hit (should only be those on enemy layer)
            {
                Debug.Log("[" + name + "] Explosion: Non-player contact at relative position " + offset + ". Entity name " + hits[i].name + ".");

                hits[i].gameObject.GetComponent<ent_health>().DamageExplosion(offset, proximity, enemyDamage, enemyDamageFloor, enemyKnockback);
            }
        }

        Debug.Log("[" + name + "] Fireball: Explosion complete. Destroying");
        Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
