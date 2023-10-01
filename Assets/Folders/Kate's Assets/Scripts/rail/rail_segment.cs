using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rail_segment : MonoBehaviour
{
#region variables

    Vector3 pointA;
    Vector3 pointB;

    [SerializeField]
    public float length; // length of rail
    float radius; // radius of volume

    public CapsuleCollider volume;

#endregion

#region initialisation

    public rail_segment (Transform a, Transform b, float radIn) // create new segment from a start and end point - also calls createvolume
    {
        //set values
        pointA = a.position;
        pointB = b.position;

        length = (pointB - pointA).magnitude;
        radius = radIn;

        transform.position = a.position;
        transform.LookAt(b);

        CreateVolume();

        Debug.Log("[" + name + "] Rail Segment: Created new segment from point " + pointA + " to " + pointB + " with length " + length + ".");
    }

    void CreateVolume() // sets up the trigger for this rail segment
    {
        //volume = gameObject.AddComponent(typeof(CapsuleCollider)) as CapsuleCollider;

        //volume.isTrigger = true;

        volume.direction = 2; //points forward
        volume.height = length + (radius * 2);

        volume.center = transform.forward * (length / 2);
    }

#endregion

    public void StartGrind() // does stuff for starting a grind - finding position for snapping, choosing direction etc.
    {

    }

#region conversions

    public Vector3 ProjectOnSegment(float posIn) // takes a distance input and puts it on the segment in 3D space
    { return transform.position + (transform.forward * posIn); }

    public float GetRailPosition(Vector3 posIn) // does the opposite of above - takes a world-space point and maps it to the rail as a linear value
    {
        Vector3 localPos = posIn - transform.position; // local space position
        return Vector3.Project(localPos, transform.forward).magnitude;
    }

#endregion
}
