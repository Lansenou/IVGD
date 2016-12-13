using System;
using UnityEngine;
using System.Collections;

public class Agent : MonoBehaviour
{
    [SerializeField]
    private float mass;

    [SerializeField]
    private float maxSpeed;

    [SerializeField]
    private float maxForce;

    [SerializeField]
    private Waypoint destination;

    [SerializeField]
    private float minRange = 0.2f;

    private Vector3 velocity;
    
    void Update()
    {
        UpdateDestination();
        MoveToDestination();
    }

    private void UpdateDestination()
    {
        if (DestinationReached())
        {
            destination = destination.GetNextRandom();
        }
    }

    private void MoveToDestination()
    {
        Vector3 desiredVelocity = GetDirection() * maxSpeed;
        Vector3 steering = desiredVelocity - velocity;

        steering = Vector3.ClampMagnitude(steering, maxForce);
        steering = steering / mass;

        velocity = Vector3.ClampMagnitude(velocity + steering, maxSpeed * Time.deltaTime);
        velocity.y = 0f;

        UpdatePosition(velocity);
        UpdateRotation(velocity);
    }

    private void UpdatePosition(Vector3 velocity)
    {
        transform.position += velocity;
    }

    private void UpdateRotation(Vector3 velocity)
    {
        transform.localRotation = Quaternion.LookRotation(velocity, transform.up);      
    }

    private Vector3 GetDirection()
    {
        return (destination.transform.position - transform.position).normalized;
    }

    private bool DestinationReached()
    {
        Vector3 currentPos = new Vector3(transform.position.x, 0f, transform.position.z);
        return Vector3.Distance(currentPos, destination.transform.position) < minRange;
    }
}