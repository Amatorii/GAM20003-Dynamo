using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reflect : MonoBehaviour
{
    public float range; //how close must the projectile be
    public Vector3 reflectOffset; //how far from player reflected projectiles spawn

    //layer masks
    public string projectileTag;

    public GameObject reflectPrefab; //the prefab for the reflected projectile

    void Awake()
    {
        Reflect(); //debug only. delete once firing script is complete
    }

    void Reflect()
    {
        GameObject[] hits = GameObject.FindGameObjectsWithTag(projectileTag); //finding all enemy projectiles

        if (hits.Length > 0)
        {
            Transform origin = transform;
            origin.position += (transform.right * reflectOffset[0]) + (transform.up * reflectOffset[1]) + (transform.forward * reflectOffset[2]); //applying offset to reflected projectiles

            foreach (GameObject hit in hits)
            {
                Vector3 offset = hit.transform.position - transform.position; //relative position

                if (offset.magnitude <= range) //filtering out projectiles that are out of range
                {
                    Debug.Log("[" + name + "] Reflect: Found projectile" + hit.name + "at relative position " + offset + ". Reflecting in direction " + transform.forward + " at position " + origin.position + " and destroying original.");

                    Instantiate(reflectPrefab, origin.position, origin.rotation); //creating reflected projectile
                    Destroy(hit); //destroying original projectile
                }
            }
        }
    }
}
