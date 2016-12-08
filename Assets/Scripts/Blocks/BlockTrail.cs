using UnityEngine;
using System.Collections;

public class BlockTrail : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem particleSystem;

    private Vector3 oldPosition = Vector3.zero;
    private Vector3 direction = Vector3.zero;

    void Start()
    {
        oldPosition = transform.position;
    }

    void Update()
    {
        CalculateDirection();
        RotateTowardsDirection();
    }


    private void RotateTowardsDirection()
    {
        transform.LookAt(transform.position + direction, Vector3.up);
    }

    private void CalculateDirection()
    {
        Vector3 newPosition = transform.position;
        direction = newPosition - oldPosition;
        direction.Normalize();
        oldPosition = newPosition;
    }
}