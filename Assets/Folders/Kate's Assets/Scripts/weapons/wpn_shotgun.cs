using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wpn_shotgun : weapon_state
{
    Transform transform;
    GameObject hitDecal;

    int surfaceLayer;
    int damageLayer;

    public wpn_shotgun(Transform transIn, GameObject objIn)
    {
        name = "shotgun";

        transform = transIn;

        hitDecal = objIn; // explosion

        surfaceLayer = LayerMask.GetMask("World");
        damageLayer = LayerMask.GetMask("Enemy");
    }

    public override void fire()
    {
        //seeing how far before shotgun hits a wall
        RaycastHit rHit;
        float max;

        if (Physics.Raycast(transform.position, transform.forward, out rHit, 5, surfaceLayer))
            max = rHit.distance;
        else
            max = 5;

        //GameObject.Instantiate(hitDecal, transform.position + transform.forward, transform.rotation);
        Debug.Log("[" + name + "] Shotgun: Firing shot with range " + max + ".");

        //getting the actual enemy hits
        Collider[] hits = Physics.OverlapSphere(transform.position, max, damageLayer);

        foreach (Collider hit in hits)
        {
            Vector3 offset = hit.transform.position - transform.position; //relative position of contact
            offset = Vector3.ClampMagnitude(offset, 5); //needed for proximity calculations
            float proximity = 1 - (offset.magnitude / 5); //multiplier for distance from shotgun

            if (Vector3.Angle(transform.forward, offset.normalized) <= 30) //if contact is within the angle of spread
            {
                Debug.Log("[" + name + "] Shotgun: Found contact at relative position" + offset + ".");
                GameObject decal = GameObject.Instantiate(hitDecal, transform.position, transform.rotation);
                decal.GetComponent<RenderHit>().HitLine(transform.position, hit.transform.position);

                hit.gameObject.GetComponent<ent_health>().DamageShotgun(offset, proximity, 75, 25, 20);
                // damage, damage floor, knockback
            }
        }
    }
}
