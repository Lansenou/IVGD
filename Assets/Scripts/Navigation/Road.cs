using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

public class Road : MonoBehaviour
{
    public float waypointOffset = -1.5f;

    protected List<Waypoint> waypoints = new List<Waypoint>();

    // Debugging
    private bool debug = false;
    private float gizmoRadius = 0.5f;
    private Color gizmoColor = Color.red;

    public enum RoadType
    {
        Lane, Corner, Intersection, Tintersection, DeadEnd
    }
    
    void OnDrawGizmos()
    {
        if (!debug)
        {
            return;
        }

        foreach (Waypoint waypoint in waypoints)
        {
            if (waypoint == null)
                return;

            Gizmos.color = gizmoColor;
            Gizmos.DrawSphere(waypoint.transform.position, gizmoRadius);
        }
    }

//    public abstract RoadType GetRoadType();
//    public abstract int WaypointLimit();
//    public abstract void CalculateWaypointPosition();

    public void AddWaypoint(Waypoint waypoint)
    {
        waypoints.Add(waypoint);
    }

    public void ClearWaypoints()
    {
        // Uncomment in case of a bug when removing children
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            DestroyImmediate(child.gameObject);
        }

        foreach (Waypoint waypoint in waypoints)
        {
            if (waypoint == null)
                continue;

            DestroyImmediate(waypoint.gameObject);
        }

        waypoints.Clear();
    }

    public void SetDebugging(bool debug)
    {
        this.debug = debug;
        print("Debugging " + (debug ? "enabled" : "disabled"));
    }

    public void SetDebugging(Color gizmoColor, float gizmoRadius)
    {
        this.gizmoColor = gizmoColor;
        this.gizmoRadius = gizmoRadius;
    }

    public void SetDebugging(bool debug, Color gizmoColor, float gizmoRadius)
    {
        SetDebugging(debug);
        this.gizmoColor = gizmoColor;
        this.gizmoRadius = gizmoRadius;
    }

    public int GetWaypointCount()
    {
        return waypoints.Count;
    }

    protected void PositionWaypointCenter(Waypoint w1, Waypoint w2)
    {
        float centerOffset = GetQuarter(GetMeshWidth());

        PositionWaypointDuo(w1, w2, transform.position, Vector3.right, centerOffset);
    }

    protected void PositionWaypointForward(Waypoint w1, Waypoint w2)
    {
        float centerOffset = GetQuarter(GetMeshWidth());

        w1.gameObject.name = w1.gameObject.name + "Forward" + 1;
        w2.gameObject.name = w2.gameObject.name + "Forward" + 2;

        Vector3 position = GetPosition(transform.forward, GetMeshLength());
        PositionWaypointDuo(w1, w2, position, Vector3.right, centerOffset);
    }

    protected void PositionWaypointBack(Waypoint w1, Waypoint w2)
    {
        float centerOffset = GetQuarter(GetMeshWidth());

        w1.gameObject.name = w1.gameObject.name + "Back" + 1;
        w2.gameObject.name = w2.gameObject.name + "Back" + 2;

        Vector3 position = GetPosition(transform.forward * -1, GetMeshLength());
        PositionWaypointDuo(w1, w2, position, Vector3.right, centerOffset);
    }

    protected void PositionWaypointRight(Waypoint w1, Waypoint w2)
    {
        float centerOffset = GetQuarter(GetMeshLength());

        w1.gameObject.name = w1.gameObject.name + "Right" + 1;
        w2.gameObject.name = w2.gameObject.name + "Right" + 2;

        Vector3 position = GetPosition(transform.right, GetMeshLength());
        PositionWaypointDuo(w1, w2, position, Vector3.forward, centerOffset);
    }

    protected void PositionWaypointLeft(Waypoint w1, Waypoint w2)
    {
        float centerOffset = GetQuarter(GetMeshLength());

        w1.gameObject.name = w1.gameObject.name + "Left" + 1;
        w2.gameObject.name = w2.gameObject.name + "Left" + 2;

        Vector3 position = GetPosition(transform.right * -1, GetMeshLength());
        PositionWaypointDuo(w1, w2, position, Vector3.forward, centerOffset);
    }

    private Vector3 GetPosition(Vector3 direction, float size)
    {
        return transform.position + (direction * (size / 2));
    }

    private void PositionWaypointDuo(Waypoint w1, Waypoint w2, Vector3 position, Vector3 offsetDirection, float centerOffset)
    {
        Vector3 translation = offsetDirection * (centerOffset + waypointOffset);

        w1.transform.position = position;
        w2.transform.position = position;

        w1.transform.Translate(translation, gameObject.transform);
        w2.transform.Translate(-translation, gameObject.transform);
    }

    private float GetQuarter(float val)
    {
        return val / 4;
    }

    private float GetMeshWidth()
    {
        return GetMeshSize().x;
    }

    private float GetMeshLength()
    {
        return GetMeshSize().z;
    }

    private Vector3 GetMeshSize()
    {
        Mesh mesh = GetComponent<MeshFilter>().sharedMesh;
        return mesh.bounds.size;
    }
}