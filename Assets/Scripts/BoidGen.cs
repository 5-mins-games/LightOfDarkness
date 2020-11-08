using System.Collections.Generic;
using UnityEngine;

public class BoidGen : MonoBehaviour
{
    public Boid boidPrefab;
    public BoidGenConfig config;
    public bool animate = true;

    readonly List<Boid> boids = new List<Boid>();

    void Start()
    {
        Initialize();

        foreach (Boid boid in boids)
        {
            boid.Initialize();
            boid.InitializeSpeed();
        }
    }

    void FixedUpdate()
    {
        foreach (Boid boid in boids)
        {
            if (animate) boid.Animate();
            else boid.Freeze();
            boid.keepDistance = config.keepDistance;
            boid.steerForce = config.steerForce;

            boid.enableGravity = config.enableGravity;
            boid.gravity = config.gravity;

            boid.enableGround = config.enableGround;
            boid.groundHeight = config.groundHeight;

            boid.center = config.center;
            boid.maxHeight = config.maxHeight;

            boid.rotateOnlyXZ = config.rotateOnlyXZ;

            if (config.enableBurst && boid.CloseToCenter(config.burstRadius))
            {
                boid.ApplyBurst(config.burstForce);
            }
        }
    }

    void Initialize()
    {
        for (int i = 0; i < config.number; i++)
        {
            float x = Random.value * config.range - config.range * .5f;
            float z = Random.value * config.range - config.range * .5f;
            Vector3 offset = new Vector3(x, Random.value * .1f - .05f, z);

            Boid boid = Instantiate(boidPrefab, transform, false);
            boid.transform.position = transform.position + offset;
            boid.transform.Rotate(Random.value * 180, 0, Random.value * 180);

            boids.Add(boid);
        }
    }
}
