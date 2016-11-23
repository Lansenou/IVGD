using System;
using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.Gizmos;
using Random = System.Random;

public class Waypoint : MonoBehaviour
{
    public bool debug = true;
    public float gizmoRadius = 1f;
    public Color gizmoColor = Color.red;

    [SerializeField]
    private List<Waypoint> next = new List<Waypoint>();

    void OnDrawGizmos()
    {
        if (!debug) return;

        Gizmos.color = gizmoColor;
        Gizmos.DrawWireSphere(transform.position, gizmoRadius);

        foreach (Waypoint t in next)
        {
            if (t == null)
            {
                Debug.Log("Removing a waypoint connection as the object probably got removed in the scene");
                next.Remove(t);
                break;
            }

            DrawArrow.ForGizmo(transform.position, t.transform.position - transform.position, Color.blue, 3f, 20f);
        }
    }

    public void AddNext(Waypoint waypoint)
    {
        next.Add(waypoint);
    }

    public Waypoint GetNextRandom()
    {
        Random random = new Random();
        int index = random.Next(0, next.Count);
        return next[index];
    }

    public override bool Equals(System.Object obj)
    {
        if (obj == null) return false;

        Waypoint wp = obj as Waypoint;

        if (wp == null) return false;

        return transform.position == wp.transform.position;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}