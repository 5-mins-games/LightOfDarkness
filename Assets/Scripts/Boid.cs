using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Boid : MonoBehaviour
{
    [Range(.1f, 5)]
    public float initForce = 3f;
    [Range(0, 3)]
    public float steerForce = .5f;
    [Range(0, 3)]
    public float keepDistance = 1;
    [Range(-3, 3)]
    public float groundHeight = .1f;
    [Range(0, 5f)]
    public float gravity = .03f;
    [Range(5f, 30f)]
    public float maxHeight = 30f;

    public Vector3 center = new Vector3(0, 2, 0);
    public bool rotateOnlyXZ = true;
    public bool enableGround = true;
    public bool enableGravity = false;

    Rigidbody rb;
    Vector3 velocity;

    readonly HashSet<Boid> boids = new HashSet<Boid>();

    void FixedUpdate()
    {
        Steer();
    }

    public void Initialize()
    {
        GameObject parent = transform.parent.gameObject;

        if (parent == null)
        {
            Debug.LogWarning("Boids must be placed under a parent!");
            return;
        }
        boids.Clear();

        for (int i = 0; i < parent.transform.childCount; i++)
        {
            GameObject go = parent.transform.GetChild(i).gameObject;
            Boid boid = go.GetComponent<Boid>();

            if (go != gameObject && boid != null && !boids.Contains(boid))
            {
                boids.Add(boid);
            }
        }
        rb = GetComponent<Rigidbody>();
    }

    public void InitializeSpeed()
    {
        rb.AddForce(transform.right * initForce, ForceMode.Impulse);
    }

    void Steer()
    {
        if (rb == null)
            return;
        if (rb.isKinematic)
            return;

        Vector3 pos = transform.position;

        if (rotateOnlyXZ)
        {
            center.y = pos.y;
        }
        // center
        Vector3 direction = Vector3.Normalize(center - pos);
        float distanceFactor = Mathf.Pow(Vector3.Distance(center, pos), 2f) / 25f;
        rb.AddForce(direction * steerForce * distanceFactor, ForceMode.Impulse);

        // boids
        Vector3 force = Vector3.zero;
        foreach (Boid boid in boids)
        {
            if (boid == null)
                continue;
            direction = Vector3.Normalize(boid.transform.position - pos);
            float distance = Vector3.Distance(boid.transform.position, pos);
            if (distance < keepDistance)
                direction *= -1;
            distanceFactor = Mathf.Pow(distance - keepDistance, 2f) / 25f;
            force += direction * steerForce * distanceFactor;
        }
        if (boids.Count > 0)
            rb.AddForce(force / boids.Count, ForceMode.Impulse);
        // ground
        if (enableGround && pos.y < groundHeight)
        {
            rb.AddForce(Vector3.up, ForceMode.Impulse);
        }
        // gravity
        if (enableGravity)
        {
            rb.AddForce(Vector3.down * gravity, ForceMode.Impulse);
        }
        // max height
        if (pos.y > maxHeight)
        {
            rb.AddForce(Vector3.down, ForceMode.Impulse);
        }
    }

    public void Freeze()
    {
        velocity = rb.velocity;
        rb.isKinematic = true;
    }

    public void Animate()
    {
        if (rb == null)
            return;
        rb.isKinematic = false;
        if (Vector3.Distance(velocity, Vector3.zero) > 0.01f)
            rb.velocity = velocity;
    }

    public void ApplyBurst(float burst)
    {
        if (rb == null)
            return;
        rb.AddForce(Vector3.up * burst, ForceMode.Impulse);
    }

    public bool CloseToCenter(float radius)
    {
        Vector3 p1 = transform.position;
        Vector3 p2 = center;

        return Vector3.Distance(p1, p2) < radius;
    }
}
