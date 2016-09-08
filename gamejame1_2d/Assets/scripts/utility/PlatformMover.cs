using UnityEngine;
using System.Collections;

public class PlatformMover : MonoBehaviour
{
    [Tooltip("Distance from starting position")] [SerializeField] private float delta = 1.5f;
    [SerializeField] private float speed = 5f;
    [SerializeField] private Axis axis = Axis.X;

    private enum Axis
    {
        X,
        Y,
        Z
    }

    private Vector3 startPosition;

    void Awake()
    {
        StoreStartPosition();
    }

    void Update()
    {
        MovePlatform();
    }

    private void StoreStartPosition()
    {
        startPosition = this.transform.position;
    }

    private void MovePlatform()
    {
        Vector3 v = startPosition;

        switch (axis)
        {
            case Axis.X:
                v.x += delta * Mathf.Sin(Time.time * speed);
                break;
            case Axis.Y:
                v.y += delta * Mathf.Sin(Time.time * speed);
                break;
            default:
                v.z += delta * Mathf.Sin(Time.time * speed);
                break;
        }
        transform.position = v;
    }
}