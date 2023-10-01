using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rail_system : MonoBehaviour
{
#region variables

    // input stuff
    [SerializeField] private Transform[] pointsIn; // used to set up the rail segments, deleted at runtime
    [SerializeField] private float radius; // radius for trigger volume
    [SerializeField] private GameObject segmentPrefab; // basic segment prefab for setup

    // rail segment data
    private rail_segment[] segmentList; // list of all rail segments
    private float[] segmentLengths; // holds the lengths of each rail segment

    // rail data
    public float totalLength; // combined length of all segments

#endregion

#region initialisation

    void CreateRail() // used to initialise rail system
    {
        Debug.Log("[" + name + "] Rail System: Creating new system...");

        segmentList = new rail_segment[pointsIn.Length - 1];
        segmentLengths = new float[pointsIn.Length - 1];
        totalLength = 0;

        for (int i = 0; i < (pointsIn.Length - 1); i++)
        {
            GameObject child = GameObject.Instantiate(segmentPrefab, transform);
            rail_segment segment = child.GetComponent<rail_segment>();
            child.name = "Segment " + i;

            segment.NewSegment(pointsIn[i], pointsIn[i + 1], radius, i);
            
            segmentList[i] = segment;
            segmentLengths[i] = segment.length;
            totalLength += segment.length;
        }

        foreach (Transform t in pointsIn) // getting rid of the setup points for clarity
        { Destroy(t.gameObject); }

        Debug.Log("[" + name + "] Rail System: Finished creating new system with total length " + totalLength + ".");
    }

    void Awake()
    {
        CreateRail();
    }

#endregion

#region functions

    int FindSegment(float posIn) // finds out which segment a linear position is on - returns -1 if there is an error (shouldn't be possible)
    {
        if (posIn > totalLength || posIn < 0)
        { return -1; Debug.Log("[" + name + "] Find rail segment: couldn't find segment for input " + posIn + " - out of bounds."); }

        else
        {
            float n = 0; // throwaway number for counting cumulative length

            for (int i = 0; i < segmentList.Length; i++)
            {
                n += segmentLengths[i];
                if (posIn <= n)
                    return i; break;
            }
            return -1;
            Debug.Log("[" + name + "] Find rail segment: couldn't find segment for input " + posIn + " - unknown error.");
        }
    }

    public Vector3 ProjectOnRail (float posIn) // gets a linear length and maps it onto the world-space rail
    {
        float pos = Mathf.Clamp(posIn, 0, totalLength); // clamping it so we don't get any fucked up returns

        int n = FindSegment(pos); // gets index of rail segment
        Debug.Log("[" + name + "] Project on rail: found linear position " + pos + " on segment " + n + ".");

        for (int i = 0; i < n; i++)
        { pos -= segmentLengths[i]; } // gets length on the segment itself
        Debug.Log("[" + name + "] Project on rail: linear position on segment " + n + " is " + pos + ".");

        return segmentList[n].ProjectOnSegment(pos);
    }

    public float GetLinearPosition (float posIn, int index) // used by player movement to find the overall position of the player on the rail
    {
        if (index == 0)
            return posIn;
        else
        {
            float count = 0; // adding up distances

            for (int i = 0; i < index; i++)
            { count += segmentLengths[i]; }

            Debug.Log("[" + name + "] Get linear position: local position on segment " + index + " converts to " + count + ".");

            return count + posIn;
        }
    }

    public Vector3 GetDirection (float posIn) // gets normalised direction of movement
    { return segmentList[FindSegment(posIn)].GetRailDirection(); }

    public IEnumerator Skip() // disables contact for a period of time. used for exiting rail grinds
    {
        foreach(rail_segment i in segmentList)
        { i.gameObject.SetActive(false); }

        yield return new WaitForSeconds(0.5f);

        foreach (rail_segment i in segmentList)
        { i.gameObject.SetActive(true); }
    }

#endregion
}
