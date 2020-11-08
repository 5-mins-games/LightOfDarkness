using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/BoidGenConfig")]
public class BoidGenConfig : ScriptableObject
{
    [Header("Basic")]
    public Vector3 center = Vector3.zero;

    [Range(1, 30)]
    public int number = 5;
    [Range(1, 5)]
    public int range = 5;
    [Range(0, 5)]
    public float keepDistance = 3;
    [Range(0, 3)]
    public float steerForce = 3;

    [Header("Gravity")]
    public bool enableGravity = false;

    [Range(0, .1f)]
    public float gravity = .1f;

    [Header("Ground")]
    public bool enableGround = false;

    [Range(-3, 3)]
    public float groundHeight = .1f;

    [Header("Burst")]
    public bool enableBurst = false;

    [Range(.1f, 3f)]
    public float burstForce = 1f;
    [Range(.1f, 3f)]
    public float burstRadius = 3f;
    [Range(5f, 30f)]
    public float maxHeight = 10f;

    [Header("Steer")]
    public bool rotateOnlyXZ = true;
}
