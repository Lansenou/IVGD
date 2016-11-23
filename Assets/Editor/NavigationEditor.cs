using UnityEngine;
using System.Collections;
using UnityEditor;

public class NavigationEditor
{
    private const string PARENT_TAG = "Waypoint Parent"; 
    private const string WAYPOINT_TAG = "Waypoint"; 
    private GameObject parent;

    public Waypoint CreateWaypoint(Road road)
    {
        return CreateWaypoint(road.transform.position, GetParent());
    }

    public Waypoint CreateNextWaypoint(Waypoint waypoint)
    {
        Waypoint wp = CreateWaypoint(waypoint.transform.position + (Vector3.forward * 5), GetParent());
        waypoint.AddNext(wp);
        return wp;
    }

    public void UpdateGizmos(float radius, Color color)
    {
        Waypoint[] waypoints = Object.FindObjectsOfType<Waypoint>();

        foreach (Waypoint t in waypoints)
        {
            t.gizmoRadius = radius;
            t.gizmoColor = color;
        }
    }

    private Waypoint CreateWaypoint(Vector3 position, GameObject parent)
    {
        GameObject obj = new GameObject("Waypoint") { tag = WAYPOINT_TAG};
        obj.transform.parent = parent.transform;
        obj.transform.position = position;
        return obj.AddComponent<Waypoint>();
    }

    private GameObject GetParent()
    {
        if (parent == null)
            parent = FindParent() ?? CreateParent();

        return parent;
    }

    private GameObject FindParent()
    {
        return GameObject.FindGameObjectWithTag(PARENT_TAG);
    }

    private GameObject CreateParent()
    {
        return new GameObject("Waypoints") {tag = PARENT_TAG};
    }
}
