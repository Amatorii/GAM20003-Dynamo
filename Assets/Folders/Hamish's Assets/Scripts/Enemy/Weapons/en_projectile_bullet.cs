using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class en_projectile_bullet : MonoBehaviour
{
    private BoxCollider boxCollider;
    private Rigidbody rb;
    private int damage;
    private int bulletSpeed;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody>();
        StartCoroutine(MoveProjectile());
    }

    private void FixedUpdate()
    {

    }

    private IEnumerator MoveProjectile()
    {
        yield return new WaitForSeconds(1.0f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag != null && other.CompareTag("Player"))
            other.GetComponent<ent_health>().Damage(damage);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }

    public void SetDamage(int damage) { this.damage = damage; }
    public void SetSpeed(int speed) { this.bulletSpeed = speed; }
}
