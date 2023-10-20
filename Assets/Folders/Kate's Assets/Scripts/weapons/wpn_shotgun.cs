using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wpn_shotgun : weapon_state
{
    int surfaceLayer;
    int damageLayer;

    public state_manager body;

    public float range; // maximum distance shotgun will check for enemies

    public float knockback; // knockback coefficient

    public float knockbackPlayer; // for player pushback

    public int spread; // shotgun spread angle

    public int damageBase; // base damage applied before scaling damage is considered
    public int damage; // additional scaling damage that is added on top of base damage

    void Awake()
    {
        surfaceLayer = LayerMask.GetMask("World", "Enemy");
        damageLayer = LayerMask.GetMask("Enemy");
    }

    public override void fire()
    {
        // instantiating effect
        if (projectile != null)
            Object.Instantiate(projectile, transform.position + transform.forward * 0.5f, transform.rotation);

        // seeing how far before shotgun hits a wall
        RaycastHit rHit;
        float max;

        if (Physics.Raycast(transform.position, transform.forward, out rHit, range, surfaceLayer))
            max = rHit.distance;
        else
            max = range;

        Debug.Log("[" + name + "] Shotgun: Firing shot with range " + max + ".");

        // getting the actual enemy hits
        Collider[] hits = Physics.OverlapSphere(transform.position, max, damageLayer);

        foreach (Collider hit in hits)
        {
            Vector3 offset = hit.transform.position - transform.position; // relative position of contact
            offset = Vector3.ClampMagnitude(offset, range); // needed for proximity calculations
            float proximity = 1 - (offset.magnitude / range); // multiplier for distance from shotgun

            if (Vector3.Angle(transform.forward, offset.normalized) <= spread) // if contact is within the angle of spread
            {
                Debug.Log("[" + name + "] Shotgun: Found contact at relative position" + offset + ".");

                hit.gameObject.GetComponent<ent_health>().DamageShotgun(offset, proximity, damage, damageBase, knockback);
                // damage, damage floor, knockback
            }
        }

        // launching player
        Vector3 launch = transform.forward;

        Vector3 vIn = body.velocity;
        if (vIn[1] < 0)
            vIn[1] = 0;

        // dot product coefficient - so the gun doesn't stop the player in their tracks
        if (body.velocity.magnitude > 0)
            launch *= knockbackPlayer - knockbackPlayer * Mathf.Clamp01(Vector3.Dot(launch, vIn.normalized));

        launch *= -1;

        body.Launch(vIn + launch);
    }
}
