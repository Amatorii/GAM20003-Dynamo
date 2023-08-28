using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnProjectile : MonoBehaviour
{
    public GameObject projectile;
    public GameObject nade;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject bullet =  GameObject.Instantiate(projectile, transform.position + transform.forward, transform.rotation);
            bullet.GetComponent<Projectile>().direction = transform.forward;
        }
        if (Input.GetMouseButtonDown(1))
        {
            GameObject bullet = GameObject.Instantiate(nade, transform.position + transform.forward, transform.rotation);
            bullet.GetComponent<LobProjectile>().direction = transform.forward;
        }
    }
}
