using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public float speed = 3.0f; // Variable used to determine character speed
    public bool grounded = false; // Flag to check if player is grounded
    public Transform jumpCheck; // Transform used for jumping
    float jumpTime, jumpDelay = 0.5f; // Used to determine if player is in the air
    bool jumped; // Flag used to check which animation should be playing

    Animator anim; // Allows for movement animations

    // Use this for initialization
    void Start () {

        anim = GetComponent<Animator>(); // Allows access to the animator for variable 'anim'
    }

    // Update is called once per frame
    void Update () {

        Movement();
        Jumping();
        Raycasting();
    }

    void Raycasting()
    {
        Debug.DrawLine(this.transform.position, jumpCheck.position, Color.green); // Debug visual representation of the Linecast for the player jump

        // Assign the bool 'Ground' with a linecast. Returns true or false depending on whether the end of line 'jumpCheck' is touching the ground
        grounded = Physics2D.Linecast(this.transform.position, jumpCheck.position, 1 << LayerMask.NameToLayer("Ground"));

    }


    void Movement() // Function that stores all movements
    {
        anim.SetFloat("speed", Mathf.Abs(Input.GetAxis("Horizontal"))); // Records a value between -1 and 1, depending on the direction the object is moving

        if (Input.GetAxisRaw("Horizontal") > 0) // Range is -1 to 1. -1 = Left, 1 = Right.
        { // Checks if 'D' or 'RightArrow' is pressed. If so, do the following...

            transform.Translate(Vector2.right * speed * Time.deltaTime); // Translate the object in the -x direction, moving it right
            transform.eulerAngles = new Vector2(0, 0); // Direction of character doesn't change
        }

        if (Input.GetAxisRaw("Horizontal") < 0) // Range is -1 to 1. -1 = Left, 1 = Right.
        { // Checks if 'A' or 'LeftArrow' is pressed. If so, do the following...

            transform.Translate(Vector2.right * speed * Time.deltaTime); // Translate the object in the x direction, moving it left
            transform.eulerAngles = new Vector2(0, 180); // Flips the character (and its animations) to face the other direction
        }
    }


    void Jumping()
    {
        if (Input.GetKeyDown(KeyCode.Space) && grounded == true)
        { // Checks if 'Space' is pressed down and the player is grounded. If so, do the following...

            GetComponent<Rigidbody2D>().AddForce(Vector2.up * 420f); // Apply a force upwards on the object
            jumpTime = jumpDelay; // When jumping, jumpTime is equal to 0.5
            anim.SetTrigger("Jump"); // Trigger the jumping animation
            jumped = true; // Keeps the jump animation going.
        }

        jumpTime -= Time.deltaTime; // Countdown in real time. At this point, jumpTime will equal 0.5 and decrease
        if (jumpTime <= 0 && grounded && jumped)
        { // Once jumpTime is equal to 0 and the object is grounded, do the following...

            anim.SetTrigger("Land");
            jumped = false; // Stops the jump animation
        }
    }

}
