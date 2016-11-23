using UnityEngine;
using System.Collections;

public class CCPlayerMovement : MonoBehaviour {
    public float moveSpeed = 5.0f;
    public float jumpSpeed = 6.0f;
    public float gravity = 20.0f;
    private Vector3 moveDirection = Vector3.zero;

	void Update () {
        //Get the Character Controller component from the game object.
        CharacterController controller = GetComponent<CharacterController>();
        //If the character is on the ground, execute this code block.
        if (controller.isGrounded){
            moveDirection = new Vector3(Input.GetAxis("P1_Horizontal"), 0.0f, Input.GetAxis("P1_Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= moveSpeed;
        }
        //If the character is in the air, execute this code block.
        else {
            moveDirection = new Vector3(Input.GetAxis("P1_Horizontal")*moveSpeed, moveDirection.y - (gravity*Time.deltaTime), Input.GetAxis("P1_Vertical")*moveSpeed);
            moveDirection = transform.TransformDirection(moveDirection);
        }
        
        //If the player presses jump... modify the move vectors Y component.
        if (Input.GetButton("P1_Fire1") && controller.isGrounded){
            moveDirection.y = jumpSpeed;
        }
        //Move the character.
        controller.Move(moveDirection * Time.deltaTime);
        
    }
}
