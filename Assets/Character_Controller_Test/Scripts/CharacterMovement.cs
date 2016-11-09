using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {
    // Update is called once per frame
    private float moveHori;
    private float moveVert;
    private Vector3 movement = Vector3.zero;
    private Rigidbody rb;

    public float moveSpeed = 5.0f;
    public float jumpSpeed = 7.0f;

    void Start() {
        rb = GetComponent<Rigidbody>();
    }

	void Update () {
        updateMovement();
        move();
        rotate();
    }

    void updateMovement() {
        moveHori = Input.GetAxis(gameObject.name + "_Horizontal");
        moveVert = Input.GetAxis(gameObject.name + "_Vertical");
        movement = new Vector3(moveHori * moveSpeed * Time.deltaTime, movement.y, moveVert * moveSpeed * Time.deltaTime);
    }

    void move() {
        transform.Translate(movement, Space.World);
    }
    void rotate() {
        if (movement != Vector3.zero){
            Quaternion rotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 10.0f * Time.deltaTime);
        }
    }
}
