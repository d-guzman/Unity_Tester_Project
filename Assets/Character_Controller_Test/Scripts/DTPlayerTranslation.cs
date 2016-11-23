using UnityEngine;
using System.Collections;

public class DTPlayerTranslation : MonoBehaviour {
    /*This script is for doing character movement along the X and Z axes via
    directly manipulating the gameObject.transform, while using rigidbody
    forces to manage the jumping.*/

    // The playerSpeed variable determines how quickly a player will be moving
    // along the X and Z axes.
    public float playerSpeed = 6.0f;

    // Variables that relate to jumping.
    public int maxJumpFrames = 60;
    private int currentJumpFrames;
    private bool isJumping = false;

    private float distToGround;

    // Floats that will represent Horizontal and Vertical movement.
    private float moveHori;
    private float moveVert;

    // A vector that will represent movement.
    private Vector3 movement;

    // A rigidbody that will be used for jumping.
    private Rigidbody rb;

    // Called only to get the rigidbody. For now
    void Start() {
        rb = GetComponent<Rigidbody>();
        distToGround = GetComponent<Collider>().bounds.extents.y;
    }
	// Update is called once per frame
	void Update () {
        move();
	}

    // This is called to handle the jumping.
    void FixedUpdate() {
        jump();
    }

    private void move() {
        // The use of gameObject.name gets input from a specific joystick via the Input Manager,
        // but the gameObject in Unity must be specifically named 'P#', where # is a number between 1-4.
        moveHori = Input.GetAxisRaw(gameObject.name + "_Horizontal");
        moveVert = Input.GetAxisRaw(gameObject.name + "_Vertical");

        // Create a new movement vector based on the input from a specific joystick.
        movement = new Vector3(moveHori * playerSpeed * Time.deltaTime, movement.y, moveVert * playerSpeed * Time.deltaTime);

        // Rotate the model. Before I used to do transform rotation both separately from and
        // after translation, and may choose to do so in the future if it proves to be beneficial.
        if (movement != Vector3.zero) {
            Quaternion rotation = Quaternion.LookRotation(movement);
            // I don't know if I should be using .localrotation here, but .rotation hasn't failed me yet. The use of a constant 10.0f
            // is also habitual and if it is ever needed, it will be changed.
            gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, rotation, 10.0f * Time.deltaTime);
        }

        // Move the player. The use of space.world is to make sure that it can move without the
        // transform rotation messing things up.
        gameObject.transform.Translate(movement, Space.World);
    }

    private void jump() {
        // The input retrival method is the same as above.
        if (currentJumpFrames < maxJumpFrames && isFalling() == false && Input.GetButton(gameObject.name + "_Fire1"))
        {
            //isJumping = true;
            rb.useGravity = false;
            rb.velocity = Vector3.up * 6.5f;
            currentJumpFrames++;
        }
        else
        {
            if (currentJumpFrames != 0)
            {
                rb.useGravity = true;
                rb.velocity = -Vector3.up * 6.0f;
                if (isGrounded()) {
                    currentJumpFrames = 0;
                    rb.velocity = Vector3.zero;
                }
                currentJumpFrames--;
            }
        }
    }

    private bool isFalling()
    {
        if (rb.velocity.y < -.001f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool isGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround);
    }

}
