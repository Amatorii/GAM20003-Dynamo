using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Hamish.Enemy;

[CustomEditor(typeof(ent_rangedEnemy))]
public class FOVEditorScript : Editor
{
    private void OnSceneGUI()
    {
        ent_rangedEnemy fov = (ent_rangedEnemy)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov._radius);

        Vector3 viewAngle01 = DirectionFromAngle(fov.transform.eulerAngles.y, -fov._angle / 2);
        Vector3 viewAngle02 = DirectionFromAngle(fov.transform.eulerAngles.y, fov._angle / 2);

        Handles.color = Color.yellow;
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle01 * fov._radius);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle02 * fov._radius);

        if (fov._canSeePlayer)
        {
            Handles.color = Color.green;
            Handles.DrawLine(fov.transform.position, fov.playerObject.transform.position);
        }
    }

    private Vector3 DirectionFromAngle(float eulerY, float andlesInDegrees)
    {
        andlesInDegrees += eulerY;
        return new Vector3(Mathf.Sin(andlesInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(andlesInDegrees * Mathf.Deg2Rad));
    }
}
