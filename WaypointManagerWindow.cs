using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class WaypointManager : EditorWindow
{
    [MenuItem("Waypoint/Waypoints Editor Tools")]
    public static void ShowWindow()
    {
        GetWindow<WaypointManager>("Waypoints Editor Tools");
    }

    public Transform waypointOrigin;

    private void OnGUI()
    {
        SerializedObject obj = new SerializedObject(this);

        EditorGUILayout.PropertyField(obj.FindProperty("waypointOrigin"));

        if(waypointOrigin==null)
        {
            EditorGUILayout.HelpBox("Please assign a waypoint oigin transform. ",MessageType.Warning);
        }
        else
        {
            EditorGUILayout.BeginVertical("box");
            CreateButtons();
            EditorGUILayout.EndVertical();

        }

        obj.ApplyModifiedProperties();


    }

    void CreateButtons()
    {
        if(GUILayout.Button("Create Waypoint"))
        {
            CreateWaypoint();
        }
    }
    void CreateWaypoint()
    {
        GameObject waypointObject=new GameObject("waypoint "+waypointOrigin.childCount,typeof(Waypoint));
        waypointObject.transform.SetParent(waypointOrigin,false);

        Waypoint waypoint = waypointObject.GetComponent<Waypoint>();
        if(waypointOrigin.childCount>1)
        {
            waypoint.previouseWaypoint=waypointOrigin.GetChild(waypointOrigin.childCount-2).GetComponent<Waypoint>();
            waypoint.previouseWaypoint.nextWaypoint=waypoint;

            waypoint.transform.position=waypoint.previouseWaypoint.transform.position;
            waypoint.transform.forward=waypoint.previouseWaypoint.transform.forward;
        }
        Selection.activeGameObject=waypoint.gameObject;
    }
}
