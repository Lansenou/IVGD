using System;
using UnityEngine;
using System.Collections;

public class REMOVE_THIS_MOVEMENT : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float moveDistance = 15f;

    private int targetIndex = 1;
    private Vector3[] points = new Vector3[3];
    private Vector3 target = Vector3.zero;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        foreach (Vector3 v in points)
        {
            Gizmos.DrawWireSphere(v, 1f);
        }
    }

    void Start ()
	{
	    points[0] = transform.position;
        points[1] = points[0] + new Vector3(moveDistance, 0, 0);
        points[2] = points[0] + new Vector3(moveDistance, 0, moveDistance);

        targetIndex = 1;
	    target = points[targetIndex];
	}
    
    void Update ()
	{
	    transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);

	    if (Reached(transform.position, target))
	        target = Next();
	}

    private bool Reached(Vector3 pos, Vector3 t)
    {
        return Vector3.Distance(pos, t) < 0.05;
    }

    private Vector3 Next()
    {
        try
        {
            targetIndex++;
            return points[targetIndex];
        }
        catch (IndexOutOfRangeException e)
        {
            targetIndex = 0;
            return points[targetIndex];
        }
    }
}
