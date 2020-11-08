using System.Collections.Generic;
using UnityEngine;

public class SpringGen : MonoBehaviour
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
            //boid.InitializeSpeed();
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

            if (config.enableBurst && boid.transform.position.y < config.groundHeight)
            {
                boid.ApplyBurst(config.burstForce * Random.value);
            }
        }

        if (animate)
        {
            for (int i = 0; i < boids.Count; i++)
            {
                Boid boid = boids[i];

                if (boid.transform.position.y < config.groundHeight - 1)
                {
                    ReplaceBoid(i);
                }
            }

            foreach (Boid boid in boids)
            {
                if (boid != null) boid.Initialize();
            }
        }
    }

    void Initialize()
    {
        for (int i = 0; i < config.number; i++)
        {
            boids.Add(CreateBoid());
        }
    }

    Boid CreateBoid()
    {
        float x = Random.value * config.range - config.range * .5f;
        float z = Random.value * config.range - config.range * .5f;
        Vector3 offset = new Vector3(x, Random.value * .1f - .05f, z);

        Boid boid = Instantiate(boidPrefab, transform, false);
        boid.transform.position = transform.position + offset;
        boid.transform.Rotate(Random.value * 180, 0, Random.value * 180);

        return boid;
    }

    void ReplaceBoid(int i)
    {
        Boid boid = boids[i];
        DestroyImmediate(boid.gameObject);

        boids[i] = CreateBoid();
    }
}
