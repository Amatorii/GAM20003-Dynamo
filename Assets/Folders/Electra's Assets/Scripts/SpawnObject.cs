using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    public KeyCode spawnKey;
    public GameObject spawnObject;

    // Update is called once per frame
    void Update()
    {
       if (Input.GetKeyDown(spawnKey))
            Instantiate(spawnObject, transform.position, transform.rotation);
    }
}
