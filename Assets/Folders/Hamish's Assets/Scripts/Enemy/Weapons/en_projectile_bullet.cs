using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class en_projectile_bullet : MonoBehaviour
{
    private BoxCollider boxCollider;
    private Rigidbody rb;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody>();
        StartCoroutine(MoveProjectile());
    }

    private void Update()
    {
        rb.AddForce(transform.forward * 100.0f, ForceMode.Force);
    }

    private IEnumerator MoveProjectile()
    {
        yield return new WaitForSeconds(1.0f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
