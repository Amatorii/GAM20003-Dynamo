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
        line.SetPosition(0, new Vector3(origin.x, origin.y, origin.z + 0.4f));
        line.SetPosition(1, new Vector3(hit.x, hit.y + 0.5f, hit.z));
    }
}
