using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class WayPointManagerWindow : EditorWindow 
{
    [MenuItem("Waypoint/ WayPoint Editor Tool")]
    public static void showWindow()
    {
        GetWindow<WayPointManagerWindow>("Waypoint Editor tool");
    }

    public Transform WaypointOrigin;

    private void OnGUI()
    {
        SerializedObject obj = new SerializedObject(this);

        EditorGUILayout.PropertyField(obj.FindProperty("WaypointOrigin"));

        if(WaypointOrigin==null)
        {
            EditorGUILayout.HelpBox("please assign a waypoint", MessageType.Warning);
        }
        else
        {
            EditorGUILayout.BeginVertical("box");
            createButtons();
            EditorGUILayout.EndVertical();
        }

        obj.ApplyModifiedProperties();
    }


    void createButtons()
    {
        if(GUILayout.Button("CreateWaypoint"))
        {
            createWaypoints();
        }
                
    }

  void  createWaypoints()
    {
        GameObject waypointObject = new GameObject("waypoint " + WaypointOrigin.childCount, typeof(WayPoint));
        waypointObject.transform.SetParent(WaypointOrigin,false);

        WayPoint wayPoint = waypointObject.GetComponent<WayPoint>();

        if(WaypointOrigin.childCount>1)
        {
            wayPoint.previousWaypoint = WaypointOrigin.GetChild(WaypointOrigin.childCount - 2).GetComponent<WayPoint>();
            wayPoint.previousWaypoint.nextWaypoint = wayPoint;

            wayPoint.transform.position = wayPoint.previousWaypoint.transform.position;
            wayPoint.transform.forward = wayPoint.previousWaypoint.transform.forward;
        }

        Selection.activeObject = wayPoint.gameObject;
    }

}
