using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad]

public class WaypointEditor 
{
    [DrawGizmo(GizmoType.NonSelected | GizmoType.Selected | GizmoType.Pickable)]
    public static void OnDrawSceneGizmos(WayPoint waypoint, GizmoType gizmoType)
    {
        if ((gizmoType & GizmoType.Selected) != 0)
        {
            Gizmos.color = Color.blue;
        }
        else
        {
            Gizmos.color = Color.blue * 0.5f;
        }
        Gizmos.DrawSphere(waypoint.transform.position, 0.1f);

        Gizmos.color = Color.white;
        Gizmos.DrawLine(waypoint.transform.position + (waypoint.transform.right * waypoint.waypointWidth / 2f), waypoint.transform.position - (waypoint.transform.right * waypoint.waypointWidth / 2f));


        if(waypoint.previousWaypoint !=null)
        {
            Gizmos.color = Color.red;
            Vector3 offset = waypoint.transform.right * waypoint.waypointWidth / 2;
            Vector3 offsetto = waypoint.previousWaypoint.transform.right * waypoint.previousWaypoint.waypointWidth / 2;
            Gizmos.DrawLine(waypoint.transform.position +offset , waypoint.previousWaypoint.transform.position + offsetto);
        }

        if (waypoint.nextWaypoint != null)
        {
            Gizmos.color = Color.green;
            Vector3 offset = waypoint.transform.right *- waypoint.waypointWidth / 2;
            Vector3 offsetto = waypoint.previousWaypoint.transform.right * -waypoint.previousWaypoint.waypointWidth / 2;
            Gizmos.DrawLine(waypoint.transform.position + offset, waypoint.previousWaypoint.transform.position + offsetto);
        }

    }
}
