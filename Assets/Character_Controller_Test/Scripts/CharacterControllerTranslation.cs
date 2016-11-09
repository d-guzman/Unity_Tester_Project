using UnityEngine;
using System.Collections;

public class CharacterControllerTranslation : MonoBehaviour {
    public float moveSpeed = 5.0f;
    public float jumpSpeed = 6.0f;
    public float gravity = 20.0f;
    private Vector3 moveDirection = Vector3.zero;

	void Update () {
        //Get the Character Controller component from the game object.
        CharacterController controller = GetComponent<CharacterController>();
        //If they are on the ground... else, no new input is recorded until they land.
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("P1_Horizontal"), 0.0f, Input.GetAxis("P1_Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= moveSpeed;
            //If the player presses jump... modify the move vectors Y component.
            if (Input.GetButton("P1_Fire1"))
            {
                moveDirection.y = jumpSpeed;
            }
        }
        //Make sure the character can fall.
        moveDirection.y -= gravity * Time.deltaTime;
        //Move the character.
        controller.Move(moveDirection * Time.deltaTime);
    }
}
