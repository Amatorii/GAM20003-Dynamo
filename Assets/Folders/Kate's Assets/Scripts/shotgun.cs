using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//H: Remember what I said. Specifically for the weapons, I highly recommend you make an abstract base weapon class and have all weapons inherit that base.
public class shotgun : MonoBehaviour
{
    [Header("Bounds")]
    public float range;
    public float spread; //angle threshold for getting hit

    public string[] surfaceLayers; //filtering for walls
    int surfaceLayermask; //converting to layermask

    [Header("Damage")]
    public int damage; //scaling damage
    public int damageFloor; //minimum damage applied
    public float knockback;

    public string[] damageLayers; //filtering for enemies
    int damageLayermask; //converting to layermask

    void Awake()
    {
        //layermask conversion
        surfaceLayermask = LayerMask.GetMask(surfaceLayers);
        damageLayermask = LayerMask.GetMask(damageLayers);

        ShotgunShoot(); //debug only. delete once firing script is complete
    }

    void ShotgunShoot()
    {
        //seeing how far before shotgun hits a wall
        RaycastHit rHit;
        float max;

        if (Physics.Raycast(transform.position, transform.forward, out rHit, range, surfaceLayermask))
            max = rHit.distance;
        else
            max = range;

        Debug.Log("[" + name + "] Shotgun: Firing shot with range" + max + ".");

        //getting the actual enemy hits
        Collider[] hits = Physics.OverlapSphere(transform.position, max, damageLayermask);

        foreach (Collider hit in hits)
        {
            Vector3 offset = hit.transform.position - transform.position; //relative position of contact
            offset = Vector3.ClampMagnitude(offset, range); //needed for proximity calculations
            float proximity = 1 - (offset.magnitude / range); //multiplier for distance from shotgun

            if (Vector3.Angle(transform.forward, offset.normalized) <= spread) //if contact is within the angle of spread
            {
                Debug.Log("[" + name + "] Shotgun: Found contact at relative position" + offset + ".");

                hit.gameObject.GetComponent<ent_health>().DamageShotgun(offset, proximity, damage, damageFloor, knockback);
            }
        }
    }
}
