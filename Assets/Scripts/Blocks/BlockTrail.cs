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
        SetColor(Color.green);
    }

    void Update()
    {
        CalculateDirection();
        RotateTowardsDirection();
    }

    public void SetColor(Color color)
    {
        var col = particleSystem.colorOverLifetime;
        Gradient gradient = new Gradient();
        gradient.SetKeys(new[] {new GradientColorKey(color, 0.0f), new GradientColorKey(color, 1.0f)},
                         new[] {new GradientAlphaKey(1.0f, 0.5f), new GradientAlphaKey(0.0f, 1.0f)});

        col.enabled = true;
        col.color = gradient;
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