using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LobProjectile : MonoBehaviour
{
    public GameObject explosion;
    public Vector3 direction;
    public float speed = 5f;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(direction*speed, ForceMode.Impulse);
        StartCoroutine(Explode());
    }

    private IEnumerator Explode()
    {
        yield return new WaitForSeconds(3);
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (!collision.gameObject.CompareTag("Player"))
    //    {
    //        if (bounces >= 3)
    //        {
    //            
    //        }
    //        else
    //            bounces++;
    //    }
    //}
}
