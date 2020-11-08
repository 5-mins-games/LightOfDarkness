using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    //player controller component
    CharacterController controller;

    //player stats
    public float speed;             //Acceleration rate
    public float maxSpeed;          //Max speed
    public float walkSpeed;         //Walking speed
    public float airControl;        //A acceleration multiplier used to reduce player's speed in air
    public float gravity;           //Gravity
    public float jumpHeight;        //Jump Height
    public float friction;          //Friction, a speed multiplier, disabled when player is in air

    public Transform groundCheck;   //A game object used to check if the player is grounded
    public float groundDistance;    //The radius of ground checking sphere
    public LayerMask groundMask;    //Ground layer used for ground checking

    public Vector3 velocity_v;      //Vertical velocity
    public Vector3 velocity_h;      //Horizontal velocity
    public bool isGrounded;         //A flag that indicate if the player is grounded or not

    const float EPSILON = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();

        //All intialization is done in the inspector
        maxSpeed = walkSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        //Check if grounded
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        //If fall to the ground
        if (isGrounded && velocity_v.y < 0)
        {
            //Set velocity to a small negative number. not zero because we want to ensure player falls to the ground
            velocity_v.y = -2f;
        }

        //Get player movement input
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //Slow down player if no long runnning
        if (isGrounded && Mathf.Abs(maxSpeed - walkSpeed) > EPSILON)
        {
            maxSpeed = walkSpeed;
        }

        //Add horizontal velocity based on the input
        if (isGrounded)
        {
            velocity_h += (transform.right * x + transform.forward * z) * speed;
        }
        //Apply air control multiplier when in air
        else
        {
            velocity_h += (transform.right * x + transform.forward * z) * speed * airControl;
        }

        //Clamp velocity using max speed
        velocity_h = Vector3.ClampMagnitude(velocity_h, maxSpeed);

        //Move player horizontally
        controller.Move(velocity_h * Time.deltaTime);

        //Debug.Log("vel_h: " + velocity_h);

        //Apply friction to horizontal velocity
        velocity_h *= friction;

        //Set horizontal velocity to zero if too small
        if (velocity_h.magnitude <= 0.3f)
        {
            velocity_h = Vector3.zero;
        }

        //Apply gravity
        velocity_v.y += gravity * Time.deltaTime;

        //Move player vertically
        controller.Move(velocity_v * Time.deltaTime);
    }
}
