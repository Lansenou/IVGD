using UnityEngine;
using System.Collections;

public class Gizmo : MonoBehaviour
{
    [SerializeField]
    private float radius = 2f;

    [SerializeField]
    private Color color = Color.blue;

    [SerializeField]
    private Shape shape = Shape.Cirle;

    private enum Shape
    {
        Cirle,
        Cube
    }

    void OnDrawGizmos()
    {
        Gizmos.color = color;
        switch (shape)
        {
            case Shape.Cirle:
                Gizmos.DrawWireSphere(transform.position, radius);
                break;
            case Shape.Cube:
                Gizmos.DrawCube(transform.position, new Vector3(radius, radius, radius));
                break;
        }
    }
}