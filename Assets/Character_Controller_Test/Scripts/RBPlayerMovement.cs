using UnityEngine;
using System.Collections;

public class RBPlayerMovement : MonoBehaviour {
    // Update is called once per frame
    private float moveHori;
    private float moveVert;
    private Vector3 movement = Vector3.zero;

    //.5 is grounded. Adjust later if desired.
    private float distToGround;
    private bool onGround = true;
    private Rigidbody rb;

    public float maxMoveSpeed;
    public float playerSpeed;
    public float jumpSpeed;
    public float gravity = 10.0f;

    //Get the rigidbody attached to the game object, and get the distance from the center of the character model to the ground.
    void Start() {
        rb = GetComponent<Rigidbody>();
        distToGround = GetComponent<Collider>().bounds.extents.y;
    }

    //Constantly check if the player is on the ground.
	void Update () {
        onGround = isGrounded();
    }
    //Call various functions that are related to movement.
    void FixedUpdate() {
        move();
        rotate();
        jump();
    }
    private void move() {
        // Get input from corresponding controller.
        moveHori = Input.GetAxis(gameObject.name + "_Horizontal");
        moveVert = Input.GetAxis(gameObject.name + "_Vertical");

        // Create new movement vector.
        movement = new Vector3(moveHori, movement.y, moveVert);
        // If the player is falling after jumping, add a negative force, towards the ground.
        if (isFalling()) {
            rb.AddForce(-Vector3.up);
        }
        // If the player goes above a particular speed 'maxMoveSpeed', then limit the
        // velocity vector of the rigidbody.
        if (rb.velocity.magnitude > maxMoveSpeed && onGround) {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxMoveSpeed);
        }
        //Apply force to the rigidbody.
        rb.AddForce(movement * playerSpeed);
    }
    private void jump() {
        // If a player presses the A button on a controller, apply a positive vertical 
        // force to the rigidbody.
        if (Input.GetButton(gameObject.name+"_Fire1") && isGrounded()) {
            rb.AddForce(Vector3.up*jumpSpeed);
        }
    }
    private void rotate() {
        // Rotate the player model based on the movement vector.
        if (movement != Vector3.zero){
            Quaternion rotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 10.0f * Time.deltaTime);
        }
    }
    private bool isGrounded() {
        // Check if player is on the ground.
        return Physics.Raycast(transform.position, -Vector3.up, distToGround) ;
    }
    private bool isFalling() {
        // See if the player is falling.
        return rb.velocity.y < -.5f;
    }
}
