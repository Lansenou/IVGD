using UnityEngine;
using System.Collections;

public class SparkParticles : MonoBehaviour
{
    public Transform BillboardTarget;
    new ParticleSystem particleSystem;
    readonly ParticleSystem.Particle[] particles = new ParticleSystem.Particle[25];

    void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();

        if (BillboardTarget == null)
            BillboardTarget = Camera.main.transform;
    }

    void FixParticleRotations()
    {
        int count = particleSystem.GetParticles(particles);

        for (int i = 0; i < count; i++)
        {
            Vector3 dir = particles[i].velocity.normalized;
            float rot = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            particles[i].rotation = rot;
        }

        particleSystem.SetParticles(particles, particleSystem.particleCount);
    }

    public void Play()
    {
        transform.LookAt(BillboardTarget);
        particleSystem.Play();
        StartCoroutine(LateFix());
    }

    IEnumerator LateFix()
    {
        yield return null;
        FixParticleRotations();
    }
}