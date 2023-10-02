using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderHit : MonoBehaviour
{
    private LineRenderer line;
    void Awake()
    {
        line = GetComponent<LineRenderer>();
        Destroy(gameObject, 1f);
    }

    public void HitLine(Vector3 origin, Vector3 hit)
    {
        line.SetPosition(0, origin);
        line.SetPosition(1, hit);
    }
}
